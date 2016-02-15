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
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountreyID { get; set; }
        public List<StreetViewModel> strets { get; set; }
        public CountryViewModel country { get; set; }
    }
}
