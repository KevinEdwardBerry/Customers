using Autofac;
using Autofac.Integration.Mvc;
using Customers.Data;
using Customers.Domain;
using Customers.Web.Controllers;
using Highway.Data;
using Highway.Data.Contexts;
using System.Web.Mvc;

namespace Customers.Web.App_Start
{
    public class ResolverConfig
    {
        public void Register()
        {
            var connectionString = "Data Source=tcp:h14og81azd.database.windows.net,1433;Initial Catalog=customers_new_db;User ID=kevin@h14og81azd;Password=Sup3erS3cureP4ssw0rd";
            var mappingConfig = new MappingConfig();
            var context = new DataContext(connectionString, mappingConfig);
            var repo = new Repository(context);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(ResolverConfig).Assembly);
            builder.RegisterType<HomeController>();
            builder.RegisterType<MappingConfig>().AsImplementedInterfaces();
            builder.RegisterInstance<DataContext>(
                new DataContext(connectionString, mappingConfig))
                .AsImplementedInterfaces();
            builder.Register<DataContext>(c => new DataContext(connectionString, mappingConfig));
            builder.RegisterType<Repository>()
                .AsImplementedInterfaces();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
