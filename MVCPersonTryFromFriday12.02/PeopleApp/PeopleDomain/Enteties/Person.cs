using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDomain
{
    public class Person
    {
        [Key]
        public virtual int ID { get; set; }
        [ForeignKey("user")]
        [Required]
        public virtual string UserID { get; set; }
        public virtual User user { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }

        public IList<Address> addresses { get; set; }
        public IList<PhoneNumber> phoneNumbers { get; set; }
    }
}
