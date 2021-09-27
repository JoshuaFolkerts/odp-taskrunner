using Newtonsoft.Json;
using ODP.Services;
using ODP.Services.Helpers;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class ODPTaskRunner
    {
        private readonly IODPService odpService;

        private readonly IContentGeneratorService contentGeneratorService;

        private readonly IJourneyGenerator journeyGenerator;

        private readonly IRandomGenerator randomGenerator;

        private JourneyType journeyType = 0;

        private ProductType productType = 0;

        private int counter = 1;

        private string apiKey = string.Empty;

        private string email = string.Empty;

        private string odpPath = string.Empty;

        private DateTime appDefaultStartDateTime = DateTime.Now.AddMonths(-1);

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

            apiKey = ReadLine.Read("Enter apiKey: ");
            Console.WriteLine();

            odpPath = ReadLine.Read("Enter remote filePath - press enter for No: ");

            var dt = ReadLine.Read($"Enter Datetime - press enter for default ({appDefaultStartDateTime.ToShortDateString()}): ", appDefaultStartDateTime.ToShortDateString());
            DateTime userDateTime;
            while (!DateTime.TryParse(dt, out userDateTime) || userDateTime > DateTime.Now.AddDays(-2))
            {
                dt = ReadLine.Read($"Enter Datetime - press enter for default ({appDefaultStartDateTime.ToShortDateString()}): ", appDefaultStartDateTime.ToShortDateString());
            }
            appDefaultStartDateTime = userDateTime;
            Console.WriteLine();
            if (this.odpPath.HasValue())
            {
                this.ReadRemoteFile();
            }
            WriteWrappedHeader("Product Type");
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine($"{i} - {((ProductType)i).GetDescription()}");
            }
            var userSelectedProductType = ReadLine.Read("Choose Product Type option: (ie. 0):");

            int productType2;
            while (!int.TryParse(userSelectedProductType, out productType2))
            {
                Console.Write("This is not valid input. Please enter a value 0-2: ");
                userSelectedProductType = Console.ReadLine();
            }
            productType = (ProductType)productType2;

            if (productType != ProductType.Journey)
            {
                Console.Write($"Number of events or customers to generate (1-100): ");
                counter = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                WriteWrappedHeader("Journey Types:");
                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine($"{i} - {((JourneyType)i).GetDescription()}");
                }
                var userSelectedJourney = ReadLine.Read("Enter Journey Type: ");
                int userJourney2;
                while (!int.TryParse(userSelectedJourney, out userJourney2) && userJourney2 > 4)
                {
                    Console.Write("This is not valid input. Please enter a value 1-4: ");
                    userSelectedJourney = Console.ReadLine();
                }
                journeyType = (JourneyType)userJourney2;

                if (journeyType != 0)
                {
                    var enteredEmail = ReadLine.Read("Enter your email address (used for journey): ");
                    if (!string.IsNullOrWhiteSpace(enteredEmail))
                    {
                        email = enteredEmail;
                    }
                }
                counter = 1;
            }

            if (apiKey.HasValue() && appDefaultStartDateTime < DateTime.Now)
            {
                // force max 100
                if (counter > 100)
                {
                    counter = 100;
                }
                // Get customers
                var customers = this.contentGeneratorService.GenerateCustomers(counter);
                if (productType != ProductType.Journey)
                {
                    // Get products and randomize them
                    var products = JsonConvert.DeserializeObject<List<Product>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\clothing-products.json"))
                        .OrderBy(x => Guid.NewGuid())
                        .ToList();

                    // Get urls for products
                    var paths = products
                        .Where(x => !string.IsNullOrWhiteSpace(x.ProductUrl))
                        .Select(x => StringHelper.GetUrlPath(x.ProductUrl));

                    // Create contact for journey since we need customer;
                    var customerResponse = await this.odpService.CreateCustomers(apiKey, customers);
                    WriteErrors(customerResponse, $"Saved {counter} customers");

                    // Push Customers
                    if (productType != ProductType.Journey)
                    {
                        var productResponse = await this.odpService.CreateProducts(apiKey, products.ToList());
                        WriteErrors(productResponse, $"Saved {counter} products");
                        if (productResponse.IsValid)
                        {
                            switch (productType)
                            {
                                case ProductType.ContentCloud:
                                    var contentEvents = this.randomGenerator.ExecuteCloudContent(appDefaultStartDateTime, products, customers, counter);
                                    var eventResponse = await this.odpService.CreateEvents(apiKey, contentEvents);
                                    this.WriteErrors(eventResponse, $"Saved {counter} content events");
                                    break;

                                case ProductType.ContentCommerce:
                                    var contentCommerceEvents = this.randomGenerator.ExecuteCloudAndCloundContent(appDefaultStartDateTime, products, customers, counter);
                                    var commerceEventResponse = await this.odpService.CreateEvents(apiKey, contentCommerceEvents);
                                    this.WriteErrors(commerceEventResponse, $"Saved {counter} content & commerce events");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    var customer = customers.FirstOrDefault();
                    await CreateJourneys(customer);
                }
                WriteWrappedHeader("Import Completed: It will take a bit of time for the items to show up in the interface but you could use GraphQL to query the data in realtime.", wrapperChar: ' ', ConsoleColor.Yellow); ;
            }
        }

        private async Task ReadRemoteFile()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(this.odpPath);
                var genericObjects = JsonConvert.DeserializeObject<List<ODPGeneric>>(json);
                var status = await this.odpService.CreateEvents(apiKey, genericObjects);
                this.WriteErrors(status, "Events saved successfully");
                Environment.Exit(0);
            }
        }

        private async Task CreateJourneys(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var foundCustomer = this.odpService.GetCustomer(apiKey, email);
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
                        customer = this.odpService.GetCustomer(apiKey, email);
                    }
                }
            }
            if (customer.Attributes.Vuid.HasValue())
            {
                var journeyEvents = new List<ODPGeneric>();
                switch (journeyType)
                {
                    case JourneyType.AssociationScript:
                        journeyEvents = this.journeyGenerator.GenerateAssociationCMSScript(customer, appDefaultStartDateTime);
                        break;

                    case JourneyType.CMS:
                        journeyEvents = this.journeyGenerator.GenerateCMSScript(customer, appDefaultStartDateTime);
                        break;

                    case JourneyType.CommerceMultiDay:
                        journeyEvents = this.journeyGenerator.GenerateCommerceMultiDayScript(customer, appDefaultStartDateTime);
                        break;

                    case JourneyType.Commerce:
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
                foreach (var detail in response.Details.Invalids)
                {
                    WriteColorLine(detail.ToString(), ConsoleColor.Red);
                }
            }
            else if (!string.IsNullOrWhiteSpace(successMessage))
            {
                WriteColorLine(successMessage, ConsoleColor.Green);
            }
        }

        #region Adding in color helpers

        //https://weblog.west-wind.com/posts/2020/Jul/10/A-NET-Console-Color-Helper

        public static void WriteColorLine(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                if (color == oldColor)
                    Console.WriteLine(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.WriteLine(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
                Console.WriteLine(text);
        }

        public static void WriteWrappedHeader(string headerText,
                                       char wrapperChar = '-',
                                       ConsoleColor headerColor = ConsoleColor.Yellow,
                                       ConsoleColor dashColor = ConsoleColor.DarkGray)
        {
            if (string.IsNullOrEmpty(headerText))
                return;

            string line = new(wrapperChar, headerText.Length);

            WriteColorLine(line, dashColor);
            WriteColorLine(headerText, headerColor);
            WriteColorLine(line, dashColor);
        }

        public static void WriteInfo(string text) =>
            WriteColorLine(text, ConsoleColor.DarkCyan);

        #endregion Adding in color helpers
    }
}