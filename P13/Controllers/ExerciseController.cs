using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P13.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P13.Controllers
{

    public class ExerciseController : Controller
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

        public IActionResult ExercisePartialPageUpdate()
        {
            var model = new List<Order>();
            return View(model);
        }

        public IActionResult GetOrderListByProductId(string id)
        {
			// TODO P13 Exercise 1a:
			// Prepare a model which is a list of orders filterd by id and sorted by order date and order id 
			// in descending order
			var model = orders.Where(o => o.ProductId == id)
								.OrderByDescending(o => o.OrderDate)
								.ThenByDescending(o => o.OrderId)
								.ToList();

			// TODO P13 Exercise 1b:
			// Return partial view _OrderList passing the prepared model to it
			return PartialView("_OrderList", model); // This line of code to be replaced
        }

        public IActionResult ExerciseWebAPI()
        {
            return View();
        }
    }
}
