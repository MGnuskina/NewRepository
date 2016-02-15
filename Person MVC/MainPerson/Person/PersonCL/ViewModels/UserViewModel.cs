using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PService
{
    public class UserViewModel
    {
        public string ID { get; set; }
        [Required]
        public string LogInName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string pictureName { get; set; }
        public byte[] Image { get; set; }
        public IList<PersonViewModel> people { get; set; }
    }
}
