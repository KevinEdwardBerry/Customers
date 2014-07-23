﻿using Customers.Domain;
using Customers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Web.Models;
using Customers.Web.Controllers;
using Highway.Data;

namespace Customers.Web.Models
{
    public class IndexModel
    {
        public IndexModel()
        {
            var connectionString = "Server=tcp:s4lin082lz.database.windows.net,1433;Database=customers_new_db;User ID=kevin@s4lin082lz;Password=Sup3erS3cureP4ssw0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            var mappingConfig = new MappingConfig();
            var context = new DataContext(connectionString, mappingConfig);
            var repo = new Repository(context);
            customers = repo.Find(new FindAll<Customer>()).ToList();
            companies = repo.Find(new FindAll<Company>()).ToList();
        }

        private List<Customer> customers = new List<Customer>();
        public List<Customer> Customers { get { return customers; } set { customers = value; } }

        private List<Company> companies = new List<Company>();
        public List<Company> Companies { get { return companies; } set { companies = value; } }
    }
}