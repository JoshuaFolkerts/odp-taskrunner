using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ODP.Services.NamesGenerator
{
    public class RandomName
    {
        private Random rand;

        private List<string> male;

        private List<string> female;

        private List<string> last;

        private List<string> emaildomain;

        /// <summary>
        /// Initialises a new instance of the RandomName class.
        /// </summary>
        /// <param name="rand">A Random that is used to pick names</param>
        public RandomName(Random rand)
        {
            this.rand = rand;
            NameList l = new();

            JsonSerializer serializer = new();

            using (StreamReader reader = new(Directory.GetCurrentDirectory() + "/NamesGenerator/names.json"))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                l = serializer.Deserialize<NameList>(jreader);
            }

            this.male = new List<string>(l.Boys);
            this.female = new List<string>(l.Girls);
            this.last = new List<string>(l.Last);
            this.emaildomain = new List<string>(l.EmailDomain);
        }

        /// <summary>
        /// Returns a new random name
        /// </summary>
        /// <param name="sex">The sex of the person to be named. true for male, false for female</param>
        /// <param name="middle">How many middle names do generate</param>
        /// <param name="isInital">Should the middle names be initials or not?</param>
        /// <returns>The random name as a string</returns>
        public Person Generate(Sex sex, int middle = 0, bool isInital = false)
        {
            var person = new Person
            {
                FirstName = sex == Sex.Male ? male[rand.Next(male.Count)] : female[rand.Next(female.Count)], // determines if we should select a name from male or female, and randomly picks
                LastName = this.last[rand.Next(this.last.Count)], // gets the last name
                Gender = sex == Sex.Male ? "M" : "F"
            };

            List<string> middles = new();

            for (int i = 0; i < middle; i++)
            {
                if (isInital)
                {
                    // randomly selects an uppercase letter to use as the inital and appends a dot
                    person.MiddleName.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[rand.Next(0, 25)].ToString() + ".");
                }
                else
                {
                    // randomly selects a name that fits with the sex of the person
                    person.MiddleName.Add(sex == Sex.Male ? male[rand.Next(male.Count)] : female[rand.Next(female.Count)]);
                }
            }

            return person;
        }

        /// <summary>
        /// Generates a list of random names
        /// </summary>
        /// <param name="number">The number of names to be generated</param>
        /// <param name="maxMiddleNames">The maximum number of middle names</param>
        /// <param name="sex">The sex of the names, if null sex is randomised</param>
        /// <param name="initials">Should the middle names have initials, if null this will be randomised</param>
        /// <returns>List of strings of names</returns>
        public List<Person> RandomNames(int number, int maxMiddleNames, Sex? sex = null, bool? initials = null)
        {
            List<Person> names = new();

            for (int i = 0; i < number; i++)
            {
                if (sex != null && initials != null)
                {
                    names.Add(Generate((Sex)sex, rand.Next(0, maxMiddleNames + 1), (bool)initials));
                }
                else if (sex != null)
                {
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate((Sex)sex, rand.Next(0, maxMiddleNames + 1), init));
                }
                else if (initials != null)
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    names.Add(Generate(s, rand.Next(0, maxMiddleNames + 1), (bool)initials));
                }
                else
                {
                    Sex s = (Sex)rand.Next(0, 2);
                    bool init = rand.Next(0, 2) != 0;
                    names.Add(Generate(s, rand.Next(0, maxMiddleNames + 1), init));
                }
            }

            return names;
        }

        public List<string> RandomEmailDomain(int number)
        {
            List<string> domains = new();
            for (int i = 0; i < number; i++)
            {
                string domain = this.emaildomain[rand.Next(this.emaildomain.Count)];
                domains.Add(domain);
            }
            return domains;
        }
    }
}