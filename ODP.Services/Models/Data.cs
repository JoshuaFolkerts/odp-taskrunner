using Newtonsoft.Json;
using ODP.Services.Helpers;
using System;

namespace ODP.Services.Models
{
    public class Data
    {
        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(JsonUnixTimeConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductId { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public Order Order { get; set; }
    }
}