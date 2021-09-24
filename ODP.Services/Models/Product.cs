using Newtonsoft.Json;

namespace ODP.Services.Models
{
    public class Product
    {
        [JsonProperty("product_id")]
        public object ProductId { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_url")]
        public string ProductUrl { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("special_price")]
        public double SpecialPrice { get; set; }
    }
}