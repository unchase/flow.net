﻿using Google.Protobuf;
using System.Collections.Generic;

namespace Flow.Net.Sdk
{
    public class FlowAccount
    {
        public FlowAccount()
        {
            Keys = new List<FlowAccountKey>();
            Contracts = new List<FlowContract>();
        }

        public ByteString Address { get; set; }
        public ByteString Code { get; set; }
        public decimal Balance { get; set; }
        public IList<FlowAccountKey> Keys { get; set; }
        public IList<FlowContract> Contracts { get; set; }
    }
}
