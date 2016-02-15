using System;
using NUnit.Framework;
using Person;
using System.Web;
using System.Web.Mvc;
using PService;
using Moq;
using System.Collections.Generic;
using Person.Controllers;
using PDomain;
using PPersistence;

namespace PersonControllerTest
{
    [TestFixture]
    public class ControllerTest
    {
        #region people
        List<PersonViewModel> people1 = new List<PersonViewModel>()
        {
            new PersonViewModel()
            {
                Id = 1,
                FirstName = "qqqq",
                LastName="eeee",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    }
                }
            }
        };

        List<PersonViewModel> people2 = new List<PersonViewModel>()
        {
            new PersonViewModel()
            {
                Id = 1,
                FirstName = "qqqq",
                LastName="eeee",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    }
                }
            },
            new PersonViewModel()
            {
                Id = 2,
                FirstName = "q11",
                LastName="e",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    },
                    new PhoneNumberViewModel()
                    {
                        Id=12,
                        PhoneNumber="44345",
                        PhoneNumberType="ret"
                    }
                }
            }
        };

        List<PersonViewModel> people4 = new List<PersonViewModel>()
        {
            new PersonViewModel()
            {
                Id = 1,
                FirstName = "qqqq",
                LastName="eeee",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    }
                }
            },
            new PersonViewModel()
            {
                Id = 11,
                FirstName = "qqqq",
                LastName="eeee",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    }
                }
            },
             new PersonViewModel()
            {
                Id = 13,
                FirstName = "qqqq",
                LastName="eeee",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    }
                }
            },
            new PersonViewModel()
            {
                Id = 2,
                FirstName = "q11",
                LastName="e",
                Age=10,
                PhoneNumbers=new List<PhoneNumberViewModel>()
                {
                    new PhoneNumberViewModel()
                    {
                        Id=11,
                        PhoneNumber="44444",
                        PhoneNumberType="werqwe"
                    },
                    new PhoneNumberViewModel()
                    {
                        Id=12,
                        PhoneNumber="44345",
                        PhoneNumberType="ret"
                    }
                }
            }
        };

        List<PersonViewModel> people0 = new List<PersonViewModel>();

        #endregion

        [Test]
        public void GetViewTest_Index0()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.ReadAll()).Returns(people0);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people0, result.Model);
        }

        [Test]
        public void GetViewTest_Index1()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.ReadAll()).Returns(people1);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people1, result.Model);
        }

        [Test]
        public void GetViewTest_Index2()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.ReadAll()).Returns(people2);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people2, result.Model);
        }

        [Test]
        public void GetViewTest_Index4()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.ReadAll()).Returns(people4);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people4, result.Model);
        }

        [Test]
        public void GetViewTest_Delete()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.Delete(people1[0]));
            mock.Setup(a => a.GetById(people1[0].Id)).Returns(people1[0]);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Delete(people1[0].Id) as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people1[0], result.Model);
        }

        [Test]
        public void GetViewTest_Edit()
        {
            var mock = new Mock<IService>();
            mock.Setup(a => a.Update(people1[0]));
            mock.Setup(a => a.GetById(people1[0].Id)).Returns(people1[0]);
            PersonsController controller = new PersonsController(mock.Object);

            ViewResult result = controller.Edit(people1[0].Id) as ViewResult;

            Assert.IsNotNull(result.Model);
            Assert.AreEqual(people1[0], result.Model);
        }

        //[Test]
        //public void GetViewTest_Add()
        //{
        //    var mock = new Mock<IService>();
        //    mock.Setup(a => a.Add(people1[0]));
        //    PersonsController controller = new PersonsController(mock.Object);

        //    ViewResult result = controller.AddPerson(people1[0], ) as ViewResult;

        //    Assert.IsNotNull(result.Model);
        //    Assert.AreEqual(people1[0], result.Model);
        //}
    }
}