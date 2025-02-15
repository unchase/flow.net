﻿using Flow.Net.Sdk.Cadence;
using Flow.Net.Sdk.Exceptions;
using Flow.Net.Sdk.Models;
using System.Collections.Generic;
using System.Linq;

namespace Flow.Net.Sdk.Templates
{
    public static class Account
    {
	    private const string CreateAccountTemplate = @"
transaction(publicKeys: [String], contracts: { String: String})
{
	prepare(signer: AuthAccount)
	{
		let acct = AuthAccount(payer: signer)
		for key in publicKeys {
				acct.addPublicKey(key.decodeHex())
		}
		for contract in contracts.keys {
			acct.contracts.add(name: contract, code: contracts[contract]!.decodeHex())
		}
	}
}";

	    public static FlowTransaction CreateAccount(IEnumerable<FlowAccountKey> flowAccountKeys, FlowAddress authorizerAddress, IEnumerable<FlowContract> flowContracts = null)
        {
			if (flowAccountKeys == null)
				throw new FlowException("Flow account key required.");

			flowAccountKeys = flowAccountKeys.ToList();
			
			if(!flowAccountKeys.Any())
				throw new FlowException("Flow account key required.");

			var accountKeys = new CadenceArray();
			foreach (var key in flowAccountKeys)
			{
				accountKeys.Value.Add(
					new CadenceString(
						Rlp.EncodedAccountKey(key).FromByteArrayToHex()
					));
			}

			var contracts = new CadenceDictionary();
			
			if (flowContracts != null)
            {
	            flowContracts = flowContracts.ToList();

	            if (flowContracts.Any())
	            {
		            foreach(var contract in flowContracts)
		            {
			            contracts.Value.Add(
				            new CadenceDictionaryKeyValue
				            {
					            Key = new CadenceString(contract.Name),
					            Value = new CadenceString(contract.Source.FromStringToHex())
				            });
		            }
	            }
            }

			var tx = new FlowTransaction
			{
				Script = CreateAccountTemplate,
				Arguments = new List<ICadence>
				{
					accountKeys,
					contracts
				}
			};

			// add authorizer
			tx.Authorizers.Add(authorizerAddress);

			return tx;
        }

		private static FlowTransaction AccountContractBase(string script, FlowContract flowContract, FlowAddress authorizerAddress)
        {
			var tx = new FlowTransaction
			{
				Script = script,
				Arguments = new List<ICadence>
				{
					new CadenceString(flowContract.Name),
					new CadenceString(flowContract.Source.FromStringToHex())
				}
			};

			// add authorizer
			tx.Authorizers.Add(authorizerAddress);

			return tx;
		}

		private const string AddAccountContractTemplate = @"
transaction(name: String, code: String)
{
	prepare(signer: AuthAccount) {
		signer.contracts.add(name: name, code: code.decodeHex())
	}
}";

		public static FlowTransaction AddAccountContract(FlowContract flowContract, FlowAddress authorizerAddress)
		{
			return AccountContractBase(AddAccountContractTemplate, flowContract, authorizerAddress);
		}

		private const string UpdateAccountContractTemplate = @"
transaction(name: String, code: String)
{
	prepare(signer: AuthAccount) {
		signer.contracts.update__experimental(name: name, code: code.decodeHex())
	}
}";

		public static FlowTransaction UpdateAccountContract(FlowContract flowContract, FlowAddress authorizerAddress)
		{
			return AccountContractBase(UpdateAccountContractTemplate, flowContract, authorizerAddress);
		}

		private const string DeleteAccountContractTemplate = @"
transaction(name: String)
{
	prepare(signer: AuthAccount) {
		signer.contracts.remove(name: name)
	}
}";

		public static FlowTransaction DeleteAccountContract(string contractName, FlowAddress authorizerAddress)
        {
			var tx = new FlowTransaction
			{
				Script = DeleteAccountContractTemplate
			};

			// add argument
			tx.Arguments.Add(new CadenceString(contractName));

			// add authorizer
			tx.Authorizers.Add(authorizerAddress);

			return tx;
		}
	}
}
