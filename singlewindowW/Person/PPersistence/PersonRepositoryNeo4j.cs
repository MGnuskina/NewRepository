using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;
using PDomain;

namespace PPersistence
{
    public class PersonRepositoryNeo4j : IRepository<Person>
    {
        IGraphClient client;

        public PersonRepositoryNeo4j()
        {
            client = new GraphClient(new Uri("http://neo4j:neo4j@localhost:7474/db/data"), "neo4j", "10g428");
            client.Connect();
        }

        public void Create(Person item)
        {
            item.ID = (item.FirstName + item.LastName + item.Age).GetHashCode();
            List<PhoneNumbers> phoneNumbers = new List<PhoneNumbers>(item.phonenumbers);
            item.phonenumbers = null;
            string inputString = "(record:Person {newRecord})";
            client.Cypher.Create(inputString).WithParam("newRecord", item).ExecuteWithoutResults();
            inputString = "(record:PhoneNumbers {newRecord})";
            foreach (var pn in phoneNumbers)
            {
                pn.ID = (pn.PhoneNumber + pn.PhoneNumberType).GetHashCode();
                pn.PersonID = item.ID;
                client.Cypher.Create(inputString).WithParam("newRecord", pn).ExecuteWithoutResults();
                client.Cypher.Match("(person:Person)", "(phonenumber:PhoneNumbers)").Where((Person person) => person.ID == item.ID).AndWhere((PhoneNumbers phonenumber) => phonenumber.ID == pn.ID).Create("person-[:OWNED]->phonenumber").ExecuteWithoutResults();
            }
        }

        public void Delete(Person item)
        {
            string inputString = "(record:Person)-[r]-()";
            client.Cypher.OptionalMatch(inputString).Where((Person record) => record.ID == item.ID).Delete("record,r").ExecuteWithoutResults();
            inputString = "(record:PhoneNumbers)";
            client.Cypher.OptionalMatch(inputString).Where((PhoneNumbers record) => record.PersonID == item.ID).Delete("record").ExecuteWithoutResults();
        }

        public IEnumerable<Person> GetAll()
        {
            string inputString = "(record:Person)";
            var query = client.Cypher.Match(inputString).Return(record => record.As<Person>()).Results;
            List<Person> res = new List<Person>(query);
            return res;
        }

        public Person GetByID(int id)
        {
            string inputString = "(record:Person)";
            var query = client.Cypher.Match(inputString).Where((Person record) => record.ID == id).Return(record => record.As<Person>()).Results;
            Person res = (Person)(query.ToList())[0];
            inputString = "(record:PhoneNumbers)";
            var queryPN = client.Cypher.Match(inputString).Where((PhoneNumbers record) => record.PersonID == id).Return(record => record.As<PhoneNumbers>()).Results;
            res.phonenumbers = new List<PhoneNumbers>(queryPN);


            //var result = client.Cypher
            //    .OptionalMatch("(user:Person)-[OWNED_BY]-(friend:Phone)")
            //    .Where((Person user) => user.ID == id)
            //    .Return((user, friend) => new
            //    {
            //        User = user.As<Person>(),
            //        Friends = friend.CollectAs<PhoneNumbers>()
            //    })
            //    .Results.FirstOrDefault();

            //Person newUser = new Person();

            //newUser = result.User;
            //newUser.phonenumbers = (result.Friends as List<PhoneNumbers>);

            //return newUser;

            return res;
        }

        public void Update(Person item)
        {
            //string inputString = "(record:Person)";
            //client.Cypher.Match(inputString).Where((Person record) => record.ID == item.ID).Set("record={changedPerson}").WithParam("changedPerson", item).ExecuteWithoutResults();
            Delete(item);
            Create(item);
        }
    }
}
