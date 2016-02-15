using System;
//using NUnit.Framework;
using PService;
using PPersistence;
using PDomain;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace TestMockPNUnit
{
    [TestFixture]
    public class RepositoryTest
    {
        #region People
        PDomain.Person persone = new PDomain.Person()
        {
            ID = 1,
            FirstName = "Lend",
            LastName = "kkk",
            Age = 66,
            phonenumbers = new List<PhoneNumbers>()
            {
                new PhoneNumbers()
                {
                    ID=2,
                    PhoneNumber="23423345",
                    PhoneNumberType="3141fs"
                }
            }
        };

        List<PDomain.Person> people = new List<PDomain.Person>()
        {
            new PDomain.Person()
            {
                ID=1,
                FirstName="Name",
                LastName="LName",
                Age=10,
                phonenumbers=new List<PhoneNumbers>()
                {
                    new PhoneNumbers()
                    {
                        ID=344,
                        PhoneNumber="235434",
                        PhoneNumberType="jh;khasdf"
                    }
                }
            },
            new PDomain.Person()
            {
                ID=3,
                FirstName="Name",
                LastName="LName",
                Age=10,
                phonenumbers=new List<PhoneNumbers>()
                {
                    new PhoneNumbers()
                    {
                        ID=34,
                        PhoneNumber="235434",
                        PhoneNumberType="jh;khasdf"
                    }
                }
            }
        };
        #endregion

        [Test]
        public void RepositoryTestGetById()
        {
            var mock = new Mock<IRepository<PDomain.Person>>();
            mock.Setup(a => a.GetByID(persone.ID)).Returns(persone);
            PersonServices service = new PersonServices(mock.Object);
            PersonViewModel p = service.ToViewModel(persone);
            PersonViewModel personResult = service.GetById(persone.ID);
            Assert.AreEqual(p.ToString(), personResult.ToString());///
        }

        [Test]
        public void RepositoryTestCrate()
        {
            var mock = new Mock<IRepository<PDomain.Person>>();
            mock.Setup(a => a.Create(persone));
            mock.Setup(a => a.GetByID(persone.ID)).Returns(persone);
            PersonServices service = new PersonServices(mock.Object);
            PersonViewModel p = service.ToViewModel(persone);
            service.Add(p);
            PersonViewModel personSavedVM= service.GetById(persone.ID);
            PDomain.Person personSaved = service.FromViewModel(personSavedVM);
            Assert.AreEqual(personSavedVM.ToString(), p.ToString());
            Assert.AreEqual(personSaved.ToString(), persone.ToString());
        }

        [Test]
        public void RepositoryTestUpdate()
        {
            var mock = new Mock<IRepository<PDomain.Person>>();
            mock.Setup(a => a.Update(persone));
            mock.Setup(a => a.GetByID(persone.ID)).Returns(persone);
            PersonServices service = new PersonServices(mock.Object);
            PersonViewModel p = service.ToViewModel(persone);
            service.Update(p);
            PersonViewModel personSavedVM = service.GetById(persone.ID);
            PDomain.Person personSaved = service.FromViewModel(personSavedVM);
            Assert.AreEqual(personSavedVM.ToString(), p.ToString());
            Assert.AreEqual(personSaved.ToString(), persone.ToString());
        }

        [Test]
        public void RepositoryTestGetAll()
        {
            var mock = new Mock<IRepository<PDomain.Person>>();
            mock.Setup(a => a.GetAll()).Returns(people);
            PersonServices service = new PersonServices(mock.Object);
            List<PersonViewModel> p = service.ToListViewModel(people);
            List<PersonViewModel> personResult = service.ReadAll();
            Assert.AreEqual(p.ToString(), personResult.ToString());
        }

        ///TestDelete
    }
}