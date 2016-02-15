using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace PDomain
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public virtual int ID { get; set; }
        [ForeignKey("user")]
        [Required]
        public virtual string UserID { get; set; }
        public virtual User user { get; set; }
        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }
        [Required]
        public virtual int Age { get; set; }

        public virtual IList<PhoneNumbers> phonenumbers { get; set; }
        public virtual IList<Address> addresses { get; set; }
    }
}
