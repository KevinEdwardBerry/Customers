﻿using System;
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
    [Authorize]
    public class HomeController : Controller
    {
        IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index(int orderBy = 0)
        {
            ViewBag.OrderBy = orderBy;
            return View();
        }

        public ViewResult AddCustomer()
        {
            return View("AddCustomer", new CustomerModel());
        }

        public ViewResult NewCustomerConfirmation()
        {
            return View("NewCustomerConfirmation");
        }

        public ActionResult DeleteCurrentCustomer(int id)
        {
            if (id > 0)
            {
                var customer = _repo.Find(new GetCustomerById(id)).First();

                customer.BillingAddress = _repo.Find(new GetBillingAddressById(customer.Id)).First();
                customer.Company = _repo.Find(new GetCompanyById(customer.Id)).First();

                if (customer.Id > 0)
                {
                    _repo.Context.Remove(customer.BillingAddress);
                    _repo.Context.Remove(customer.Company);
                    _repo.Context.Remove(customer);
                    _repo.Context.Commit();
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
                _repo.Context.Add(customer);
                _repo.Context.Commit();

                return View("NewCustomerConfirmation", model);
            }

            return View("AddCustomer", model);
        }

        public ActionResult EditCurrentCustomer(int id)
        {
            if (id > 0)
            {
                var customer = _repo.Find(new GetCustomerById(id)).First();

                customer.Company = _repo.Find(new GetCompanyById(customer.Id)).First();
                customer.BillingAddress = _repo.Find(new GetBillingAddressById(customer.Id)).First();

                var model = new CustomerModel()
                {
                    LastName = customer.LastName,
                    CompanyName = customer.Company.Name,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    Phone = customer.Phone,
                    Street1 = customer.BillingAddress.Street1,
                    Street2 = customer.BillingAddress.Street2,
                    City = customer.BillingAddress.City,
                    State = customer.BillingAddress.State,
                    Zip = customer.BillingAddress.ZipCode,
                    Id = customer.Id,
                };

                return View("EditCustomer", model);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult ViewDetails(CustomerModel model)
        {
            return View("Details", model);
        }

        [HttpPost]
        public ActionResult SubmitEditedCustomer(CustomerModel model)
        {
            var customer = _repo.Find(new GetCustomerById(model.Id)).First();

            customer.Company = _repo.Find(new GetCompanyById(customer.Id)).First();
            customer.BillingAddress = _repo.Find(new GetBillingAddressById(customer.Id)).First();

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
                _repo.Context.Commit();
            }

           return View("Index");
        }
    }
}