using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class Identifiers
    {
        [JsonConstructor]
        public Identifiers(
            [JsonProperty("vuid")] string vuid,
            [JsonProperty("email")] string email
        )
        {
            this.Vuid = vuid;
            this.Email = email;
        }

        [JsonProperty("vuid")]
        public string Vuid { get; }

        [JsonProperty("email")]
        public string Email { get; }
    }
}