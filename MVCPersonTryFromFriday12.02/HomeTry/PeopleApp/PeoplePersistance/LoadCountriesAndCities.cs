using Newtonsoft.Json.Linq;
using PeopleDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePersistance
{
    public class LoadCountriesAndCities
    {
        private DBContext db = new DBContext();
        public void LoadJson()
        {
            string path = @"C:\Users\Mariya\Dropbox\MVCPersonTryFromFriday12.02\HomeTry\countriesToCities.json";
            string jsonFile = File.ReadAllText(path);
            List<Country> Countries = new List<Country>();
            JObject jO = JObject.Parse(jsonFile);
            foreach (JProperty item in jO.AsJEnumerable())
            {
                List<City> c = new List<City>();
                foreach (string city in item.Values())
                {
                    City ci = new City();
                    ci.Name = city;
                    c.Add(ci);
                }
                Country co = new Country();
                co.Name = item.Name;
                co.cities = new List<City>(c);
                Countries.Add(co);
            }
            for (int i = 0; i < Countries.Count; i++)
            {
                if (i!=132 && i != 247 && i!=247 && i!=242 && i!=230 && i!=226 && i!=223 && i!=221 && i!=220 && i!=219 && i!=217 && i!=216 && i!=215 && i!=205 && i!=202 && i!=201 && i!=197 && i!=187 && i!=172 && i!=151 && i!=125 && i!=110 && i!=124 && i != 6 && i != 14 && i != 29 && i!=50 && i!=101)
                {
                    db.Countries.Add(Countries[i]);
                    db.SaveChanges();
                }
            }
        }
    }
}
