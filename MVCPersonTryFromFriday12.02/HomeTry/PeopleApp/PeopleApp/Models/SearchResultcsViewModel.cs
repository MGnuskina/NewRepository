using PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleApp.Models
{
    public class SearchResultcsViewModel
    {
        public List<PersonViewModel> people;
        public string SearchParam;
    }
}