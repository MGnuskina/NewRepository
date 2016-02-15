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
        public string ID;
        [Required]
        public string LogIn;
        public string FirstName;
        public string LastName;
        public string PictureName;
        public byte[] Image;
        public List<PersonViewModel> people;
    }
}
