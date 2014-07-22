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

    public class MappingConfig : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOptional(e => e.BillingAddress).WithRequired(e => e.Customer);
            modelBuilder.Entity<Company>().HasOptional(e => e.MailingAddress).WithRequired(e => e.Company);
        }
    }
}
