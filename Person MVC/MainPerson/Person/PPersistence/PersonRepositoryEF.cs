using PDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPersistence
{
    public class PersonRepositoryEF : IRepository<Person>
    {
        UserContextEntitySQL db = new UserContextEntitySQL();

        public PersonRepositoryEF()
        {
        }

        public void Create(Person item)
        {
            db.Persons.Add(item);
            foreach (var pn in item.phonenumbers)
            {
                db.PhoneNumbers.Add(pn);
            }
            //foreach (var ad in item.addresses)
            //{
            //    db.Addresses.Add(ad);
            //}
            db.SaveChanges();
        }

        public void Delete(Person item)
        {
            item = GetByID(item.ID);
            db.Persons.Remove(item);
            db.SaveChanges();
        }

        public IEnumerable<Person> GetAll()
        {
            IEnumerable<Person> people = db.Persons.ToList();
            return people;
        }

        public Person GetByID(int id)
        {
            return db.Persons.Find(id);
        }

        public void Update(Person item)//Костыль
        {
            int p = item.ID;
            foreach (var element in item.phonenumbers)
            {
                UpdatePhoneNumber(element);
            }
            foreach (var element in item.addresses)
            {
                UpdateAddress(element);
            }
            db.Persons.Remove(GetByID(p));
            db.SaveChanges();

        }

        private void UpdatePhoneNumber(PhoneNumbers phone)
        {
            if (phone.ID == 0)
            {
                db.PhoneNumbers.Add(phone);
            }
            db.SaveChanges();
        }

        private void UpdateAddress(Address address)
        {
            if (address.ID==0)
            {
                db.Addresses.Add(address);
            }
            db.SaveChanges();
        }

    }
}
