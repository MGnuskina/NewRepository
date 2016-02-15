using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PService
{
    public class CityViewModel
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }

        public CountryViewModel Country { get; set; }
    }
}
