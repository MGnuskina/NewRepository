using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PeopleApp.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService service;

        public PeopleController(IPeopleService serv)
        {
            service = serv;
        }

        public PeopleController()
        {
            service = new PeopleService.PeopleService();
        }

        public ActionResult Index()
        {
            UserViewModel user = service.GetUserByID(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel user)
        {
            foreach (var per in user.people)
            {
                per.UserID = user.ID;
                service.Update(per);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int personID)
        {
            PersonViewModel person = service.GetPersonBy(personID);
            return View(person);
        }

        [HttpPost]
        public ActionResult Details()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int personID)
        {
            PersonViewModel person = service.GetPersonBy(personID);
            return PartialView(person);
        }

        [HttpPost]
        public ActionResult Delete(PersonViewModel person)
        {
            service.Delete(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int personID)
        {
            PersonViewModel person = service.GetPersonBy(personID);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel person)
        {
            service.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult AddAddress(int personID)
        {
            SelectList sl = new SelectList(service.ReadAllCountries(), "ID", "Name");
            ViewBag.CountriesID = sl;
            List<CityViewModel> cities = new List<CityViewModel>();
            ViewBag.CitiesID = new SelectList(cities,"ID","Name");
            PersonViewModel perosn = service.GetPersonBy(personID);
            perosn.addresses.Add(new AddressViewModel());
            return View(perosn);
        }

        [HttpPost]
        public ActionResult AddAddress(PersonViewModel person, FormCollection collection)
        {
            string ss = collection[collection.Count - 4];
            person.addresses[person.addresses.Count - 1].street.CityId = Convert.ToInt32(ss);
            service.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult AddPN(int personID)
        {
            PersonViewModel perosn = service.GetPersonBy(personID);
            perosn.phones.Add(new PhoneNumberViewModel());
            return PartialView(perosn);
        }

        [HttpPost]
        public ActionResult AddPN(PersonViewModel person)
        {
            PersonViewModel personNew = service.GetPersonBy(person.ID);
            personNew.phones.Add(person.phones[person.phones.Count - 1]);
            service.Update(personNew);
            return RedirectToAction("Index");
        }

        public ActionResult AddPerson()
        {
            PersonViewModel person = new PersonViewModel();
            person.UserID = User.Identity.GetUserId();
            person.addresses = new List<AddressViewModel>();
            person.phones = new List<PhoneNumberViewModel>();
            return View(person);
        }

        [HttpPost]
        public ActionResult AddPerson(PersonViewModel person)
        {
            service.Add(person);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult GetCities(int CountryID)
        {
            SelectList sl = new SelectList(service.ReadAllCitiesByCountry(CountryID), "ID", "Name");
            ViewBag.CitiesID = sl;
            return Json(ViewBag.CitiesID);
        }

        [HttpPost]
        public JsonResult GetCounries()
        {
            SelectList sl = new SelectList(service.ReadAllCountries(), "ID", "Name");
            ViewBag.CountriesID = sl;
            return Json(ViewBag.CountriesID);
        }
    }
}