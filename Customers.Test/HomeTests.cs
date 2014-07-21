using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customers.Web;

namespace Customers.Web.Tests
{
    [TestFixture]
    public class HomeTests
    {
        [Test]
        public void When_The_Home_Page_Is_Loaded_It_Should_Return_The_Index_View()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
