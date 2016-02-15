using System;
using System.Collections.Generic;
using MongoDB.Driver;
using PDomain;
using PDomain.Enteties;
using System.Linq;

namespace PPersistence
{
    public class PersonRepositoryMongo : IRepository<Person>
    {
        IMongoClient client;
        IMongoDatabase db;

        public PersonRepositoryMongo()
        {
            client = new MongoClient();
            db = client.GetDatabase("NewPeople");
        }

        public void Create(Person item)
        {
            var document = PersonToMongoPerson(item);
            var collection = db.GetCollection<MongoPerson>("NewPeople");
            collection.InsertOne(document);
        }

        public void Delete(Person item) 
        {
            var document = PersonToMongoPerson(item);
            try { 
                var collection = db.GetCollection<MongoPerson>("NewPeople");
                var filter = Builders<MongoPerson>.Filter.Eq("ID", item.ID);
                collection.DeleteMany(filter);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            var collection = db.GetCollection<MongoPerson>("NewPeople");
            List<Person> p = new List<Person>();
            var list = collection.Find(_ => true).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                p.Add(MongoPersonToPerson(list[i]));
            }
            return p;
        }

        public Person GetByID(int id)
        {
            var collection = db.GetCollection<MongoPerson>("NewPeople");
            var pers = collection.FindSync(x => x.ID==id).ToList();
            Person p = MongoPersonToPerson(pers[0]);
            return p;
        }

        public void Update(Person item)
        {
            var collection = db.GetCollection<MongoPerson>("NewPeople");
            var filter = Builders<MongoPerson>.Filter.Eq("ID", item.ID);
            collection.FindOneAndReplace(filter,PersonToMongoPerson(item));
        }

        private Person MongoPersonToPerson(MongoPerson MPerson)
        {
            Person res = new Person()
            {
                ID = MPerson.ID,
                FirstName = MPerson.FirstName,
                LastName = MPerson.LastName,
                Age = MPerson.Age,
                phonenumbers = new List<PhoneNumbers>()
            };

            for (int i = 0; i < MPerson.phonenumbers.Count; i++)
            {
                res.phonenumbers.Add(
                new PhoneNumbers()
                {
                    ID = MPerson.phonenumbers[i].ID,
                    PhoneNumber = MPerson.phonenumbers[i].PhoneNumber,
                    PhoneNumberType = MPerson.phonenumbers[i].PhoneNumberType
                });
            }

            return res;
        }

        private MongoPerson PersonToMongoPerson(Person person)
        {
            MongoPerson res = new MongoPerson
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                ID = (person.FirstName + person.LastName + person.Age.ToString()).GetHashCode(),
                phonenumbers = new List<MongoPhoneNumber>()
            };
            for (int i = 0; i < person.phonenumbers.Count; i++)
            {
                res.phonenumbers.Add(
                    new MongoPhoneNumber()
                    {
                        PhoneNumber = person.phonenumbers[i].PhoneNumber,
                        PhoneNumberType = person.phonenumbers[i].PhoneNumberType,
                        ID = (person.phonenumbers[i].PhoneNumber + person.phonenumbers[i].PhoneNumberType).GetHashCode(),                                                                                             
                    });
            }
            return res;
        }


    }
}
