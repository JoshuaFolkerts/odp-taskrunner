using Newtonsoft.Json;
using System.Collections.Generic;

namespace ODP.Services.Models
{
    public class Order
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("subtotal")]
        public double Subtotal { get; set; }

        [JsonProperty("tax")]
        public double Tax { get; set; }

        [JsonProperty("shipping")]
        public double Shipping { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; } = new();
    }
}