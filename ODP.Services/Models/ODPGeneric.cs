using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class ODPGeneric
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        [JsonProperty("identifiers")]
        public Identifier Identifiers { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; } = new();
    }
}