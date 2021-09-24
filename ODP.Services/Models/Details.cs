using Newtonsoft.Json;
using System.Collections.Generic;

namespace ODP.Services.Models
{
    public class Invalid
    {
        [JsonProperty("invalids")]
        public List<object> Invalids { get; set; } = new();
    }
}