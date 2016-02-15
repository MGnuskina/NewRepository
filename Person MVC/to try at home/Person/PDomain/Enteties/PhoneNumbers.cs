using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class PhoneNumbers
    {
        [Key]
        public virtual int ID { get; set; }
        [ForeignKey("person")]
        [Required]
        public virtual int PersonID { get; set; }
        [Phone]
        [Required]
        public virtual string PhoneNumber { get; set; }
        public virtual string PhoneNumberType { get; set; }

        public virtual Person person { get; set; }

    }
}
