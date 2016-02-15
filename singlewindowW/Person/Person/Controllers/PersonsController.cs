using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PService;

namespace Person.Controllers
{
    public class PersonsController : Controller
    {

        IService service;

        public PersonsController(IService serv)
        {
            service = serv;
        }

        public PersonsController()
        {
            service = new PersonServices();
        }

        public ActionResult Index()
        {
            List<PersonViewModel> people = new List<PersonViewModel>();
            people = service.ReadAll();
            return View(people);
        }

        [HttpPost]
        public ActionResult Index(List<PersonViewModel> per)
        {
            foreach (var item in per)
            {
                service.Update(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddPerson()
        {
            PersonViewModel person = new PersonViewModel();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Mobile", Value = "Mobile", Selected=true });
            li.Add(new SelectListItem { Text = "Work", Value = "Work" });
            li.Add(new SelectListItem { Text = "Home", Value = "Home" });
            ViewData["numberTypes"] = li;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddPerson(PersonViewModel person, FormCollection collection)
        {
            person.PhoneNumbers = new List<PhoneNumberViewModel>();
            for (int i=4; i<collection.Count-1; i=i+2)
            {
                if (collection[i]!="" && collection[i+1]!="")
                {
                    PhoneNumberViewModel phone = new PhoneNumberViewModel()
                    {
                        PhoneNumber = collection[i],
                        PhoneNumberType = collection[i + 1]
                    };
                    person.PhoneNumbers.Add(phone);
                }
            }
            service.Add(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int PersonID)
        {
            PersonViewModel person = service.GetById(PersonID);
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Mobile", Value = "Mobile", Selected = true });
            li.Add(new SelectListItem { Text = "Work", Value = "Work" });
            li.Add(new SelectListItem { Text = "Home", Value = "Home" });
            ViewData["numberTypes"] = li;
            return PartialView(person);
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel person)
        {
            service.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int personID)
        {
            PersonViewModel per = service.GetById(personID);
            return PartialView(per);
        }

        [HttpPost]
        public ActionResult Delete(PersonViewModel person)
        {
            service.Delete(service.GetById(person.Id));
            return RedirectToAction("Index");
        }
    }
}