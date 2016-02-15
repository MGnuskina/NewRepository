using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDomain;

namespace PPersistence
{
    public class AddressRepository : IAddressRepository<Address>
    {
        UserContextEntitySQL db = new UserContextEntitySQL();

        public void AddAddress(Address address)
        {
            db.Addresses.Add(address);
            db.SaveChanges();
        }

        public List<City> GetCities(int CountryID)
        {
            return db.Countries.Find(CountryID).cities.ToList();
        }

        public List<Country> GetCountries()
        {
            return db.Countries.ToList();
        }

        public List<Street> GetStreets(int CityID)
        {
            return db.Cities.Find(CityID).streets.ToList();
        }
    }
}
