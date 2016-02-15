using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace PeopleApp.Controllers
{
    public class UserController : Controller
    {
        IPeopleService service;

        public UserController(IPeopleService serv)
        {
            service = serv;
        }

        public UserController()
        {
            service = new PeopleService.PeopleService();
        }

        public ActionResult Index()
        {
            UserViewModel user = service.GetUserByID(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel user, HttpPostedFileBase uploadImage)
        {
            service.Update(user);
            return RedirectToAction("Index");
        }

        public ActionResult AddPicture()
        {
            UserViewModel user = service.GetUserByID(User.Identity.GetUserId());
            return PartialView(user);
        }

        [HttpPost]
        public ActionResult AddPicture(UserViewModel user, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                UserViewModel userOld = service.GetUserByID(User.Identity.GetUserId());
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                userOld.Image = imageData;
                service.UserOnluUpdate(userOld);
            }

            return RedirectToAction("Index");
        }


    }
}