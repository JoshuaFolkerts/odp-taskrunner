using ODP.Services.Helpers;
using ODP.Services.Models;
using ODP.Services.NamesGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ODP.Services
{
    public class ContentGeneratorService : IContentGeneratorService
    {
        public List<Customer> GenerateCustomers(int count)
        {
            var customers = new List<Customer>();
            // we need a random variable to select names randomly
            Random rand = new(DateTime.Now.Second);

            // create a new instance of the RandomName class
            RandomName nameGenerator = new(rand);

            // generate 100 random names with up to two middle names
            List<Person> persons = nameGenerator.RandomNames(count, 2);
            List<string> domains = nameGenerator.RandomEmailDomain(count);

            int domainIndex = 0;
            foreach (var person in persons)
            {
                if (domainIndex > count)
                    domainIndex = 0;

                customers.Add(new Customer()
                {
                    Attributes = new Attributes()
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Email = $"{person.FirstName}.{person.LastName}@{domains.ElementAt(domainIndex)}".ToLower(),
                        Vuid = Guid.NewGuid().ToString("N").ToLower(),
                        Gender = person.Gender
                    }
                });
            }

            return customers;
        }

        public ODPGeneric GeneratePageViewEvent(Identifier identifier, DateTime dateTime, string eventValue, Data data) =>
            GenerateGenericEvent(identifier, dateTime, "pageview", eventValue, data);

        public ODPGeneric GeneratePageViewEvent(DateTime dateTime, Customer customer, string url) =>
            this.GeneratePageViewEvent(Identifier.AddVuid(customer.Attributes.Vuid), dateTime, url, new Data());

        public ODPGeneric GeneratePageViewEvent(DateTime dateTime, Customer customer, Product product) =>
            this.GeneratePageViewEvent(Identifier.AddVuid(customer.Attributes.Vuid), dateTime, StringHelper.GetUrlPath(product.ProductUrl), new Data() { Title = product.Name, Page = StringHelper.GetUrlPath(product.ProductUrl) });

        public ODPGeneric GenerateProductEvent(Identifier identifiers, DateTime dateTime, string eventAction, string eventValue, string productId)
        {
            var productEvent = GenerateGenericEvent(identifiers, dateTime, "product", eventValue, new Data()
            {
                ProductId = productId
            });
            return productEvent;
        }

        public ODPGeneric GenerateOrderEvent(Identifier identifiers, DateTime dateTime, string eventAction, Order order)
        {
            var orderEvent = GenerateGenericEvent(identifiers, dateTime, "order", eventAction, new Data()
            {
                Order = order,
                Page = "/checkout/confirm"
            });
            return orderEvent;
        }

        public ODPGeneric GenerateGenericEvent(Identifier identifier, DateTime dateTime, string eventName, Data data) =>
            this.GenerateGenericEvent(identifier, dateTime, eventName, string.Empty, data);

        public ODPGeneric GenerateGenericEvent(Identifier identifier, DateTime dateTime, string eventName, string eventValue, Data data)
        {
            var genericEvent = new ODPGeneric()
            {
                Type = eventName,
                Identifiers = identifier
            };

            if (!string.IsNullOrWhiteSpace(eventValue))
            {
                genericEvent.Action = eventValue;
            }

            genericEvent.Data = data;
            data.Time = dateTime;

            return genericEvent;
        }
    }
}