using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class Order
    {
        [JsonConstructor]
        public Order(
            [JsonProperty("order_id")] string orderId,
            [JsonProperty("subtotal")] int subtotal,
            [JsonProperty("tax")] double tax,
            [JsonProperty("shipping")] double shipping,
            [JsonProperty("total")] double total,
            [JsonProperty("items")] List<Item> items
        )
        {
            this.OrderId = orderId;
            this.Subtotal = subtotal;
            this.Tax = tax;
            this.Shipping = shipping;
            this.Total = total;
            this.Items = items;
        }

        [JsonProperty("order_id")]
        public string OrderId { get; }

        [JsonProperty("subtotal")]
        public int Subtotal { get; }

        [JsonProperty("tax")]
        public double Tax { get; }

        [JsonProperty("shipping")]
        public double Shipping { get; }

        [JsonProperty("total")]
        public double Total { get; }

        [JsonProperty("items")]
        public IReadOnlyList<Item> Items { get; }
    }
}