using Newtonsoft.Json;
using ODP.Services;
using ODP.Services.Helpers;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class ODPTaskRunner
    {
        private readonly IODPService odpService;

        private readonly IContentGeneratorService contentGeneratorService;

        private readonly IJourneyGenerator journeyGenerator;

        private readonly IRandomGenerator randomGenerator;

        private string apiKey = string.Empty;

        private DateTime appDefaultStartDateTime = DateTime.Now.AddMonths(-1);

        private string email = string.Empty;

        private int journey = 0;

        private int counter = 1;

        public ODPTaskRunner(IODPService odpService, IContentGeneratorService contentGeneratorService, IJourneyGenerator journeyGenerator, IRandomGenerator randomGenerator)
        {
            this.odpService = odpService;
            this.contentGeneratorService = contentGeneratorService;
            this.journeyGenerator = journeyGenerator;
            this.randomGenerator = randomGenerator;
        }

        public async Task Run()
        {
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

            Console.WriteLine("Product Type");
            Console.WriteLine("0 - Journey Only");
            Console.WriteLine("1 = \"Content Cloud\"");
            Console.WriteLine("2 = \"Content Cloud and Commerce\"");
            Console.Write("Choose Product Type option: (ie. 0): ");
            var productTypes = Console.ReadLine();

            if (productTypes != "0")
            {
                Console.Write($"Number of events or customers to generate (1-100): ");
                counter = Convert.ToInt32(Console.ReadLine());
            }

            if (productTypes == "0")
            {
                Console.WriteLine("Journey Types:");
                Console.WriteLine("1 - Association Script - CMS / Forms / Experiments / Email");
                Console.WriteLine("2 - CMS / Search / Form / Experimentation / Email: ");
                Console.WriteLine("3 - Commerce (multi-day) / Experiment / Form / Add To Cart / Triggered Email / In-store: ");
                Console.WriteLine("4 - Commerce / Experiment / Form / Add To Cart / Triggered Email / Purchase: ");
                Console.Write("Enter Journey Type: ");
                var selectedJourney = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(selectedJourney))
                {
                    if (!int.TryParse(selectedJourney, out journey))
                    {
                        Console.WriteLine("Please select a value 0, 1, 2, 3, 4");
                        Console.ReadLine();
                    }
                }

                if (journey != 0)
                {
                    Console.WriteLine($"Enter your email address (used for journey): ");
                    var enteredEmail = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(enteredEmail))
                    {
                        email = enteredEmail;
                    }
                }
            }
            if (!string.IsNullOrEmpty(apiKey) && appDefaultStartDateTime < DateTime.Now && !string.IsNullOrWhiteSpace(productTypes))
            {
                // force max 100
                if (counter > 100)
                {
                    counter = 100;
                }
                // Get customers
                var customers = this.contentGeneratorService.GenerateCustomers(counter);

                // Get products and randomize them
                var products = JsonConvert.DeserializeObject<List<Product>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\clothing-products.json"))
                    .OrderBy(x => Guid.NewGuid())
                    .ToList();

                // Get urls for products
                var paths = products
                    .Where(x => !string.IsNullOrWhiteSpace(x.ProductUrl))
                    .Select(x => StringHelper.GetUrlPath(x.ProductUrl));

                if ((journey > 0 && !string.IsNullOrWhiteSpace(email)) || productTypes != "0")
                {
                    var customerResponse = await this.odpService.CreateCustomers(apiKey, customers);
                    WriteErrors(customerResponse, $"Saving customers: {customerResponse.Status} - {customerResponse.Title}");
                }

                // Push Customers
                if (productTypes != "0")
                {
                    var productResponse = await this.odpService.CreateProducts(apiKey, products.ToList());
                    WriteErrors(productResponse, $"Saving products: {productResponse.Status} - {productResponse.Title}");
                    if (productResponse.IsValid)
                    {
                        switch (productTypes)
                        {
                            case "1":
                                var contentEvents = this.randomGenerator.ExecuteCloudContent(appDefaultStartDateTime, products, customers, counter);
                                var eventResponse = await this.odpService.CreateEvents(apiKey, contentEvents);
                                this.WriteErrors(eventResponse, $"Saving content events: {eventResponse.Status} - {eventResponse.Title}");
                                break;

                            case "2":
                                var contentCommerceEvents = this.randomGenerator.ExecuteCloudAndCloundContent(appDefaultStartDateTime, products, customers, counter);
                                var commerceEventResponse = await this.odpService.CreateEvents(apiKey, contentCommerceEvents);
                                this.WriteErrors(commerceEventResponse, $"Saving content & commerce events: {commerceEventResponse.Status} - {commerceEventResponse.Title}");
                                break;
                        }
                    }
                }
                // Push Journey
                if (journey > 0)
                {
                    var customer = customers.FirstOrDefault();
                    await CreateJourneys(customer);
                }

                Console.WriteLine("Import Completed: Press any key to stop");
                Console.ReadKey();
            }
        }

        private async Task CreateJourneys(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var foundCustomer = await this.odpService.GetCustomer(apiKey, email);
                if (foundCustomer != null)
                {
                    customer = foundCustomer;
                }
                else
                {
                    var response = await this.odpService.CreateCustomers(
                        apiKey,
                        new List<Customer>() {
                                    new Customer() {
                                        Attributes = new Attributes() {
                                            Email = email,
                                            Vuid = Guid.NewGuid().ToString("N").ToLower()
                                        }
                                    }
                        });

                    if (response.Status == 202)
                    {
                        customer = await this.odpService.GetCustomer(apiKey, email);
                    }
                }
            }
            if (customer != null)
            {
                var journeyEvents = new List<ODPGeneric>();
                switch (journey)
                {
                    case 1:
                        journeyEvents = this.journeyGenerator.GenerateAssociationCMSScript(customer, appDefaultStartDateTime);
                        break;

                    case 2:
                        journeyEvents = this.journeyGenerator.GenerateCMSScript(customer, appDefaultStartDateTime);
                        break;

                    case 3:
                        journeyEvents = this.journeyGenerator.GenerateCommerceMultiDayScript(customer, appDefaultStartDateTime);
                        break;

                    case 4:
                        journeyEvents = this.journeyGenerator.GenerateCommerceScript(customer, appDefaultStartDateTime);
                        break;
                }
                if (journeyEvents.Any())
                {
                    var journeyResponse = await this.odpService.CreateEvents(apiKey, journeyEvents);
                    WriteErrors(journeyResponse, "Imported Journey Completed");
                }
            }
            else
            {
                Console.WriteLine("ERROR retrieving customer");
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

        private void WriteErrors(ODPResponse response, string successMessage = "")
        {
            if (response.Details != null && response.Details.Invalids.Any())
            {
                var red = Console.ForegroundColor = ConsoleColor.Red;
                foreach (var detail in response.Details.Invalids)
                {
                    Console.WriteLine(detail.ToString(), red);
                }
            }
            else if (!string.IsNullOrWhiteSpace(successMessage))
            {
                Console.WriteLine(successMessage);
            }
        }
    }
}