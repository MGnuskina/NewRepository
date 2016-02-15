using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PDomain
{
    public class PersonContextEntitySQL: DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
    }
}
