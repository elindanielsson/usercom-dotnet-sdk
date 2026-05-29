using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UserCom.Model.Users
{
    public class UserProductEvent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("client")]
        public int Client { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("custom_data")]
        public Dictionary<string, object> CustomData { get; set; }
    }
}