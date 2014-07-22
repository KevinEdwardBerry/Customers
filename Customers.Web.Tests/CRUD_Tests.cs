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

namespace Customers.Web.Tests
{
    
    [TestFixture]
    public class CRUD_Tests
    {

        //private InMemoryDataContext context;
        //private IRepository repo;
        //private HomeController controller;

        [SetUp]
        public void Setup()
        {
            //context = new InMemoryDataContext();
            //repo = new Repository(context);
            //controller = new HomeController(repo);
        }

        [Test]
        public void When_Creating_A_New_Customer_It_Should_Persist_To_The_Database()
        {
            // Arrange
            var connectionString = "Server=.;Database=HighwayDemo;Integrated Security=true";
            var mappingConfig = new MappingConfig();
            var context = new DataContext(connectionString, mappingConfig);
            var repo = new Repository(context);

            // Act
            repo.Context.Add(new Customer
            {
                FirstName = "Kevin",
                LastName = "Berry",
                Company = new Company("Improving Enterprises"),
                Email = "kevin.berry@improvingenterprises.com",
                Phone = "(555)123-4567"
            });
            repo.Context.Commit();

            // Assert
            var customerK = repo.Find(new FindCustomerByFirstName("Kevin"));
            Assert.AreEqual("Kevin", customerK.First().FirstName);
        }

        [Test]
        public void When_Getting_A_Customer_From_The_Database_Appropriate_Information_Should_Be_Accessible()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void When_Updating_The_Information_Of_A_Customer_The_New_Information_Should_Persist()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void When_A_Customer_Is_Removed_From_The_Database_They_Should_No_Longer_Be_Found_In_The_Database()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(1, 1);
        }
    }

    public class FindCustomerByFirstName : Query<Customer>
    {
        public FindCustomerByFirstName(string firstName)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.FirstName == firstName);
        }
    }

    public class MappingConfig : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOptional(e => e.BillingAddress).WithRequired(e => e.Customer);
            modelBuilder.Entity<Company>().HasOptional(e => e.MailingAddress).WithRequired(e => e.Company);
        }
    }
}
