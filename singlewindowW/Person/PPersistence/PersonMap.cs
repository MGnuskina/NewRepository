using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class MappingForNHibernatePerson:ClassMap<Person>
    {
        public MappingForNHibernatePerson()
        {
            Id(x => x.ID).GeneratedBy.Identity();//.Column("ID");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Age);
            HasMany(x => x.phonenumbers).Cascade.All();//.KeyColumn("ID").Cascade.All();
           // Table("Person");
        }
    }
}
