using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODP.Services.Models
{
    public class ODPResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("detail")]
        public Invalid Details { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsValid =>
            this.Status != 0 && this.Status == 202;
    }
}