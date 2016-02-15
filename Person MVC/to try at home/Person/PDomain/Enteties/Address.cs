using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class Address
    {
        [Key]
        public virtual int ID { get; set; }
        [ForeignKey("person")]
        [Required]
        public virtual int PersonID { get; set; }
        [Required]
        public virtual string BuildingNumber { get; set; }
        public virtual string FlatNumber { get; set; }
        public virtual double Lat { get; set; }
        public virtual double Lng { get; set; }
        public virtual IList<Person> person { get; set; }
        public virtual Street street { get; set; }
    }
}
