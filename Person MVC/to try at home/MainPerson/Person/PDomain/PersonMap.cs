using FluentNHibernate.Mapping;
using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class PersonMap:ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.ID).Column("ID");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Age);
           // HasMany(x => x.phonenumbers);//.KeyColumn("ID").Cascade.All();
            Table("Person");
        }
    }
}
