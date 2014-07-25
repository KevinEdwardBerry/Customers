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
        public CustomerModel() : this(new Repo())
        {
            // Changed to non-fake repo because fake repo was being passed to the controller, and
            // the fake repo will not work
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

        [Required(ErrorMessage="First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [Phone(ErrorMessage = "Must be a valid phone number")]
        public string Phone { get; set; }

        public int Id { get; set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}