using Newtonsoft.Json;
using System;

namespace ODP.Services.Models
{
    public class ODPResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("detail")]
        public Details Details { get; set; }
    }
}