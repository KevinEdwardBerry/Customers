using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address BillingAddress { get; set; }
    }
}
