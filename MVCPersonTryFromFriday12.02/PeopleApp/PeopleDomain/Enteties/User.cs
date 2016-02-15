using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDomain
{
    public class User
    {
        [Key]
        public virtual string ID { get; set; }
        [Required]
        public virtual string LogInName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PictureName { get; set; }
        public virtual byte[] Image { get; set; }
        public virtual IList<Person> people { get; set; }
    }
}
