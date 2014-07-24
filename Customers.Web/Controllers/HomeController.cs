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

        public ActionResult DeleteCurrentCustomer(int id)
        {
            if (id > 0)
            {
                var connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                var mappingConfig = new MappingConfig();
                var context = new DataContext(connectionString, mappingConfig);
                var repo = new Repository(context);

                var customers = repo.Find(new GetAllCustomers()).ToList();
                var customer = customers.Where(c => c.Id == id).First();

                if (customer.Id > 0)
                {
                    repo.Context.Remove(customer);
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

            if (ModelState.IsValid)
            {
                model.Repository.AddCustomer(customer);
                return View("NewCustomerConfirmation", model);
            }

            return View("AddCustomer", model);
        }

        public ActionResult EditCurrentCustomer(int id)
        {
            if (id > 0)
            {
                var connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                var mappingConfig = new MappingConfig();
                var context = new DataContext(connectionString, mappingConfig);
                var repo = new Repository(context);

                var customers = repo.Find(new GetAllCustomers()).ToList();
                var customer = customers.Where(c => c.Id == id).First();

                customer.Company = repo.Find(new GetCompanyById(customer.Id)).First();
                customer.BillingAddress = repo.Find(new GetBillingAddressById(customer.Id)).First();

                var model = new CustomerModel(new Repo());
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.CompanyName = customer.Company.Name;
                model.Email = customer.Email;
                model.Phone = customer.Phone;
                model.Street1 = customer.BillingAddress.Street1;
                model.Street2 = customer.BillingAddress.Street2;
                model.City = customer.BillingAddress.City;
                model.State = customer.BillingAddress.State;
                model.Zip = customer.BillingAddress.ZipCode;
                model.Id = customer.Id;

                return View("EditCustomer", model);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult SubmitEditedCustomer(CustomerModel model)
        {
            var connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            var mappingConfig = new MappingConfig();
            var context = new DataContext(connectionString, mappingConfig);
            var repo = new Repository(context);

            var customers = repo.Find(new GetAllCustomers()).ToList();
            var customer = customers.Where(c => c.Id == model.Id).First();

            customer.Company = repo.Find(new GetCompanyById(customer.Id)).First();
            customer.BillingAddress = repo.Find(new GetBillingAddressById(customer.Id)).First();

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            if (customer.Company.Name != model.CompanyName)
                customer.Company = new Company(model.CompanyName);
            customer.Email = model.Email;
            customer.Phone = model.Phone;

            customer.BillingAddress.Street1 = model.Street1;
            customer.BillingAddress.Street2 = model.Street2;
            customer.BillingAddress.City = model.City;
            customer.BillingAddress.State = model.State;
            customer.BillingAddress.ZipCode = model.Zip;

            if (ModelState.IsValid)
            {
                repo.Context.Commit();
            }

           return View("Index");
        }
    }
}