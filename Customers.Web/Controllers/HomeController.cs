using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.Domain;
using Customers.Web.Models;
using Customers.Data;
using Highway.Data;
using System.Data.Entity;

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

        //[HttpDelete]
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                var mappingConfig = new MappingConfig();
                var context = new DataContext(connectionString, mappingConfig);
                var repo = new Repository(context);

                var customers = repo.Find(new GetAllCustomers()).ToList();

                if (customers.Count > 0)
                {
                    repo.Context.Remove(customers.Where(c => c.Id == id).First());
                    repo.Context.Commit();
                }
            }

            return View("Index");
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

        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            return View("Index");
        }
    }
}