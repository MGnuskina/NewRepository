﻿using PeoplePersistance;
using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeopleApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //LoadCountriesAndCities f = new LoadCountriesAndCities();
            //f.LoadJson();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}