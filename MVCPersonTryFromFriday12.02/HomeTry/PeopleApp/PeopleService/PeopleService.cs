using AutoMapper;
using PeopleDomain;
using PeoplePersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class PeopleService : IPeopleService
    {
        IRepository<Person> repositoryPersonGeneral;
        IRepository<Address> repositoryAddressGeneral;
        IRepository<Country> repositoryCountryGeneral;
        IRepository<City> repositoryCityGeneral;
        IRepository<PhoneNumber> repositoryPhoneNumberGeneral;
        IRepository<User> repository;

        public PeopleService()
        {
            repository = new UserRepository();
            repositoryPersonGeneral = new GeneralCRUD<Person>();
            repositoryAddressGeneral = new GeneralCRUD<Address>();
            repositoryCountryGeneral = new GeneralCRUD<Country>();
            repositoryCityGeneral = new GeneralCRUD<City>();
            repositoryPhoneNumberGeneral = new GeneralCRUD<PhoneNumber>();
            #region Mapping
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<Person, PersonViewModel>();
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<Street, StreetViewModel>();
            Mapper.CreateMap<City, CityViewModel>();
            Mapper.CreateMap<Country, CountryViewModel>();
            Mapper.CreateMap<PhoneNumber, PhoneNumberViewModel>();
            Mapper.CreateMap<List<City>, List<CityViewModel>>();

            Mapper.CreateMap<CountryViewModel, Country>();
            Mapper.CreateMap<CityViewModel, City>();
            Mapper.CreateMap<StreetViewModel, Street>();
            Mapper.CreateMap<PersonViewModel, Person>();
            Mapper.CreateMap<PhoneNumberViewModel, PhoneNumber>();
            Mapper.CreateMap<AddressViewModel, Address>();
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<List<CityViewModel>, List<City>>();
            #endregion
        }

        public void Add(UserViewModel user)
        {
            User User = Mapper.Map<User>(user);
            repository.Create(User);
        }

        public void Add(PersonViewModel person)
        {
            Person Person = Mapper.Map<Person>(person);
            Person.user = null;
            repositoryPersonGeneral.Create(Person);
            //User user = repository.GetByID(Person.UserID);
            //user.people.Add(Person);
            //repository.Update(user);
        }

        public void Add(AddressViewModel address)
        {
            Address Address = Mapper.Map<Address>(address);
            //Address.street.city.country = null;
            Address.street.city = null;
            Address.street = null;
            repositoryAddressGeneral.Create(Address);
        }

        public void Add(PhoneNumberViewModel pn)
        {
            PhoneNumber PN = Mapper.Map<PhoneNumber>(pn);
            repositoryPhoneNumberGeneral.Create(PN);
        }

        public void Delete(UserViewModel user)
        {
            User User = Mapper.Map<User>(user);
            repository.Delete(User);
        }

        public void Delete(PersonViewModel person)
        {
            Person Person = repositoryPersonGeneral.GetByID(person.ID.ToString());//Mapper.Map<Person>(person);
            repositoryPersonGeneral.Delete(Person);
        }

        public void Delete(AddressViewModel address)
        {
            Address Address = Mapper.Map<Address>(address);
            repositoryAddressGeneral.Delete(Address);
        }

        public void Delete(PhoneNumberViewModel pn)
        {
            PhoneNumber PN = Mapper.Map<PhoneNumber>(pn);
            repositoryPhoneNumberGeneral.Delete(PN);
        }

        public UserViewModel GetUserByID(string ID)
        {
            User User = repository.GetByID(ID);
            UserViewModel user = Mapper.Map<UserViewModel>(User);
            return user;
        }

        public PersonViewModel GetPersonBy(int ID)
        {
            Person Person = repositoryPersonGeneral.GetByID(ID.ToString());
            PersonViewModel person = Mapper.Map<PersonViewModel>(Person);

            person.addresses = new List<AddressViewModel>();
            if (Person.addresses!=null)
            for(int i=0; i<Person.addresses.Count; i++)
            {
                person.addresses.Add(Mapper.Map<AddressViewModel>(Person.addresses[i]));
            }
            person.phones = new List<PhoneNumberViewModel>();
            if (Person.phoneNumbers!=null)
            for (int i=0; i<Person.phoneNumbers.Count; i++)
            {
                person.phones.Add(Mapper.Map<PhoneNumberViewModel>(Person.phoneNumbers[i]));
            }
            return person;
        }

        public List<UserViewModel> ReadAll()
        {
            List<User> Users = repository.GetAll().ToList();
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var u in Users)
            {
                UserViewModel tmp = Mapper.Map<UserViewModel>(u);
                users.Add(tmp);
            }
            return users;
        }

        public List<CountryViewModel> ReadAllCountries()
        {
            List<Country> Countries = repositoryCountryGeneral.GetAll().ToList();
            List<CountryViewModel> countries = new List<CountryViewModel>();
            foreach (var c in Countries)
            {
                CountryViewModel country = Mapper.Map<CountryViewModel>(c);
                countries.Add(country);
            }
            return countries;
        }

        public List<CityViewModel> ReadAllCities()
        {
            List<City> Cities = repositoryCityGeneral.GetAll().ToList();
            List<CityViewModel> cities = new List<CityViewModel>();
            foreach (var c in Cities)
            {
                CityViewModel city = Mapper.Map<CityViewModel>(c);
                cities.Add(city);
            }
            return cities;
        }

        public List<CityViewModel> ReadAllCitiesByCountry(int countryID)
        {
            //Country country = repositoryCountryGeneral.GetByID(countryID.ToString());
            UserRepository repAdditional = new UserRepository();
            List<City> Cities = repAdditional.GetAllCitiesByCountryID(countryID);//country.cities.ToList();
            List<CityViewModel> cities = new List<CityViewModel>();
            cities = Mapper.Map <List<CityViewModel>>(Cities);
            for (int i = 0; i < Cities.Count; i++)
            {
                CityViewModel city = Mapper.Map<CityViewModel>(Cities[i]);
                cities.Add(city);
            }
            return cities;
        }

        public void Update(UserViewModel user)
        {
            User User = Mapper.Map<User>(user);
            repository.Update(User);
        }

        public void Update(PersonViewModel person)
        {
            Person Person = Mapper.Map<Person>(person);
            User user = repository.GetByID(person.UserID);
            Person.user = user;
            if (person.addresses != null)
            {
                Person.addresses = new List<Address>();
                foreach (var a in person.addresses)
                {
                    Address address = Mapper.Map<Address>(a);
                   // address.people = new List<Person>();
                   ////
                    //address.people.Add(Person);
                    if (address.ID == 0)
                    {
                        repositoryAddressGeneral.Create(address);
                    }
                    else
                    {
                        repositoryAddressGeneral.Update(address);
                    }
                    Person.addresses.Add(address);
                    //
                }
            }
            if (person.phones != null)
            {
                Person.phoneNumbers = new List<PhoneNumber>();
                foreach (var a in person.phones)
                {
                    PhoneNumber pn = Mapper.Map<PhoneNumber>(a);
                    pn.PersonID = person.ID;
                   // pn.person = Person;
                    if (pn.ID == 0)
                    {
                        repositoryPhoneNumberGeneral.Create(pn);
                    }
                    else
                    {
                        repositoryPhoneNumberGeneral.Update(pn);
                    }
                    Person.phoneNumbers.Add(pn);
                }
            }
            //User user = repository.GetByID(person.UserID);
            //user.people.Add(Person);
            //repository.Update(user);

            repositoryPersonGeneral.Create(Person);//.Update(Person);
        }

        public void UserOnluUpdate(UserViewModel user)
        {
            User User = Mapper.Map<User>(user);
            //User.people = null;
            repository.Update(User);
        }

        public List<PersonViewModel> GetAllThatContain(string searchParam)
        {
            List<PersonViewModel> person = new List<PersonViewModel>();
            UserRepository UserRep = new UserRepository();
            List<Person> p= UserRep.GetAllThatContain(searchParam);
            foreach (var pers in p)
            {
                person.Add(Mapper.Map<PersonViewModel>(pers));
            }
            return person;
        }
    }
}
