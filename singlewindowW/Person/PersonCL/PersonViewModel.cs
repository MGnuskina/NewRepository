using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PService
{
    public class PersonViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0,120)]
        public int Age { get; set; }

        //check that it is a phone number!/
        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }

        //public enum PNTypes ///maybe not here.........
        //{
        //    Mobile,
        //    Work,
        //    Home
        //}
    }
}