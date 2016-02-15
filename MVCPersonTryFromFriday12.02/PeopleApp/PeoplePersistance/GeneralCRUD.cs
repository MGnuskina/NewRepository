using PeopleDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePersistance
{
    public class GeneralCRUD<T> : IRepository<T> where T : class
    {
        DBContext dbContext= new DBContext();
        DbSet<T> dbSet;

        public GeneralCRUD(/*DBContext dbContext*/)
        {
           // this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();///
        }

        public void Create(T item)
        {
            dbSet.Add(item);
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
            int ID=0;
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
            dbSet.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
