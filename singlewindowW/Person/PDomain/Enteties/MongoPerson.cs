using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain.Enteties
{
    [BsonIgnoreExtraElements]
    public class MongoPerson
    {
        public MongoPerson()
        {
            phonenumbers = new List<MongoPhoneNumber>();
        }

        [BsonElement("PNID")]
        public int ID { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Age")]
        [BsonRepresentation(BsonType.Int32)]
        public int Age { get; set; }
        [BsonElement("PhoneNumbers")]
        public List<MongoPhoneNumber> phonenumbers { get; set; }
    }
}
