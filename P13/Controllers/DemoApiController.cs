using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P13.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P13.Controllers
{
    [Route("api/Demo")]
    public class DemoApiController : Controller
    {
        private List<Product> products = new List<Product>()
        {
            new Product { Id="MO051", Description="Broker Information Terminal", Qty=10 },
            new Product { Id="PF979", Description="Pre-emptive Trading Floor", Qty=20 },
            new Product { Id="SF815", Description="Interactive Unit Mark II", Qty=30 },
            new Product { Id="CZ854", Description="Linux Client Platform", Qty=15 },
            new Product { Id="KZ663", Description="Turbo Client Platform", Qty=25 },
            new Product { Id="YW373", Description="General Trading Floor Mark II", Qty=35 }
        };

        [HttpGet("Now")]
        public IActionResult GetCurrentTime()
        {
            string strNow = String.Format("{0:dd/MM/yyyy hh:mm tt}", DateTime.Now);
            return Ok($"\"{strNow}\"");
        }

        [HttpGet("inventory/{id}")]
        public IActionResult GetStockQty(string id)
        {
            Product product = products.Where(p => p.Id == id)
                                        .FirstOrDefault();
            if (product != null)
                return Ok(product.Qty);
            else
                return NotFound();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            Product product = products.Where(p => p.Id == id)
                            .FirstOrDefault();
            if (product != null)
               return Ok(product);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpGet("productids")]
        public IActionResult GetProductIds()
        {
            List<string> result = products.Select(p => p.Id)
                                            .ToList();
            return Ok(result);
        }
    }
}
