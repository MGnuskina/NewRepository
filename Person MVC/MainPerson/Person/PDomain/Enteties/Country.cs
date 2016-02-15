using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class Country
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }

        public virtual IList<City> cities { get; set; }
    }
}
