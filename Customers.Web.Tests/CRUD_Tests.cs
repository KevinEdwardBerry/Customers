using Customers.Web.Controllers;
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
    public class CRUD_Tests
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
            repo.Context.Add(new Customer
            {
                FirstName = "Kevin",
                LastName = "Berry",
                Company = new Company("Improving Enterprises"),
                Email = "kevin.berry@improvingenterprises.com",
                Phone = "(555)123-4567"
            });
            repo.Context.Commit();

            customerK = repo.Find(new FindCustomerByFirstName("Kevin")).First();
        }

        [Test]
        public void When_Creating_A_New_Customer_It_Should_Persist_To_The_Database()
        {
            // Arrange
            //var connectionString = "Server=.;Database=HighwayDemo;Integrated Security=true";
            //var mappingConfig = new MappingConfig();
            //var context = new DataContext(connectionString, mappingConfig);
            //var repo = new Repository(context);


            // ******** Test Read ********
            Assert.AreEqual("Kevin", customerK.FirstName);
        }

        [Test]
        public void When_Updating_The_Information_Of_A_Customer_The_New_Information_Should_Persist()
        {
            // ******** Test Update ********
            customerK.Phone = "(555)987-4321";
            repo.Context.Commit();
            var newPhoneCustomerK = repo.Find(new FindCustomerByFirstName("Kevin")).First();
            Assert.AreEqual("(555)987-4321", newPhoneCustomerK.Phone);
        }

        [Test]
        public void When_A_Customer_Is_Removed_From_The_Database_They_Should_No_Longer_Be_Found_In_The_Database()
        {
            // ******** Test Delete ********
            repo.Context.Remove(customerK);
            repo.Context.Commit();
            Assert.IsEmpty(repo.Find(new FindCustomerByFirstName("Kevin")));
        }
    }
}
