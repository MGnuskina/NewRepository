using PeopleDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePersistance
{
    public class UserRepository : GeneralCRUD<User>
    {
        public List<Person> GetAllThatContain(string searchParam)
        {
            List<Person> person = new List<Person>();
            if (!String.IsNullOrEmpty(searchParam))
            {
                person = (from s in dbContext.Persons select s).ToList();
                person = person.Where(s => s.LastName.Contains(searchParam)|| s.FirstName.Contains(searchParam)).ToList();
            }
            return person;
        }

        public List<City> GetAllCitiesByCountryID(int countryID)
        {
            List<City> cities = new List<City>();
            if (countryID!=0)
            {
                cities = (from s in dbContext.Cities select s).ToList();
                cities = cities.Where(s => s.CountryID==countryID).ToList();
            }
            return cities;
        }

    }
}
