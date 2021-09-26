using ODP.Services;
using ODP.Services.Helpers;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class RandomGenerator : IRandomGenerator
    {
        private IContentGeneratorService contentGeneratorService;

        private List<Product> products = new();

        private List<Customer> customers = new();

        private DateTime dateTime;

        private Random random;

        public RandomGenerator(IContentGeneratorService contentGeneratorService)
        {
            this.contentGeneratorService = contentGeneratorService;
        }

        public List<ODPGeneric> ExecuteCloudContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 30)
        {
            this.dateTime = dateTime;
            this.products = products;
            this.customers = customers;
            this.random = new Random();

            var list = new List<ODPGeneric>();
            for (int i = 0; i < count; i++)
            {
                list.AddRange(this.GenerateRandomPageView());
                list.AddRange(this.GenerateRandomFormSubmit());
                list.AddRange(this.GenerateRandomEvents());
            }
            return list.Take(count).ToList();
        }

        public List<ODPGeneric> ExecuteCloudAndCloundContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 100)
        {
            this.dateTime = dateTime;
            this.products = products;
            this.customers = customers;
            this.random = new Random();

            var list = new List<ODPGeneric>();
            for (int i = 0; i < count; i++)
            {
                list.AddRange(this.GenerateRandomPageView());
            }
            return list.Take(count).ToList();
        }

        private int GetRandomInt(int max) =>
             random.Next(1, max + 1);

        private int GetRandomIntInRange(int min, int max) =>
            random.Next(min, max);

        private DateTime GetRandomDate()
        {
            int range = (DateTime.Today - dateTime).Days;
            return dateTime.AddDays(random.Next(range));
        }

        public List<ODPGeneric> GenerateRandomPageView()
        {
            int count = GetRandomInt(5);
            var list = new List<ODPGeneric>();

            for (int i = 0; i <= count; i++)
            {
                var productIndex = GetRandomIntInRange(0, products.Count - 1);
                var customerIndex = GetRandomIntInRange(0, customers.Count - 1);

                var product = products.ElementAt(productIndex);
                var customer = customers.ElementAt(customerIndex);

                var dt = GetRandomDate();
                list.Add(contentGeneratorService.GeneratePageViewEvent(dt, customer, product));
            }
            return list;
        }

        public List<ODPGeneric> GenerateRandomFormSubmit()
        {
            int count = GetRandomInt(5);

            var list = new List<ODPGeneric>();
            for (int i = 0; i <= count; i++)
            {
                var productIndex = GetRandomIntInRange(0, products.Count - 1);
                var customerIndex = GetRandomIntInRange(0, customers.Count - 1);

                var product = products.ElementAt(productIndex);
                var customer = customers.ElementAt(customerIndex);

                var dt = GetRandomDate();

                list.Add(contentGeneratorService.GeneratePageViewEvent(dt, customer, product));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(
                    Identifier.AddVuid(customer.Attributes.Vuid),
                    dt,
                    "Submit Form",
                    "Find Out More", new Data()
                    {
                        Title = product.Name,
                        Page = StringHelper.GetUrlPath(product.ProductUrl)
                    }));
            }
            return list;
        }

        public List<ODPGeneric> GenerateRandomEvents()
        {
            int count = GetRandomInt(5);

            var list = new List<ODPGeneric>();
            for (int i = 0; i <= count; i++)
            {
                var productIndex = GetRandomIntInRange(0, products.Count - 1);
                var customerIndex = GetRandomIntInRange(0, customers.Count - 1);

                var product = products.ElementAt(productIndex);
                var customer = customers.ElementAt(customerIndex);

                var dt = GetRandomDate();

                var searchTerms = product.Name.Split(new char[] { ' ' });
                var searchTerm = searchTerms.ElementAt(GetRandomIntInRange(0, searchTerms.Length));

                var standardData = new Data() { Page = StringHelper.GetUrlPath(product.ProductUrl), Title = product.Name };
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "search", searchTerm, new Data() { Page = "/search", Title = "Search" }));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "bestbetselected", "best bet selected", standardData));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "PageCategories", searchTerm, standardData));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "Experiment-Decision", "ServicesEngagement - LimitedTimeOffer", standardData));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "Experiment-Conversion", "ServicesEngagement - LimitedTimeOffer", standardData));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "email", "Services Customer Stories", standardData));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "email", "Internal - Demo Sign Up", standardData));
            }
            return list;
        }
    }
}