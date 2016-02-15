using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDomain
{
    public class PhoneNumber
    {
        [Key]
        public virtual int ID { get; set; }
        [Phone]
        [Required]
        public virtual string Number { get; set; }
        public virtual string Type { get; set; }

        [Required]
        [ForeignKey("person")]
        public virtual int PersonID { get; set; }
        public virtual Person person { get; set; }
    }
}
