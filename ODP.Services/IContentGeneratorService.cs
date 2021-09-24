using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Services
{
    public interface IContentGeneratorService
    {
        Task<List<Customer>> GenerateCustomers(int count);

        ODPGeneric GeneratePageViewEvent(Identifier identifiers, DateTime dateTime, string eventValue, Data data);

        ODPGeneric GeneratePageViewEvent(DateTime dateTime, Customer customer, string url);

        ODPGeneric GeneratePageViewEvent(DateTime dateTime, Customer customer, Product product);

        ODPGeneric GenerateProductEvent(Identifier identifiers, DateTime dateTime, string eventAction, string eventValue, string productId);

        ODPGeneric GenerateOrderEvent(Identifier identifiers, DateTime dateTime, string eventAction, Order order);

        ODPGeneric GenerateGenericEvent(Identifier identifier, DateTime dateTime, string eventName, Data data);

        ODPGeneric GenerateGenericEvent(Identifier identifier, DateTime dateTime, string eventName, string eventValue, Data data);
    }
}