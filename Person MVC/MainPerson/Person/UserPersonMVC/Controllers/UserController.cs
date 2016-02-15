using Microsoft.AspNet.Identity;
using PService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserPersonMVC.Models;

namespace UserPersonMVC.Controllers
{
    public class UserController : Controller
    {
        IUserService service;

        public UserController(IUserService serv)
        {
            service = serv;
        }

        public UserController()
        {
            service = new UserService();
        }

        public ActionResult Index()
        {
            string ID = User.Identity.GetUserId();
            UserViewModel user = service.GetById(ID);
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel user, HttpPostedFileBase uploadImage)
        {
            service.UpdateUserData(user);
            return RedirectToAction("Index");
        }

        public ActionResult AddPicture()
        {
            UserViewModel user = service.GetById(User.Identity.GetUserId());
            return PartialView(user);
        }

        [HttpPost]
        public ActionResult AddPicture(UserViewModel user, HttpPostedFileBase uploadImage)
        {
            if ( uploadImage != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                user.Image = imageData;
                user.ID = User.Identity.GetUserId();
                service.AddPicture(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult People()
        {
            UserViewModel user = service.GetById(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        public ActionResult People(UserViewModel user)
        {
            service.UpdateWithoutAddressPhone(user);
            return RedirectToAction("People");
        }

        public ActionResult AddPerson()
        {
            AddPersonModel addPerson = new AddPersonModel();
            PersonViewModel person= new PersonViewModel();
            person.UserID = User.Identity.GetUserId();
         //   List<CountryViewModel> countries = service.GetCountries();
           // addPerson.countries = new SelectList(countries, "ID", "Name");
           // ViewBag.CountryID = addPerson.countries;
            List<CityViewModel> cities = new List<CityViewModel>();
            //addPerson.cities = new SelectList(cities, "ID", "Name");
           // ViewBag.CitiesID = addPerson.cities;
            addPerson.person = person;
            //List<StreetViewModel> streets = new List<StreetViewModel>();
            //ViewBag.StreetID = new SelectList(streets, "StreetsID", "StreetsName");
            //var tupleModel = new Tuple<PersonViewModel, List<CountryViewModel>, List<CityViewModel>, List<StreetViewModel>>(person, countries, cities, streets);
            return PartialView(addPerson.person);
        }

        [HttpPost]
        public ActionResult AddPerson(PersonViewModel person/*, FormCollection collection*/)
        {
            UserViewModel user = service.GetById(User.Identity.GetUserId());
            //person.Address[0].Street.City = service.GetCityByID(Convert.ToInt32(collection[10]));
            //person.Address[0].Street.City.Country = service.GetCountryByID(Convert.ToInt32(collection[11]));
            user.people.Add(person);
            service.Update(user);
            return RedirectToAction("People");
        }

        [HttpPost]
        public JsonResult GetCities(int CountryID)
        {
            SelectList sl = new SelectList(service.GetCities(CountryID), "ID", "Name");
            ViewBag.CitiesID = sl;
            return Json( ViewBag.CitiesID );
        } 

        public ActionResult Edit(int PersonID)
        {
            PersonViewModel person = service.GetPersonById(PersonID);
            return PartialView(person);
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel person)
        {
            service.UpdatePerson(person);
            return RedirectToAction("People");
        }

        public ActionResult Delete(int PersonID)
        {
            PersonViewModel per = service.GetPersonById(PersonID);
            return PartialView(per);
        }

        [HttpPost]
        public ActionResult Delete(PersonViewModel person)
        {
            service.DeletePerson(person);
            return RedirectToAction("People");
        }

        public ActionResult AddAddress(int PersonID)
        {
            PersonViewModel per = service.GetPersonById(PersonID);
            per.Address[0] = new AddressViewModel();
            per.Address[0].lng = "38";
            per.Address[0].lat = "44";
            return PartialView(per);    
        }

        [HttpPost]
        public ActionResult AddAddress(PersonViewModel pers, FormCollection collection)
        {
            PersonViewModel per = service.GetPersonById(pers.Id);
            pers.Address[0].lat = collection[8];
            pers.Address[0].lng =collection[9];
            per.Address.Add(pers.Address[0]);
            service.AddAddress(per);
            return RedirectToAction("People");
        }

        public ActionResult AddPhoneNumber(int PersonID)
        {
            PersonViewModel per = service.GetPersonById(PersonID);
            per.PhoneNumbers[0] = new PhoneNumberViewModel();
            return PartialView(per);
        }

        [HttpPost]
        public ActionResult AddPhoneNumber(PersonViewModel pers)
        {
            PersonViewModel per = service.GetPersonById(pers.Id);
            per.PhoneNumbers.Add(pers.PhoneNumbers[0]);
            service.AddPhoneNumber(per);
            return RedirectToAction("People");
        }

        public ActionResult Details(int personID)
        {
            PersonViewModel person = service.GetPersonById(personID);
            return View(person);
        }


    }
}