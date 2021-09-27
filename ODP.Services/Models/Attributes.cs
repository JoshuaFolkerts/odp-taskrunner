using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class Attributes
    {
        public Attributes()
        {
        }

        [JsonProperty("vuid")]
        public string Vuid { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}