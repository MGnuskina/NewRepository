using FluentNHibernate.Mapping;
using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class PhoneNumbersMap:ClassMap<PhoneNumbers>
    {
        public PhoneNumbersMap()
        {
            Id(x => x.ID).Column("ID");
            Map(x => x.PersonID);
            Map(x => x.PhoneNumber);
            Map(x => x.PhoneNumberType);
           // References(x => x.PersonID);
           Table("PhoneNumbers");
        }
    }
}
