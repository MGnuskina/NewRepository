using PService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserPersonMVC.Models
{
    public class AddPersonModel
    {
        public PersonViewModel person { get; set; }
        public System.Web.Mvc.SelectList countries { get; set; }
        public System.Web.Mvc.SelectList cities { get; set; }
    }
}