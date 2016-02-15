using Microsoft.AspNet.Identity;
using PService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            PersonViewModel person= new PersonViewModel();
            person.UserID = User.Identity.GetUserId();
            List<CountryViewModel> countries = service.GetCountries();
            ViewBag.CountryID = new SelectList(countries, "CountryID", "CountryName");
            List<CityViewModel> cities = new List<CityViewModel>();
            ViewBag.CitiesID = new SelectList(cities, "CitiesID", "CitiesName");
            List<StreetViewModel> streets = new List<StreetViewModel>();
            ViewBag.StreetID = new SelectList(streets, "StreetsID", "StreetsName");
            //var tupleModel = new Tuple<PersonViewModel, List<CountryViewModel>, List<CityViewModel>, List<StreetViewModel>>(person, countries, cities, streets);
            return PartialView(person);
        }

        [HttpPost]
        public ActionResult AddPerson(PersonViewModel person)
        {
            UserViewModel user = service.GetById(User.Identity.GetUserId());
            user.people.Add(person);
            service.Update(user);
            return RedirectToAction("People");
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
            AddressViewModel address = new AddressViewModel() { };
            per.Address.Add(address);
            return PartialView(per);    
        }

        [HttpPost]
        public ActionResult AddAddress(PersonViewModel pers)
        {
            PersonViewModel per = service.GetPersonById(pers.Id);
            per.Address.Add(pers.Address[pers.Address.Count - 1]);
            service.AddAddress(per);
            return RedirectToAction("People");
        }

        public ActionResult Details(int personID)
        {
            PersonViewModel person = service.GetPersonById(personID);
            return View(person);
        }
    }
}