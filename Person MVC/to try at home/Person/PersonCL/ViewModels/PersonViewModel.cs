using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PService
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0,120)]
        public int Age { get; set; }

        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
        public List<AddressViewModel> Address { get; set; }
    }
}