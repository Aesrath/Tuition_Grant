using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P13.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P13.Controllers
{
    [Route("api/Exercise")]
    public class ExerciseApiController : Controller
    {
        private List<Order> orders = new List<Order>()
        {
            new Order { OrderId="4L9718",
                OrderDate =Utils.ConvertDate("01-01-2017"),
                ProductId ="MO051",
                Price =142.6,
                Qty =1 },

new Order { OrderId="9K4726", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="PF979", Price=97.7, Qty=23 },
new Order { OrderId="9L0330", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="SF815", Price=67.0, Qty=14 },
new Order { OrderId="2L5481", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="CZ854", Price=29.8, Qty=20 },
new Order { OrderId="9I4742", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="PF979", Price=111.9, Qty=8 },
new Order { OrderId="9L8034", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="SF815", Price=171.0, Qty=18 },
new Order { OrderId="1L5522", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="YW373", Price=166.2, Qty=1 },
new Order { OrderId="0L8013", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="YW373", Price=167.1, Qty=44 },
new Order { OrderId="8I6046", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="CZ854", Price=135.1, Qty=11 },
new Order { OrderId="8J8888", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="PF979", Price=121.2, Qty=21 }
        };

        [HttpGet("ByProductId")]
        public IActionResult GetOrdersSummaryList()
        {
			// TODO P13 Exercise 2a:
			// Prepare a model which is a list of objects having properties described in the 6P part 1 slides
			var model = orders.OrderBy(o => o.ProductId)
							.GroupBy(o => o.ProductId)
							.Select(g => new
							{
								productId = g.Key,
								numberOfOrders = g.Count()
							});
				
            // TODO P13 Exercise 2b:
            // Return the model with a Http response code Ok

            return Ok(model); // this line of code has to be replaced
        }
    }
}
