using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.Domain;
using Customers.Web.Models;
//using Customers.Data;
//using Highway.Data;
//using System.Data.Entity;

namespace Customers.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult AddCustomer()
        {
            return View("AddCustomer", new CustomerModel(new Repo()));
        }

        public ViewResult NewCustomerConfirmation()
        {
            return View("NewCustomerConfirmation");
        }

        [HttpPost]
        public ActionResult SubmitNewCustomer(CustomerModel model)
        {
            var customer = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Company = new Company(model.CompanyName),
                Email = model.Email,
                Phone = model.Phone
            };

            customer.BillingAddress = new CustomerBillingAddress()
            {
                Street1 = model.Street1,
                Street2 = model.Street2,
                City = model.City,
                State = model.State,
                ZipCode = model.Zip
            };

            model.Repository.AddCustomer(customer);

            return View("NewCustomerConfirmation", model);
        }
    }
}