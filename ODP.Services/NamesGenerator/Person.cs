using System.Collections.Generic;

namespace ODP.Services.NamesGenerator
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> MiddleName { get; set; } = new();

        public string Gender { get; set; }
    }
}