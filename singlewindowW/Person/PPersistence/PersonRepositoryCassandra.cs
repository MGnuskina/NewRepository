using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using CassandraSharp;

namespace PPersistence
{
    public class PersonRepositoryCassandra : IRepository<Person>
    {
        ISession session;
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        public PersonRepositoryCassandra()
        {
            session = cluster.Connect("person");
        }

        public void Create(Person item)
        {
            int id = (item.FirstName + item.LastName + item.Age.ToString()).GetHashCode();
            session.Execute("insert into person (id, firstname, lastname, age) values (" + id + ",'" + item.FirstName + "','" + item.LastName + "'," + item.Age + ")");
            if (item.phonenumbers != null)
            {
                foreach (var pn in item.phonenumbers)
                {
                    int idPn = (pn.PhoneNumber + pn.PhoneNumberType).GetHashCode();
                    session.Execute("insert into phonenumber (id, personid, phonenumber, phonenumbertype) values (" + idPn + "," + id + ",'" + pn.PhoneNumber + "','" + pn.PhoneNumberType + "')");
                }
            }
        }

        public void Delete(Person item)
        {
            RowSet PhoneNumbersRows = session.Execute("select * from phonenumber");
            foreach (var pnrow in PhoneNumbersRows)
            {
                if (Convert.ToInt32(pnrow["personid"]) == item.ID)
                {
                    session.Execute("delete from phonenumber where id=" + Convert.ToInt32(pnrow["id"]));
                }
            }
            session.Execute("delete from person where id=" + item.ID);
        }

        public IEnumerable<Person> GetAll()
        {
            List<Person> people = new List<Person>();
            RowSet rows = session.Execute("select * from person");
            foreach (var row in rows)
            {
                people.Add(new Person()
                {
                    ID = Convert.ToInt32(row["id"]),
                    FirstName = row["firstname"].ToString(),
                    LastName = row["lastname"].ToString(),
                    Age = Convert.ToInt32(row["age"]),
                    phonenumbers = new List<PhoneNumbers>()
                });
                int pID = Convert.ToInt32(row["id"]);
                RowSet PhoneNumbersRows = session.Execute("select * from phonenumber");
                foreach (var pnrow in PhoneNumbersRows)
                {
                    int pnID = Convert.ToInt32(pnrow["personid"]);
                    if ( pnID== pID)
                    {
                        people[people.Count - 1].phonenumbers.Add(new PhoneNumbers()
                        {
                            ID = Convert.ToInt32(pnrow["id"]),
                            PhoneNumber = pnrow["phonenumber"].ToString(),
                            PhoneNumberType = pnrow["phonenumbertype"].ToString(),
                            PersonID = Convert.ToInt32(pnrow["personid"])
                        });
                    }
                }
            }
            return people;
        }

        public Person GetByID(int id)
        {
            Row res = session.Execute("select * from person where id=" + id).First();
            RowSet PhoneNumbersRows = session.Execute("select * from phonenumber");
            Person person = new Person()
            {
                ID = id,
                FirstName = res["firstname"].ToString(),
                LastName = res["lastname"].ToString(),
                Age = Convert.ToInt32(res["age"]),
                phonenumbers = new List<PhoneNumbers>()
            };
            foreach (var pnrow in PhoneNumbersRows)
            {
                if (Convert.ToInt32(pnrow["personid"]) == Convert.ToInt32(res["id"]))
                {
                    person.phonenumbers.Add(new PhoneNumbers()
                    {
                        ID = Convert.ToInt32(pnrow["id"]),
                        PhoneNumber = pnrow["phonenumber"].ToString(),
                        PhoneNumberType = pnrow["phonenumbertype"].ToString(),
                        PersonID = Convert.ToInt32(res["id"])
                    });
                }
            }
            return person;
        }

        public void Update(Person item)
        {
            session.Execute("update person set firstname='" + item.FirstName + "', lastname='" + item.LastName + "', age=" + item.Age + " where id=" + item.ID);
            foreach (var pnrow in item.phonenumbers)
            {
                session.Execute("update phonenumber set phonenumber='" + pnrow.PhoneNumber + "', phonenumbertype='" + pnrow.PhoneNumberType + "' where id=" + pnrow.ID);
            }
        }
    }
}
