using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleService
{
    public class CountryViewModel
    {
        [Required]
        public int ID;
        [Required]
        public string Name;
        public List<CityViewModel> cities;
    }
}
