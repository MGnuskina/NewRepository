using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public interface IAddressRepository<T>
    {
        void AddAddress(T address);
        List<Street> GetStreets(int CityID);
        List<City> GetCities(int CountryID);
        List<Country> GetCountries();
    }
}
