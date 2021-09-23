using Newtonsoft.Json;
using ODP.Services;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class ODPTaskRunner
    {
        private readonly IODPService odpService;

        private readonly IContentGeneratorService contentGeneratorService;

        public ODPTaskRunner(IODPService odpService, IContentGeneratorService contentGeneratorService)
        {
            this.odpService = odpService;
            this.contentGeneratorService = contentGeneratorService;
        }

        public async Task Run()
        {
            string apiKey = string.Empty;
            var appDefaultStartDateTime = DateTime.Now.AddMonths(-1);

            Console.WriteLine("Api key is require in order to find the account to insert data into.");
            Console.Write("Enter apikey: ");
            apiKey = Console.ReadLine();
            Console.Write($"Enter Datetime - press enter for default ({appDefaultStartDateTime.ToShortDateString()}): ");
            var dt = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dt))
            {
                var userDateTime = appDefaultStartDateTime;
                while (!DateTime.TryParse(dt, out userDateTime) || userDateTime > DateTime.Now)
                {
                    Console.WriteLine("Please specify a date less than current Date and Time");
                    dt = Console.ReadLine();
                }
            }

            if (!string.IsNullOrEmpty(apiKey) && appDefaultStartDateTime < DateTime.Now)
            {
                // Create customers first
                //var customers = JsonConvert.DeserializeObject<List<Customer>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\customers.json"));
                //var response = await this.odpService.CreateCustomers(apiKey, customers);

                //Console.WriteLine($"Customer Count: {customers.Count}");
                //Console.WriteLine($"Customer Title: {response.Title}");
                //Console.WriteLine($"Customer Status: {response.Status}");
                //Console.WriteLine($"----------------------------------");

                //var products = JsonConvert.DeserializeObject<List<Product>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\products.json"));
                //var productResponse = await this.odpService.CreateProducts(apiKey, products);
                //Console.WriteLine($"Products Count: {products.Count}");
                //Console.WriteLine($"Products Title: {productResponse.Title}");
                //Console.WriteLine($"Products Status: {productResponse.Status}");
                //Console.WriteLine($"----------------------------------");

                //var events = JsonConvert.DeserializeObject<List<ODPRoot>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\misc-events.json"));
                //var eventsResponse = await this.odpService.CreateEvents(apiKey, events);
                //Console.WriteLine($"Events Count: {events.Count}");
                //Console.WriteLine($"Events Title: {eventsResponse.Title}");
                //Console.WriteLine($"Events Status: {eventsResponse.Status}");
                //Console.WriteLine($"----------------------------------");

                var customers = this.contentGeneratorService.GenerateCustomers(100);

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer FirstName: {customer.Attributes.FirstName}");
                    Console.WriteLine($"Customer LastName: {customer.Attributes.LastName}");
                    Console.WriteLine($"Customer Email: {customer.Attributes.Email}");
                    Console.WriteLine($"Customer VUID: {customer.Attributes.Vuid}");
                    Console.WriteLine($"----------------------------------");
                }

                Console.WriteLine("Press any key to stop");
                Console.ReadKey();
            }
        }

        private async Task<string> ReadJsonFile(string file)
        {
            string json = string.Empty;
            if (File.Exists(file))
            {
                json = await File.ReadAllTextAsync(file);
            }
            return json;
        }
    }
}