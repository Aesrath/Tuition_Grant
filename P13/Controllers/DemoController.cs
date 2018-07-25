using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P13.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P13.Controllers
{
    public class DemoController : Controller
    {
        private List<Product> products = new List<Product>()
        {
            new Product { Id="MO051", Description="Broker Information Terminal", Qty=10 },
            new Product { Id="PF979", Description="Pre-emptive Trading Floor", Qty=20 },
            new Product { Id="SF815", Description="Interactive Unit Mark II", Qty=30 },
            new Product { Id="CZ854", Description="Linux Client Platform", Qty=15 },
            new Product { Id="KZ663", Description="Turbo Client Platform", Qty=25 },
            new Product { Id="YW373", Description="General Trading Floor Mark II", Qty=35 },
            new Product { Id="YW373", Description="Cyber Computer", Qty=50 },
        };

        public IActionResult DemoPartialView()
        {
            return View();
        }

        public IActionResult DemoPartialView2()
        {
            return View();
        }

        public IActionResult DemoPartialPageUpdate()
        {
            var model = products.OrderBy(p => p.Id)
                                .ToList();
            return View(model);
        }

        public IActionResult GetProductListSortedBy(int id)
        {
            var model = products;
            if (id == 0)
                model = products.OrderBy(p => p.Id)
                                .ToList();
            else if (id == 1)
                model = products.OrderBy(p => p.Description)
                                .ToList();
            return PartialView("_ProductList", model);
        }

        public IActionResult DemoWebAPI()
        {
            return View();
        }
    }
}
