using PeopleDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePersistance
{
    public class GeneralCRUD<T> : IRepository<T> where T : class
    {
        internal DBContext dbContext = new DBContext();
        internal DbSet<T> dbSet;

        public GeneralCRUD(/*DBContext dbContext*/)
        {
            // this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();///
        }

        public void Create(T item)
        {
            //dbSet.Add(item);
            dbContext.Set<T>().AddOrUpdate(item);
            dbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            dbSet.Remove(item);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> list = dbSet.ToList();
            return list;
        }

        public T GetByID(string id)
        {
            int ID = 0;
            if (int.TryParse(id, out ID))
            {
                return dbSet.Find(ID);
            }
            else
            {
                return dbSet.Find(id);
            }
        }

        public void Update(T item)
        {
            dbContext.Set<T>().AddOrUpdate(item);
            //if (typeof(T) == typeof(Person))
            //{
            //    dbContext.Persons.AddOrUpdate(h => h.ID, item as Person);
            //}
            //else
            //{
            //    if (typeof(T) == typeof(User))
            //    {
            //        dbContext.Users.AddOrUpdate(h => h.ID, item as User);
            //    }
            //    else
            //    {
            //        dbContext.Entry(item).State = EntityState.Modified;
            //    }
            //}
            dbContext.SaveChanges();
        }
    }
}
