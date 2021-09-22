using ODP.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Services
{
    public interface IODPService
    {
        Task<ODPResponse> CreateCustomers(List<ODPRoot> data);

        Task<ODPResponse> CreateProducts(List<Product> data);

        Task<ODPResponse> CreateProducts(string productId, Dictionary<string, string> fields);

        Task<ODPResponse> CreateEvents(List<ODPRoot> data);
    }
}