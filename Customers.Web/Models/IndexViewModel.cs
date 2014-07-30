using Customers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers.Web.Models
{
    public class IndexViewModel
    {
        public int OrderBy { get; set; }

        private List<Customer> _customers = new List<Customer>();
        public List<Customer> Customers { get { return _customers; } set { _customers = value; } }
    }
}