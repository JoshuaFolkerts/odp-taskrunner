using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class Item
    {
        [JsonConstructor]
        public Item(
            [JsonProperty("product_id")] string productId,
            [JsonProperty("price")] int price,
            [JsonProperty("quantity")] int quantity,
            [JsonProperty("subtotal")] int subtotal
        )
        {
            this.ProductId = productId;
            this.Price = price;
            this.Quantity = quantity;
            this.Subtotal = subtotal;
        }

        [JsonProperty("product_id")]
        public string ProductId { get; }

        [JsonProperty("price")]
        public int Price { get; }

        [JsonProperty("quantity")]
        public int Quantity { get; }

        [JsonProperty("subtotal")]
        public int Subtotal { get; }
    }
}