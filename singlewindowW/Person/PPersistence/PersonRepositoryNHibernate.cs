using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using yesmarket.NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Linq;

namespace PPersistence
{
    public class PersonRepositoryNHibernate : IRepository<Person>
    {

        protected readonly ISession session;

        public PersonRepositoryNHibernate(ISession session)
        {
            this.session = session;
        }

        public void Create(Person item)
        {
            foreach(var element in item.phonenumbers)
            {
                element.person = item;
            }
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                foreach (var element in item.phonenumbers)
                {
                    element.PersonID = item.ID;
                }
                session.SaveOrUpdate(item);

                transaction.Commit();
            }
        }

        public void Delete(Person item)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                var data = session.QueryOver<Person>().Where(x => x.ID == item.ID).SingleOrDefault();
                session.Delete(data);
                transaction.Commit();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            IList<Person> people;
            people = session.CreateCriteria<Person>().List<Person>();
            return people;
        }

        public Person GetByID(int id)
        {
            Person person;
            person = session.QueryOver<Person>().Where(x => x.ID == id).SingleOrDefault();
            return person;
        }

        public void Update(Person item)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                Person p = GetByID(item.ID);
                p.LastName = item.LastName;
                p.FirstName = item.FirstName;
                p.Age = item.Age;
                p.phonenumbers = new List<PhoneNumbers>();
                foreach(var pn in item.phonenumbers)
                {
                    PhoneNumbers ph = new PhoneNumbers { ID = pn.ID, person = p, PersonID = p.ID, PhoneNumber = pn.PhoneNumber, PhoneNumberType = pn.PhoneNumberType };
                    p.phonenumbers.Add(ph);
                }
                session.Merge(p);
                transaction.Commit();
            }
        }
    }
}
