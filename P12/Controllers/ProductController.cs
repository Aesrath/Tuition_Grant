using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P12.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace P12.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> products = new List<Product>()
        {
new Product { Id="MO051", Description="Broker Information Terminal" },

new Product { Id="PF979", Description="Pre-emptive Trading Floor" },
new Product { Id="SF815", Description="Interactive Unit Mark II" },
new Product { Id="CZ854", Description="Linux Client Platform" },
new Product { Id="KZ663", Description="Turbo Client Platform" },
new Product { Id="YW373", Description="General Trading Floor Mark II" },
new Product { Id="YW373", Description="Cyber Computer" },
        };

        public string Index()
        {
            string result = "<h2>All Products</h2>\n\n";
            foreach (Product product in products)
            {
                result += string.Format("Product ID: {0}\nDescription: {1}\n\n", product.Id, product.Description);
            }
            return result;

        }

        public string GetProduct(string Id)
        {
            Product product = (from p in products
                               where p.Id == Id
                               select p).FirstOrDefault();

            if (product == null)
            {
                return string.Format("Product {0} not found!", Id);
            }
            else
            {
                return string.Format("Product ID: {0}\nDescription: {1}", product.Id, product.Description);
            }
        }
    }
}