using ODP.Services.Models;
using ODP.Services.NamesGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Vuid = Guid.NewGuid().ToString("N").ToUpper()
                    }
                });
            }

            return customers;
        }
    }
}