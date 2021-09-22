using Newtonsoft.Json;
using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class ODPTaskRunner
    {
        public async Task Run()
        {
            string apiKey = string.Empty;
            var appDefaultStartDateTime = DateTime.Now.AddMonths(-1);

            Console.WriteLine("Api key is require in order to find the account to insert data into.");
            Console.Write("Enter apikey: ");
            var apikey = Console.ReadLine();
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

            if (!string.IsNullOrEmpty(apikey) && appDefaultStartDateTime < DateTime.Now)
            {
                // Create customers first
                var customers = JsonConvert.DeserializeObject<List<Customer>>(await ReadJsonFile(Directory.GetCurrentDirectory() + @"\data\customers.json"));

                Console.WriteLine(customers.Count);
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