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
        public Company(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public Address StreetAddress { get; set; }
    }
}
