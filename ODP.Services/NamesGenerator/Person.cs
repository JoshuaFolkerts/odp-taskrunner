using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.NamesGenerator
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> MiddleName { get; set; } = new();
    }
}