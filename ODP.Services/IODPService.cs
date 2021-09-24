using ODP.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Services
{
    public interface IODPService
    {
        Task<Customer> GetCustomer(string apiKey, string email);

        Task<ODPResponse> CreateCustomers(string apiKey, List<Customer> data);

        Task<ODPResponse> CreateProducts(string apiKey, List<Product> data);

        Task<ODPResponse> CreateProducts(string apiKey, string productId, Dictionary<string, string> fields);

        Task<ODPResponse> CreateEvents(string apiKey, List<ODPGeneric> data);
    }
}