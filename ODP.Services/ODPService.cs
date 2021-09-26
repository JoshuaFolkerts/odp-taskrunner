using Microsoft.Extensions.Options;
using ODP.Services.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Services
{
    public class ODPService : IODPService
    {
        private readonly IRestClient _restClient;

        private readonly IOptions<AppSettings> _options;

        public ODPService(IOptions<AppSettings> options)
        {
            this._options = options;
            _restClient = new RestClient(_options.Value.RestBaseUrl);
            _restClient.UseSerializer(() => new JsonNetSerializer());
            _restClient.AddDefaultHeader("Authorization", _options.Value.RestAuthToken);
        }

        public async Task<Customer> GetCustomer(string apiKey, string email)
        {
            var request = new RestRequest("/{apiVersion}/profiles", Method.GET)
                .AddHeader("x-api-key", apiKey)
                .AddUrlSegment("apiVersion", this._options.Value.RestBaseVersion)
                .AddQueryParameter("email", email);

            return await _restClient.GetAsync<Customer>(request);
        }

        public async Task<ODPResponse> CreateCustomers(string apiKey, List<Customer> data)
        {
            var request = new RestRequest("/{apiVersion}/profiles", Method.POST)
                .AddHeader("x-api-key", apiKey)
                .AddUrlSegment("apiVersion", this._options.Value.RestBaseVersion)
                .AddJsonBody(data);

            return await _restClient.PostAsync<ODPResponse>(request);
        }

        public async Task<ODPResponse> CreateProducts(string apiKey, List<Product> data)
        {
            var request = new RestRequest("/{apiVersion}/objects/products", Method.POST)
                .AddHeader("x-api-key", apiKey)
                .AddUrlSegment("apiVersion", this._options.Value.RestBaseVersion)
                .AddJsonBody(data);

            return await _restClient.PostAsync<ODPResponse>(request);
        }

        /// <summary>
        /// Adds a single product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<ODPResponse> CreateProducts(string apiKey, string productId, Dictionary<string, string> fields)
        {
            Dictionary<string, string> requestFields = new();
            requestFields.Add("product_id", productId);
            var requestData = new List<Dictionary<string, string>>();
            if (fields.Keys.Any())
            {
                // verify fields
                foreach (var fieldKeyValue in fields)
                {
                    requestFields.TryAdd(fieldKeyValue.Key, fieldKeyValue.Value);
                }
            }

            requestData.Add(fields);

            var request = new RestRequest("/{apiVersion}/objects/products", Method.POST)
                .AddHeader("x-api-key", apiKey)
                .AddUrlSegment("apiVersion", this._options.Value.RestBaseVersion)
                .AddJsonBody(requestData);

            return await _restClient.PostAsync<ODPResponse>(request);
        }

        public async Task<ODPResponse> CreateEvents(string apiKey, List<ODPGeneric> data)
        {
            var request = new RestRequest("/{apiVersion}/events", Method.POST)
                    .AddHeader("x-api-key", apiKey)
                    .AddUrlSegment("apiVersion", this._options.Value.RestBaseVersion)
                    .AddJsonBody(data);

            return await _restClient.PostAsync<ODPResponse>(request);
        }
    }
}