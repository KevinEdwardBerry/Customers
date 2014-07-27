using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Domain;
using Customers.Data;
using Highway.Data;
using Highway.Data.Contexts;
using System.Data.Entity;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class When_Accessing_The_Database_From_The_Repo
    {
        private Customer customer1;
        private Customer customer2;
        private Customer customer3;
        private InMemoryDataContext context;
        private Repository hwyRepo;

        [SetUp]
        public void Setup()
        {
            context = new InMemoryDataContext();
            hwyRepo = new Repository(context);

            customer1 = new Customer()
            {
                Id = 1,
                FirstName = "ajim",
                LastName = "bob",
                Company = new Company("aCompany") { Id = 1 },
                Email = "ajim.bob@gmail.com",
                Phone = "(555)123-1142",
                BillingAddress = new CustomerBillingAddress()
                {
                    Street1 = "123 some street",
                    City = "awesome city",
                    State = "TX",
                    ZipCode = "75001"
                }
            };

            customer2 = new Customer()
            {
                Id = 2,
                FirstName = "bjim",
                LastName = "bob",
                Company = new Company("bCompany") { Id = 2 },
                Email = "bjim.bob@gmail.com",
                Phone = "(555)123-1242",
                BillingAddress = new CustomerBillingAddress()
                {
                    Street1 = "223 some street",
                    City = "awesome city",
                    State = "TX",
                    ZipCode = "75001"
                }
            };

            customer3 = new Customer()
            {
                Id = 3,
                FirstName = "cjim",
                LastName = "bob",
                Company = new Company("cCompany") { Id = 3 },
                Email = "cjim.bob@gmail.com",
                Phone = "(555)123-1342",
                BillingAddress = new CustomerBillingAddress()
                {
                    Street1 = "323 some street",
                    City = "awesome city",
                    State = "TX",
                    ZipCode = "75001"
                }
            };

            hwyRepo.Context.Add(customer1);
            hwyRepo.Context.Add(customer2);
            hwyRepo.Context.Add(customer3);
            hwyRepo.Context.Commit();
        }

        [Test]
        public void One_Should_Be_Able_To_Get_A_List_Of_All_The_Customers()
        {
            var allCustomers = hwyRepo.Find(new GetAllCustomers()).ToList();
            //var allCustomers = hwyRepo.Find(new FindCustomerByLastName("bob")).ToList();
            Assert.AreEqual(3, allCustomers.Count);
        }

        [Test] // Repo is not working because InContextDataMemory is not global scope
        public void One_Should_Be_Able_To_Get_A_List_Of_All_The_Companies_From_The_Database()
        {
            //var allCompanies = repo.GetAllCompanies();
            var allCompanies = hwyRepo.Find(new GetAllCompanies()).ToList();
            Assert.AreEqual(3, allCompanies.Count);
        }
    }
}
