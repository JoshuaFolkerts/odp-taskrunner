using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class Identifier
    {
        [JsonProperty("vuid", NullValueHandling = NullValueHandling.Ignore)]
        public string Vuid { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        public static Identifier AddVuid(string vuid) => new()
        {
            Vuid = vuid
        };

        public static Identifier AddEmail(string email) => new()
        {
            Email = email
        };
    }
}