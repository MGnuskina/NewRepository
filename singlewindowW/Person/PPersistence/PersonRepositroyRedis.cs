using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace PPersistence
{
    public class PersonRepositroyRedis : IRepository<Person>
    {
        static RedisClient redisClient = new RedisClient("127.0.0.1", 6379); // Set the Redis service IP and port
        IRedisTypedClient<Person> client;

        public PersonRepositroyRedis()
        {
            client = redisClient.As<Person>();
        }

        public void Create(Person item)
        {
            item.ID = (item.FirstName + item.LastName + item.Age.ToString()).GetHashCode();
            client.Store(item);
        }

        public void Delete(Person item)
        {
            redisClient.DeleteById<Person>(item.ID);
        }

        public IEnumerable<Person> GetAll()
        {
            return redisClient.As<Person>().GetAll();
        }

        public Person GetByID(int id)
        {
            return redisClient.As<Person>().GetById(id);
        }

        public void Update(Person item)
        {
            client.Store(item);
        }
    }
}
