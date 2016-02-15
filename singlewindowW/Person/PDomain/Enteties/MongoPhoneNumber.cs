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
    public class MongoPhoneNumber
    {
        [BsonElement("PNID")]////
        public int ID { get; set; }
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("PhoneNumberType")]
        public string PhoneNumberType { get; set; }
    }
}
