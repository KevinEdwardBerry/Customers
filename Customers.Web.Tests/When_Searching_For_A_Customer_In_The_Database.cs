using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway.Data;
using Highway.Data.Contexts;
using System.Data.Entity;
using Customers.Domain;
using Customers.Data;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class When_Searching_For_A_Customer_In_The_Database
    {
        private InMemoryDataContext context;
        private IRepository repo;
        private Customer customerK;

        [SetUp]
        public void Setup()
        {
            context = new InMemoryDataContext();
            repo = new Repository(context);

            // All tests will fail if the Create does not work
            var myCustomer = new Customer
            {
                Id = 1,
                FirstName = "Kevin",
                LastName = "Berry",
                Company = new Company("Improving Enterprises"),
                Email = "kevin.berry@improvingenterprises.com",
                Phone = "(555)123-4567",
            };
            var billing = new CustomerBillingAddress
            {
                Customer = myCustomer,
                Street1 = "123 SomeStreet",
                City = "Awesome City",
                State = "TX",
                ZipCode = "75100"
            };
            myCustomer.BillingAddress = billing;

            repo.Context.Add(myCustomer);
            repo.Context.Commit();
        }

        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_First_Name()
        {
            customerK = repo.Find(new FindCustomerByFirstName("Kevin")).First();
            Assert.AreEqual("Kevin", customerK.FirstName);
        }

        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_Last_Name()
        {
            customerK = repo.Find(new FindCustomerByLastName("Berry")).First();
            Assert.AreEqual("Berry", customerK.LastName);
        }

        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_Email()
        {
            customerK = repo.Find(new FindCustomerByEmail("kevin.berry@improvingenterprises.com")).First();
            Assert.AreEqual("kevin.berry@improvingenterprises.com", customerK.Email);
        }

        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_Company_Name()
        {
            customerK = repo.Find(new FindCustomerByCompanyName("Improving Enterprises")).First();
            Assert.AreEqual("Improving Enterprises", customerK.Company.Name);
        }

        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_Phone()
        {
            customerK = repo.Find(new FindCustomerByPhone("(555)123-4567")).First();
            Assert.AreEqual("(555)123-4567", customerK.Phone);
        }

        [Test]
        public void You_Should_Be_Able_To_Get_All_Customers_In_The_Database()
        {
            var myCustomer2 = new Customer
            {
                FirstName = "Bob",
                LastName = "Ridge",
                Company = new Company("DryFire USA"),
                Email = "askbob@dryfireus.com",
                Phone = "(555)321-4568",
            };
            var billing2 = new CustomerBillingAddress
            {
                Customer = myCustomer2,
                Street1 = "321 SomeOtherStreet",
                City = "Awesome Other City",
                State = "TX",
                ZipCode = "75168"
            };
            myCustomer2.BillingAddress = billing2;

            repo.Context.Add(myCustomer2);
            repo.Context.Commit();

            var allCustomers = repo.Find(new FindAll<Customer>());
            Assert.AreEqual(2, allCustomers.Count());
        }
        
        [Test]
        public void You_Should_Be_Able_To_Find_The_Customer_By_Their_Id()
        {
            customerK = repo.Find(new GetCustomerById(1)).First();
            Assert.AreEqual(1, customerK.Id);
        }
    }
}
