﻿using Newtonsoft.Json;

namespace Flow.Net.Sdk.Cadence
{
    public class CadenceCapability : Cadence
    {
        public CadenceCapability() { }

        public CadenceCapability(FlowCapabilityValue value)
        {
            Value = value;
        }

        [JsonProperty("type")]
        public override string Type => "Capability";

        [JsonProperty("value")]
        public FlowCapabilityValue Value { get; set; }
    }

    public class FlowCapabilityValue
    {
        [JsonProperty("path")]
        public CadencePath Path { get; set; }

        [JsonProperty("borrowType")]
        public string BorrowType { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }        
    }    
}
