using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PService
{
    public class StreetViewModel
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual CityViewModel City { get; set; }
    }
}
