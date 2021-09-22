using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Models
{
    public class Data
    {
        [JsonConstructor]
        public Data(
            [JsonProperty("page")] string page,
            [JsonProperty("ts")] int ts,
            [JsonProperty("product_id")] string productId,
            [JsonProperty("order")] Order order
        )
        {
            this.Page = page;
            this.Ts = ts;
            this.ProductId = productId;
            this.Order = order;
        }

        [JsonProperty("page")]
        public string Page { get; }

        [JsonProperty("ts")]
        public int Ts { get; }

        [JsonProperty("product_id")]
        public string ProductId { get; }

        [JsonProperty("order")]
        public Order Order { get; }
    }
}