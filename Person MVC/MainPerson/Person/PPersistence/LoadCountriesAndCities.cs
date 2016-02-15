using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class LoadCountriesAndCities
    {
        private UserContextEntitySQL db = new UserContextEntitySQL();
        public void LoadJson()
        {
            string path = @"C:\Users\Mariya\Dropbox\Person MVC\countriesToCities.json";
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
            for(int i=248; i<Countries.Count; i++)
            {
                if (i>247/*247,242,230,226,223,221,220,219,217,216,215,205,202,201,197,187,172,151,125,110,124 i != 6 && i != 14 && i != 29 && i!=50 && i!=101*/)
                {
                    db.Countries.Add(Countries[i]);
                    db.SaveChanges();
                }
            }
        }
    }
}

public class Country1
{
    string country;
    List<City1> cities;
    public Country1(string country, List<City1> cities)
    {
        this.country = country;
        this.cities = cities;
    }
    public Country1() { }
}

public class City1
{
    string city;
    public City1(string city)
    {
        this.city = city;
    }
    public City1() { }
}

