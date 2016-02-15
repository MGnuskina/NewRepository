using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class UserViewModel
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string LogInName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureName { get; set; }
        public byte[] Image { get; set; }
        public List<PersonViewModel> people { get; set; }
    }
}
