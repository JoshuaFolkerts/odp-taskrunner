using ODP.Services;
using ODP.Services.Helpers;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ODPContentRunner
{
    public class RandomGenerator : IRandomGenerator
    {
        private IContentGeneratorService contentGeneratorService;

        private List<Product> products = new();

        private List<Customer> customers = new();

        private DateTime dateTime;

        private Random random;

        private string[] EmailForms = new[] { "Services Customer Stories", "Internal - Demo Sign Up", "Contact Us", "Request Information" };

        public RandomGenerator(IContentGeneratorService contentGeneratorService)
        {
            this.contentGeneratorService = contentGeneratorService;
            this.random = new Random();
        }

        public List<ODPGeneric> ExecuteCloudContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 30)
        {
            this.dateTime = dateTime;
            this.products = products;
            this.customers = customers;

            var list = new List<ODPGeneric>();
            for (int i = 0; i < count; i++)
            {
                list.AddRange(this.GenerateRandomPageView());
                list.AddRange(this.GenerateRandomFormSubmit());
                list.AddRange(this.GenerateRandomEvents());
            }
            return list.ToList();
        }

        public List<ODPGeneric> ExecuteCloudAndCloundContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 100)
        {
            this.dateTime = dateTime;
            this.products = products;
            this.customers = customers;

            var list = new List<ODPGeneric>();
            for (int i = 0; i < count; i++)
            {
                list.AddRange(this.GenerateRandomPageView());
                list.AddRange(this.GenerateRandomFormSubmit());
                list.AddRange(this.GenerateRandomEvents());
                list.AddRange(this.GenerateCartEvents());
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
                var product = GetRandomProduct();
                var customer = GetRandomCustomer();
                var dt = GetRandomDate();

                list.Add(contentGeneratorService.GeneratePageViewEvent(dt, customer, product));
            }

            return list;
        }

        public List<ODPGeneric> GenerateRandomFormSubmit()
        {
            var list = new List<ODPGeneric>();

            var product = GetRandomProduct();
            var customer = GetRandomCustomer();

            var dt = GetRandomDate();

            list.Add(contentGeneratorService.GeneratePageViewEvent(dt, customer, product));
            list.Add(this.contentGeneratorService.GenerateGenericEvent(Identifier.AddVuid(customer.Attributes.Vuid), dt, "Submit Form", "Find Out More", new Data() { Title = product.Name, Page = StringHelper.GetUrlPath(product.ProductUrl) }));

            return list;
        }

        public List<ODPGeneric> GenerateRandomEvents()
        {
            var list = new List<ODPGeneric>();
            var productIndex = GetRandomIntInRange(0, products.Count - 1);
            var customerIndex = GetRandomIntInRange(0, customers.Count - 1);

            var product = products.ElementAt(productIndex);
            var customer = customers.ElementAt(customerIndex);

            var dt = GetRandomDate();

            var searchTerms = product.Name.Split(new char[] { ' ' });
            var searchTerm = searchTerms.ElementAt(GetRandomIntInRange(0, searchTerms.Length));

            var standardData = new Data() { Page = StringHelper.GetUrlPath(product.ProductUrl), Title = product.Name };
            var i = GetRandomIntInRange(1, 7);
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomInt(5)), "search", searchTerm, new Data() { Page = "/search", Title = "Search" })); ;
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomIntInRange(6, 10)), "bestbetselected", "best bet selected", standardData));
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomIntInRange(11, 15)), "PageCategories", searchTerm, standardData));
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomIntInRange(16, 20)), "Experiment-Decision", "ServicesEngagement - LimitedTimeOffer", standardData));
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomIntInRange(21, 25)), "Experiment-Conversion", "ServicesEngagement - LimitedTimeOffer", standardData));
            list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(GetRandomIntInRange(31, 35)), "email", EmailForms.ElementAt(GetRandomIntInRange(0, 3)), standardData));
            return list.Take(i).ToList();
        }

        private IEnumerable<ODPGeneric> GenerateCartEvents()
        {
            var list = new List<ODPGeneric>();
            var dt = GetRandomDate();
            var customerIndex = GetRandomIntInRange(0, customers.Count - 1);
            var customer = customers.ElementAt(customerIndex);
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);

            var shouldConvertToOrder = GetRandomIntInRange(0, 1);
            if (shouldConvertToOrder == 1)
            {
                var productList = new List<Product>();

                int numProducts = GetRandomIntInRange(1, 10);

                for (int i = 0; i <= numProducts; i++)
                {
                    var p = GetRandomProduct();
                    productList.Add(p);
                    dt = dt.AddMinutes(1);
                    list.Add(this.contentGeneratorService.GenerateProductEvent(vuid, dt, "detail", p.Name, p.ProductId.ToString()));
                    dt = dt.AddMinutes(1);
                    list.Add(this.contentGeneratorService.GenerateProductEvent(vuid, dt, "add_to_card", p.Name, p.ProductId.ToString()));
                    dt = dt.AddMinutes(1);
                    list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt, "pageview", new Data() { Page = "/cart", Title = "Cart" }));
                }

                var order = new Order
                {
                    Items = productList.Select(x => new Item()
                    {
                        ProductId = x.ProductId.ToString(),
                        Discount = 5.00,
                        Price = x.Price,
                        Quantity = GetRandomIntInRange(1, 3),
                        Subtotal = GetRandomIntInRange(1, 3) * x.Price // will not match but its just dummy data
                    }).ToList()
                };
                order.Subtotal = order.Items.Sum(x => x.Subtotal);
                order.Shipping = 10;
                order.Tax = order.Subtotal * .06;
                order.Discount = order.Items.Count * 5;
                order.Total = order.Subtotal + 10 + order.Tax + order.Discount;
                order.OrderId = GetRandomIntInRange(8675, 100000).ToString();

                list.Add(this.contentGeneratorService.GenerateOrderEvent(vuid, dt, "purchase", order));
            }
            else
            {
                var productIndex = GetRandomIntInRange(0, products.Count - 1);
                var product = products.ElementAt(productIndex);
                var standardData = new Data() { Page = StringHelper.GetUrlPath(product.ProductUrl), Title = product.Name };
                list.Add(this.contentGeneratorService.GenerateProductEvent(vuid, dt, "detail", product.Name, product.ProductId.ToString()));
                list.Add(this.contentGeneratorService.GenerateProductEvent(vuid, dt.AddMinutes(1), "add_to_card", product.Name, product.ProductId.ToString()));
                list.Add(this.contentGeneratorService.GenerateGenericEvent(vuid, dt.AddMinutes(2), "pageview", new Data() { Page = "/cart", Title = "Cart" }));
            }
            return list;
        }

        private Product GetRandomProduct() =>
        products.ElementAt(GetRandomIntInRange(0, products.Count - 1));

        private Customer GetRandomCustomer() =>
            customers.ElementAt(GetRandomIntInRange(0, customers.Count - 1));
    }
}