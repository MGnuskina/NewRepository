using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class PersonViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public List<PhoneNumberViewModel> phones { get; set; }
        public List<AddressViewModel> addresses { get; set; }

        public PersonViewModel()
        {
            phones = new List<PhoneNumberViewModel>();
            addresses = new List<AddressViewModel>();
        }
    }
}
