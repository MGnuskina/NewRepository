using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDomain
{
    public class Address
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string BuildingNumber { get; set; }
        public virtual string FlatNumber { get; set; }
        [Required]
        [ForeignKey("street")]
        public virtual int StreetID { get; set; }
        public virtual Street street { get; set; }

        public IList<Person> people { get; set; }
    }
}
