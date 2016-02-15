using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class PhoneNumberViewModel
    {
        [Required]
        public int ID { get; set; }
        [Phone]
        [Required]
        public string Number { get; set; }
        public string Type { get; set; }
    }
}
