using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Customers.Web.Startup))]
namespace Customers.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
