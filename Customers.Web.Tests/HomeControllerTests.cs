﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Customers.Web.Controllers;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void When_Viewing_The_Home_Page_The_Controler_Should_Return_The_Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void After_Creating_A_New_Customer_A_Confirmation_Should_Appear()
        {
            var controller = new HomeController();
            var result = controller.NewCustomerConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
