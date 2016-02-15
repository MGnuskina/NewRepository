using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class StreetViewModel
    {
        [Required]
        public int ID;
        [Required]
        public string Name;
        [Required]
        public int CityId;
    }
}
