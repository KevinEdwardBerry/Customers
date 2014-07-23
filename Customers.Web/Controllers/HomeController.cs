using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.Domain;
using Customers.Data;

namespace Customers.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult AddCustomer()
        {
            return View("AddCustomer");
        }

        public ViewResult NewCustomerConfirmation()
        {
            return View("NewCustomerConfirmation");
        }

        public ActionResult SubmitNewCustomer(Customer newCustomer)
        {
            return View("NewCustomerConfirmation");
        }
    }
}