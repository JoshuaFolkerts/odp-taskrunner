using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class Customer
    {
        public Customer()
        {
        }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; } = new();
    }
}