using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Domain;
using Customers.Web.Controllers;
using Highway.Data;
using Highway.Data.Contexts;
using Customers.Data;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class When_Submitting_A_New_Customer
    {
        private Customer newCustomer;
        private InMemoryDataContext context;
        private Repository repo;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            context = new InMemoryDataContext();
            repo = new Repository(context);

            newCustomer = new Customer()
            {
                FirstName = "K Bob",
                LastName = "Jr.",
                Company = new Company("make things, inc."),
                Email = "kbob@gmail.com",
                Phone = "(555)123-4567"
            };

            controller = new HomeController();
            controller.SubmitNewCustomer(newCustomer);

        }

        [Test]
        public void A_Valid_Customer_Should_Be_Added_To_The_Database()
        {
            var testCustomer = repo.Find(new FindCustomerByFirstName(newCustomer.FirstName)).First();
            Assert.AreEqual(newCustomer.FirstName, testCustomer.FirstName);
        }
    }
}
