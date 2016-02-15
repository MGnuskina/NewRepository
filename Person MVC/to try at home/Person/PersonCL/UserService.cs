using PDomain;
using PPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PService
{
    public class UserService : IUserService
    {
        IUserRepository<User> repository;
        IRepository<Person> repositoryPerson;
        IAddressRepository<Address> repositoryAddress;

        public UserService()
        {
            #region AutoMap
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<AddressViewModel, Address>();
            Mapper.CreateMap<Person, PersonViewModel>();
            Mapper.CreateMap<PersonViewModel, Person>();
            Mapper.CreateMap<Street, StreetViewModel>();
            Mapper.CreateMap<StreetViewModel, Street>();
            Mapper.CreateMap<City, CityViewModel>();
            Mapper.CreateMap<CityViewModel, City>();
            Mapper.CreateMap<Country, CountryViewModel>();
            Mapper.CreateMap<CountryViewModel, Country>();
            Mapper.CreateMap<PhoneNumbers, PhoneNumberViewModel>();
            Mapper.CreateMap<PhoneNumberViewModel, PhoneNumbers>();
            Mapper.CreateMap<List<User>, List<UserViewModel>>();
            Mapper.CreateMap<List<UserViewModel>, List<User>>();
            Mapper.CreateMap<List<PhoneNumbers>, List<PhoneNumberViewModel>>();
            Mapper.CreateMap<List<PhoneNumberViewModel>, List<PhoneNumbers>>();
            Mapper.CreateMap<List<Address>, List<AddressViewModel>>();
            Mapper.CreateMap<List<AddressViewModel>, List<Address>>();
            Mapper.CreateMap<List<Person>, List<PersonViewModel>>();
            Mapper.CreateMap<List<PersonViewModel>, List<Person>>();
            Mapper.CreateMap<List<Street>, List<StreetViewModel>>();
            Mapper.CreateMap<List<StreetViewModel>, List<Street>>();
            Mapper.CreateMap<List<City>, List<CityViewModel>>();
            Mapper.CreateMap<List<CityViewModel>, List<City>>();
            Mapper.CreateMap<List<Country>, List<CountryViewModel>>();
            Mapper.CreateMap<List<CountryViewModel>, List<Country>>();
            #endregion
            repository = new UserRepositoryEF();
            repositoryAddress = new AddressRepository();
            repositoryPerson = new PersonRepositoryEF();
        }

        public void Add(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            repository.Create(userR);
        }

        public void Delete(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            repository.Delete(userR);
        }

        public UserViewModel GetById(string id)
        {
            User user = repository.GetByID(id);
            UserViewModel userVM = Mapper.Map<UserViewModel>(user);
            userVM.people = new List<PersonViewModel>();
            foreach (var per in user.people)
            {
                userVM.people.Add(Mapper.Map<PersonViewModel>(per));
                userVM.people[userVM.people.Count - 1].Address = new List<AddressViewModel>();
                foreach (var add in per.addresses)
                {
                    userVM.people[userVM.people.Count - 1].Address.Add(Mapper.Map<AddressViewModel>(add));
                }
                userVM.people[userVM.people.Count - 1].PhoneNumbers = new List<PhoneNumberViewModel>();
                foreach (var ph in per.phonenumbers)
                {
                    userVM.people[userVM.people.Count - 1].PhoneNumbers.Add(Mapper.Map<PhoneNumberViewModel>(ph));
                }
            }
            return userVM;
        }

        public PersonViewModel GetPersonById(int id)
        {
            Person person = repositoryPerson.GetByID(id);
            PersonViewModel personVM = Mapper.Map<PersonViewModel>(person);
            personVM.Address = new List<AddressViewModel>();
            foreach (var add in person.addresses)
            {
                personVM.Address.Add(Mapper.Map<AddressViewModel>(add));
            }
            personVM.PhoneNumbers = new List<PhoneNumberViewModel>();
            foreach (var ph in person.phonenumbers)
            {
                personVM.PhoneNumbers.Add(Mapper.Map<PhoneNumberViewModel>(ph));
            }
            return personVM;
        }

        public List<UserViewModel> ReadAll()
        {
            List<User> users = repository.GetAllUsers().ToList();
            List<UserViewModel> usersVM = Mapper.Map<List<UserViewModel>>(users);
            return usersVM;
        }

        public void Update(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            userR.people = new List<Person>();
            foreach (var per in user.people)
            {
                userR.people.Add(Mapper.Map<Person>(per));
                userR.people[userR.people.Count - 1].UserID = user.ID;
                userR.people[userR.people.Count - 1].addresses = new List<Address>();
                if (per.Address != null)
                {
                    foreach (var add in per.Address)
                    {
                        Address address = Mapper.Map<Address>(add);
                        userR.people[userR.people.Count - 1].addresses.Add(address);
                    }
                }
                userR.people[userR.people.Count - 1].phonenumbers = new List<PhoneNumbers>();
                foreach (var ph in per.PhoneNumbers)
                {
                    userR.people[userR.people.Count - 1].phonenumbers.Add(Mapper.Map<PhoneNumbers>(ph));
                }
            }
            repository.Update(userR);
        }

        public void UpdateUserData(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            repository.UpdateUserData(userR);
        }

        public void UpdatePerson(PersonViewModel person)
        {
            Person personR = ToPerson(person);
            repository.UpdatePerson(personR);
        }

        public void UpdateWithoutAddressPhone(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            userR.people = new List<Person>();
            foreach (var per in user.people)
            {
                Person p = Mapper.Map<Person>(per);
                p.UserID = user.ID;
                repository.UpdateWithoutAddressPhone(p);
            }

        }

        public void SaveOrUpdatePerson(PersonViewModel person, string UserID)
        {
            Person personR = ToPerson(person);
            personR.UserID = UserID;
            repository.SaveOrUpdatePerson(personR);
        }

        public void DeletePerson(PersonViewModel person)
        {
            Person personR = ToPerson(GetPersonById(person.Id));

            repository.DeletePerson(personR);
        }

        #region Mapping
        public Person ToPerson(PersonViewModel person)
        {
            Person personR = Mapper.Map<Person>(person);
            personR.addresses = new List<Address>();
            foreach (var add in person.Address)
            {
                Address a = Mapper.Map<Address>(add);
                a.PersonID = person.Id;
                personR.addresses.Add(a);
            }
            personR.phonenumbers = new List<PhoneNumbers>();
            foreach (var ph in person.PhoneNumbers)
            {
                PhoneNumbers p = Mapper.Map<PhoneNumbers>(ph);
                p.PersonID = person.Id;
                personR.phonenumbers.Add(p);
            }
            return personR;
        }
        #endregion

        public void AddAddress(PersonViewModel person)
        {
            Address address = Mapper.Map<Address>(person.Address[person.Address.Count - 1]);
            address.PersonID = person.Id;
            Person personR = ToPerson(GetPersonById(person.Id));
            personR.addresses.Add(address);
            personR.addresses[personR.addresses.Count - 1].PersonID = person.Id;
            personR.addresses[personR.addresses.Count - 1].person = new List<Person>();
            personR.addresses[personR.addresses.Count - 1].person.Add(personR);
            repositoryPerson.Update(personR);
        }

        public void AddPicture(UserViewModel user)
        {
            User userR = Mapper.Map<User>(user);
            repository.AddPicture(userR);
        }

        public List<StreetViewModel> GetStreets(int CityID)
        {
            List<Street> streets = repositoryAddress.GetStreets(CityID);
            List<StreetViewModel> streetsVM = Mapper.Map<List<StreetViewModel>>(streets);
            return streetsVM;
        }

        public List<CityViewModel> GetCities(int CountryID)
        {
            List<City> cities = repositoryAddress.GetCities(CountryID);
            List<CityViewModel> citiesVM = Mapper.Map<List<CityViewModel>>(cities);
            return citiesVM;
        }

        public List<CountryViewModel> GetCountries()
        {
            List<Country> counties = repositoryAddress.GetCountries();
            List<CountryViewModel> countriesVM = new List<CountryViewModel>();
            foreach (var c in counties)
            {
                countriesVM.Add(Mapper.Map<CountryViewModel>(c));
            }
            return countriesVM;
        }
    }
}
