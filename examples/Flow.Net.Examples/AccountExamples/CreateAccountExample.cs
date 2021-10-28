﻿using Flow.Net.Sdk;
using Flow.Net.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flow.Net.Examples
{
    public class CreateAccountExample : ExampleBase
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("\nRunning CreateAccountExample\n");            
            // generate our new account key
            var flowAccountKey = FlowAccountKey.NewEcdsaAccountKey(SignatureAlgo.ECDSA_P256, HashAlgo.SHA3_256, 1000);

            // example found in base class
            var account = await CreateAccountAsync(new List<FlowAccountKey> { flowAccountKey });
            PrintResult(account);

            Console.WriteLine("\nCreateAccountExample Complete\n");
        }

        private static void PrintResult(FlowAccount flowAccount)
        {
            Console.WriteLine($"Address: {flowAccount.Address.FromByteStringToHex()}");
            Console.WriteLine($"Balance: {flowAccount.Balance}");
            Console.WriteLine($"Contracts: {flowAccount.Contracts.Count}");
            Console.WriteLine($"Keys: {flowAccount.Keys.Count}\n");
        }
    }
}
