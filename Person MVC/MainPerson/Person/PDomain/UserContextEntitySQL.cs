using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class UserContextEntitySQL : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
