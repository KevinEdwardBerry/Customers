using Customers.Domain;
using Customers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Web.Models;
using Customers.Web.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Customers.Web.Models
{
    public class CustomerModel
    {
        public CustomerModel() : this(new FakeRepo())
        {
            //
        }
        public CustomerModel(IRepo repo)
        {
            Repository = repo;
        }

        public IRepo Repository { get; set; }
        private List<Customer> customers = new List<Customer>();
        public List<Customer> Customers { get { return customers; } set { customers = value; } }

        private List<Company> companies = new List<Company>();
        public List<Company> Companies { get { return companies; } set { companies = value; } }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}