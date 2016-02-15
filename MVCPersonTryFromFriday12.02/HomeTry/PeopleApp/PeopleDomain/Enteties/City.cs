using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDomain
{
    public class City
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        [ForeignKey("country")]
        public virtual int CountryID { get; set; }
        public virtual Country country { get; set; }

        public  virtual IList<Street> streets { get; set; }
    }
}
