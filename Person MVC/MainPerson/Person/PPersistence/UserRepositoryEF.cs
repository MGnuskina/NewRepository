using PDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class UserRepositoryEF : IUserRepository<User>
    {
        private IRepository<Person> personRepository;
        private UserContextEntitySQL db = new UserContextEntitySQL();

        public UserRepositoryEF()
        {
            personRepository = new PersonRepositoryEF();
        }

        public void Create(User item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void Delete(User item)
        {
            item = GetByID(item.ID);
            db.Users.Remove(item);
            db.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = db.Users.ToList();
            return users;
        }

        public User GetByID(string id)
        {
            return db.Users.Find(id);
        }

        public void Update(User item)
        {
            User itemsInDb = GetByID(item.ID);
            if (itemsInDb.people.Count < item.people.Count)
            {
                AddPerson(item.people[item.people.Count - 1]);
            }
            for (int i = 0; i < itemsInDb.people.Count; i++)
            {
                UpdatePerson(item.people[i]);
            }
        }

        private void UpdatePersonInUser(Person person)
        {
            personRepository.Update(person);
        }

        public void UpdateUserData(User item)
        {
            User itemsInDb = GetByID(item.ID);
            itemsInDb.FirstName = item.FirstName;
            itemsInDb.LastName = item.LastName;
            itemsInDb.LogInName = item.LogInName;
            db.Entry(itemsInDb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            User user = GetByID(person.UserID);
            int i = 0;
            while (user.people.Count > i && user.people[i].ID != person.ID)
            {
                i++;
            }
            if (i == user.people.Count)
            {
                throw new ArgumentException();
            }
            user.people[i].LastName = person.LastName;
            user.people[i].Age = person.Age;
            user.people[i].FirstName = person.FirstName;
            for (int k = 0; k < person.addresses.Count; k++)
            {
                user.people[i].addresses[k].FlatNumber = person.addresses[k].FlatNumber;
                user.people[i].addresses[k].BuildingNumber = person.addresses[k].BuildingNumber;
                user.people[i].addresses[k].street = person.addresses[k].street;
                user.people[i].addresses[k].street.city = person.addresses[k].street.city;
                user.people[i].addresses[k].street.city.country = person.addresses[k].street.city.country;
            }
            for (int k = 0; k < person.phonenumbers.Count; k++)
            {
                user.people[i].phonenumbers[k].PhoneNumber = person.phonenumbers[k].PhoneNumber;
                user.people[i].phonenumbers[k].PhoneNumberType = person.phonenumbers[k].PhoneNumberType;
            }
            db.Entry(user.people[i]).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateWithoutAddressPhone(Person person)
        {
            User user = GetByID(person.UserID);
            int i = 0;
            while (user.people.Count > i && user.people[i].ID != person.ID)
            {
                i++;
            }
            if (i == user.people.Count)
            {
                throw new ArgumentException();
            }
            user.people[i].LastName = person.LastName;
            user.people[i].Age = person.Age;
            user.people[i].FirstName = person.FirstName;
            db.Entry(user.people[i]).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveOrUpdatePerson(Person person)
        {
            Person p = db.Persons.Find(person.ID);
            if (p != null)
            {
                UpdatePersonInUser(person);
            }
            else
            {
                User u = GetByID(person.UserID);
                u.people.Add(person);
                Update(u);
            }
        }

        public void DeletePerson(Person person)
        {
            person = db.Persons.Find(person.ID);
            string UserID = person.UserID;
            db.Persons.Remove(person);
            db.SaveChanges();
        }

        private void AddPerson(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }

        public void AddPicture(User item)
        {
            User itemsInDb = GetByID(item.ID);
            itemsInDb.Image = item.Image;
            itemsInDb.PictureName = item.PictureName;
            db.Entry(itemsInDb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
