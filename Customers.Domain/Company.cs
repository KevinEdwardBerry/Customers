using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Domain
{
    public class Company
    {
        public Company()
        {
            //
        }
        public Company(string name) : this()
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public CompanyMailingAddress MailingAddress { get; set; }
    }
}
