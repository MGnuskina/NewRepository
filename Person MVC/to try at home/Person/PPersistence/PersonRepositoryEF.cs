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
            //if (item.phonenumbers != null)///
            //{
                foreach (var pn in item.phonenumbers)
                {
                    db.PhoneNumbers.Add(pn);
                }
            //}
            //if (item.addresses != null)///
            //{
                foreach (var ad in item.addresses)
                {
                    db.Addresses.Add(ad);
                }
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
            IEnumerable<Person> people= db.Persons.ToList();
            return people;
        }

        public Person GetByID(int id)
        {
            return db.Persons.Find(id);
        }

        public void Update(Person item)
        {
            foreach (var element in item.phonenumbers)
            {
                UpdatePhoneNumber(element);
            }
            foreach (var element in item.addresses)
            {
                UpdateAddress(element);
            }
            db.SaveChanges();
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            //db.Set<Person>().AddOrUpdate(item);
            db.SaveChanges();
            
        }

        private void UpdatePhoneNumber(PhoneNumbers phone)
        {
            db.Set<PhoneNumbers>().AddOrUpdate(phone);
            //db.Entry(phone).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        private void UpdateAddress(Address address)
        {
            db.Set<Address>().AddOrUpdate(address);
            //db.Entry(address).State = System.Data.Entity.EntityState.Modified;
            //db.Entry(address.street).State = System.Data.Entity.EntityState.Modified;
            //db.Entry(address.street.city).State = System.Data.Entity.EntityState.Modified;
            //db.Entry(address.street.city.country).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        
    }
}
