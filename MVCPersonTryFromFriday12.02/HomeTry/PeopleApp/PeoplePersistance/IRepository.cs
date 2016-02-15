using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePersistance
{
    public interface IRepository<T>
    {
        void Create(T item);
        IEnumerable<T> GetAll();
        T GetByID(string id);
        void Update(T item);
        void Delete(T item);
    }
}
