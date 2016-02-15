using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeopleApp.Models;

namespace PeopleApp.Controllers
{
    public class SearchController : Controller
    {
        IPeopleService service;

        public SearchController(IPeopleService serv)
        {
            service = serv;
        }

        public SearchController()
        {
            service = new PeopleService.PeopleService();
        }

        public ActionResult Index()
        {
            SearchResultcsViewModel result = new SearchResultcsViewModel();
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(SearchResultcsViewModel search, FormCollection collection)
        {
            search.SearchParam = collection[1];
            search.people=service.GetAllThatContain(search.SearchParam);            
            return View(search);
        }
    }
}