using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public interface IPeopleService
    {
        void Add(UserViewModel user);
        void Add(PersonViewModel person);
        void Add(PhoneNumberViewModel pn);
        void Add(AddressViewModel address);
        void Delete(UserViewModel user);
        void Delete(PersonViewModel person);
        void Delete(PhoneNumberViewModel pn);
        void Delete(AddressViewModel address);
        UserViewModel GetUserByID(string ID);
        PersonViewModel GetPersonBy(int ID);
        List<UserViewModel> ReadAll();
        List<CountryViewModel> ReadAllCountries();
        List<CityViewModel> ReadAllCities();
        List<CityViewModel> ReadAllCitiesByCountry(int countryID);
        void Update(UserViewModel user);
        void Update(PersonViewModel person);
        void UserOnluUpdate(UserViewModel user);
        List<PersonViewModel> GetAllThatContain(string searchParam);
    }
}
