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
        [JsonConstructor]
        public Attributes(
            [JsonProperty("vuid")] string vuid,
            [JsonProperty("email")] string email,
            [JsonProperty("first_name")] string firstName,
            [JsonProperty("last_name")] string lastName)
        {
            this.Vuid = vuid;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        [JsonProperty("vuid")]
        public string Vuid { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("first_name")]
        public string FirstName { get; }

        [JsonProperty("last_name")]
        public string LastName { get; }
    }

    public class Customer
    {
        [JsonConstructor]
        public Customer([JsonProperty("attributes")] Attributes attributes)
        {
            this.Attributes = attributes;
        }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; }
    }
}