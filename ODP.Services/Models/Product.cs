using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class Product
    {
        [JsonConstructor]
        public Product(
            [JsonProperty("product_id")] object productId,
            [JsonProperty("image_url")] string imageUrl,
            [JsonProperty("name")] string name,
            [JsonProperty("product_url")] string productUrl,
            [JsonProperty("price")] double price,
            [JsonProperty("special_price")] double specialPrice)
        {
            this.ProductId = productId;
            this.ImageUrl = imageUrl;
            this.Name = name;
            this.ProductUrl = productUrl;
            this.Price = price;
            this.SpecialPrice = specialPrice;
        }

        [JsonProperty("product_id")]
        public object ProductId { get; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("product_url")]
        public string ProductUrl { get; }

        [JsonProperty("price")]
        public double Price { get; }

        [JsonProperty("special_price")]
        public double SpecialPrice { get; }
    }
}