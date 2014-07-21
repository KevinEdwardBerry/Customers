using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Customers.Startup))]
namespace Customers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
