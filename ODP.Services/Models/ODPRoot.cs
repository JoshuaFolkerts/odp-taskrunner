using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class ODPRoot
    {
        [JsonConstructor]
        public ODPRoot(
            [JsonProperty("type")] string type,
            [JsonProperty("action")] string action,
            [JsonProperty("identifiers")] Identifiers identifiers,
            [JsonProperty("data")] Data data
        )
        {
            this.Type = type;
            this.Action = action;
            this.Identifiers = identifiers;
            this.Data = data;
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("action")]
        public string Action { get; }

        [JsonProperty("identifiers")]
        public Identifiers Identifiers { get; }

        [JsonProperty("data")]
        public Data Data { get; }
    }
}