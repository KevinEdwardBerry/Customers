using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway.Data;
using Highway.Data.Contexts;
using Customers.Domain;
using System.Data.Entity;

namespace Customers.Data
{
    public class FindCustomerByFirstName : Query<Customer>
    {
        public FindCustomerByFirstName(string firstName)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.FirstName == firstName);
        }
    }

    public class FindCustomerByLastName : Query<Customer>
    {
        public FindCustomerByLastName(string lastName)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.LastName == lastName);
        }
    }

    public class FindCustomerByCompanyName : Query<Customer>
    {
        public FindCustomerByCompanyName(string companyName)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.Company.Name == companyName);
        }
    }

    public class FindCustomerByEmail : Query<Customer>
    {
        public FindCustomerByEmail(string email)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.Email == email);
        }
    }

    public class FindCustomerByPhone : Query<Customer>
    {
        public FindCustomerByPhone(string phone)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.Phone == phone);
        }
    }

    public class GetAllCustomers : Query<Customer>
    {
        public GetAllCustomers()
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(e => e.LastName != "");
        }
    }
    
    public class GetAllCompanies : Query<Company>
    {
        public GetAllCompanies()
        {
            ContextQuery = c => c.AsQueryable<Company>().Where(e => e.Name != "");
        }
    }

    public class GetCompanyById : Query<Company>
    {
        public GetCompanyById(int id)
        {
            ContextQuery = c => c.AsQueryable<Company>().Where(e => e.Id == id);
        }
    }

    public class GetBillingAddressById :Query<CustomerBillingAddress>
    {
        public GetBillingAddressById(int id)
        {
            ContextQuery = c => c.AsQueryable<CustomerBillingAddress>().Where(e => e.Id == id);
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
