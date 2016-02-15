using PDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public interface IUserRepository<T>
    {
        void Create(T item);
        IEnumerable<T> GetAllUsers();
        T GetByID(string id);
        void Update(T item);
        void Delete(T item);
        void UpdateUserData(T item);
        //Person GetPersonByID(int Id);////
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        void SaveOrUpdatePerson(Person person);
        void UpdateWithoutAddressPhone(Person person);
        void AddPicture(T item);
    }
}
