//using Autofac;
//using Autofac.Integration.Mvc;
//using Customers.Domain;
//using Highway.Data;
//using Highway.Data.Contexts;
//using System.Web.Mvc;

//namespace Customers.Web.App_Start
//{
//    public class ResolverConfig
//    {
//        public static void Register()
//        {
//            Customer instance = new Customer();
//            CreateFakeData();

//            var builder = new ContainerBuilder();
//            builder.RegisterControllers(typeof(ResolverConfig).Assembly);
//            builder.Register<Customer>(c => instance);
//            var container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
//        }

//        private static void CreateFakeData()
//        {
//            InMemoryDataContext context = new InMemoryDataContext();
//            IRepository repo = new Repository(context);

//            var improving = new Company("Improving Enterprises");
//            var dryfire = new Company("DryFire USA");

//            var myCustomer = new Customer
//            {
//                FirstName = "Kevin",
//                LastName = "Berry",
//                Company = improving,
//                Email = "kevin.berry@improvingenterprises.com",
//                Phone = "(555)123-4567",
//            };
//            var billing = new CustomerBillingAddress
//            {
//                Customer = myCustomer,
//                Street1 = "123 SomeStreet",
//                City = "Awesome City",
//                State = "TX",
//                ZipCode = "75100"
//            };
//            myCustomer.BillingAddress = billing;

//            repo.Context.Add(myCustomer);
//            repo.Context.Commit();

//            var myCustomer2 = new Customer
//            {
//                FirstName = "Bob",
//                LastName = "Ridge",
//                Company = dryfire,
//                Email = "askbob@dryfireus.com",
//                Phone = "(555)321-4568",
//            };
//            var billing2 = new CustomerBillingAddress
//            {
//                Customer = myCustomer2,
//                Street1 = "321 SomeOtherStreet",
//                City = "Awesome Other City",
//                State = "TX",
//                ZipCode = "75168"
//            };
//            myCustomer2.BillingAddress = billing2;

//            repo.Context.Add(myCustomer2);
//            repo.Context.Commit();
//        }
//    }
//}
