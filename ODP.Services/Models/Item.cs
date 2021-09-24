using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class Item
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("subtotal")]
        public double Subtotal { get; set; }
    }
}