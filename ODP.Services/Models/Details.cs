using Newtonsoft.Json;
using System.Collections.Generic;

namespace ODP.Services.Models
{
    public class Details
    {
        [JsonProperty("invalids")]
        public List<object> Invalids { get; set; }
    }
}