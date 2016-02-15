using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class Street
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        //[ForeignKey("city")]
        //[Required]
        //public virtual int CityID { get; set; }
        public virtual City city { get; set; }

        public virtual IList<Address> addresses { get; set; }
    }
}
