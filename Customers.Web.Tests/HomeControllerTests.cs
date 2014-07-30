using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Customers.Web.Controllers;
using Customers.Web.Models;
using Highway.Data;
using Highway.Data.Contexts;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Repository repo;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            repo = new Repository(new InMemoryDataContext());
            controller = new HomeController(repo);
        }
        [Test]
        public void When_Viewing_The_Home_Page_The_Controler_Should_Return_The_Index()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void After_Creating_A_New_Customer_A_Confirmation_Should_Appear()
        {
            var result = controller.NewCustomerConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void When_Deleting_A_Customer_From_The_Database_You_Should_Be_Returned_To_The_Index()
        {
            var result = controller.DeleteCurrentCustomer(0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void When_Editing_A_Customer_From_The_Database_You_Should_Be_Returned_To_The_Index()
        {
            var result = controller.EditCurrentCustomer(0) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
