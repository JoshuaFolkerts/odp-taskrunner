using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; } = new();
    }
}