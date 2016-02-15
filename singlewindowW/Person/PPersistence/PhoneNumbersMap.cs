using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public class MappingForNHibernatePhoneNumbers:ClassMap<PhoneNumbers>
    {
        public MappingForNHibernatePhoneNumbers()
        {
            Id(x => x.ID).Column("ID");
            Map(x => x.PersonID);
            Map(x => x.PhoneNumber).Column("PhoneNumber");
            Map(x => x.PhoneNumberType).Column("PhoneNumberType");
            //Table("PhoneNumbers");
            References(x => x.person);
        }
    }
}
