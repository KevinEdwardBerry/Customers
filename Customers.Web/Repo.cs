using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain;
using Customers.Data;
using Highway.Data;
using Highway.Data.Contexts;
using System.Data.Entity;

namespace Customers.Web
{
    public interface IRepo
    {
        void AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        List<Company> GetAllCompanies();

        void Delete(Customer customer);
    }

    public class FakeRepo : IRepo
    {
        // THIS WILL NOT WORK BECAUSE IT WILL NEVER BE IN SCOPE OF TESTS
        private InMemoryDataContext context;
        private Repository repo;

        private void Init()
        {
            context = new InMemoryDataContext();
            repo = new Repository(context);
        }

        public void AddCustomer(Customer customer)
        {
            Init();
            repo.Context.Add(customer);
            repo.Context.Commit();
        }

        public List<Customer> GetAllCustomers()
        {
            Init();
            //return Data.GetAllCustomers().ToList();
            return repo.Find(new FindCustomerByLastName("bob")).ToList();
        }

        public List<Company> GetAllCompanies()
        {
            Init();
            return repo.Find(new FindAll<Company>()).ToList();
        }

        public void Delete(Customer customer)
        {
            Init();
            repo.Context.Remove(customer);
            repo.Context.Commit();
        }
    }

    public class Repo : IRepo
    {
        private string connectionString;
        private MappingConfig mappingConfig;
        private DataContext context;
        private Repository repo;

        private void Init()
        {
            connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            mappingConfig = new MappingConfig();
            context = new DataContext(connectionString, mappingConfig);
            repo = new Repository(context);
        }

        public void AddCustomer(Customer customer)
        {
            Init();
            repo.Context.Add(customer);
            repo.Context.Commit();
        }

        public List<Customer> GetAllCustomers()
        {
            Init();
            return repo.Find(new FindAll<Customer>()).ToList();
        }

        public List<Company> GetAllCompanies()
        {
            Init();
            return repo.Find(new FindAll<Company>()).ToList();
        }

        public void Delete(Customer customer)
        {
            Init();
            repo.Context.Remove(customer);
            repo.Context.Commit();
        }
    }
}