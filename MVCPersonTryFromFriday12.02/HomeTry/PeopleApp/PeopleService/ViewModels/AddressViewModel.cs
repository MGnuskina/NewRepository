using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleService
{
    public class AddressViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public int StreetID { get; set; }
        public StreetViewModel street { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public AddressViewModel()
        {
            Lng = "40";
            Lat = "40";
        }
    }
}
