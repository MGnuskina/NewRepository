using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PService
{
    public class PhoneNumberViewModel
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }  
    }
}
