using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PService
{
    public class AddressViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        [Required]
        public StreetViewModel Street { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public AddressViewModel()
        {
            Street = new StreetViewModel();
            Street.City = new CityViewModel();
            Street.City.Country = new CountryViewModel();
        }
    }
}
