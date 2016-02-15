using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class CityViewModel
    {
        [Required]
        public int ID;
        [Required]
        public string Name;
        [Required]
        public int CountreyID;
        public List<StreetViewModel> strets;
    }
}
