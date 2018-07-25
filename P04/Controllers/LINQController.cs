using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P04.Models;

namespace Week04.Controllers
{
    public class LINQController : Controller
    {
         public ActionResult FilterByPrice(double id)
        {
            ViewBag.Title = $"All orders with price > {id}";
            Sales sales = new Sales();
            List<Order> orders = sales.GetOrderHistorySimplified();

            // TODO Task 4a: Use LINQ method chaining to filtered orders list by price greater than id
            List<Order> model = orders.Where(x => x.Price > id).ToList<Order>(); // modify this line

            return View("Display",model);
        }

        public ActionResult SortedByPrice()
        {
            ViewBag.Title = $"All orders sorted by Price in ascending order";
            Sales sales = new Sales();
            List<Order> orders = sales.GetOrderHistorySimplified();

            // TODO Task 4b: Use LINQ method chaining to sort orders list by price in ascending order
            List<Order> model = orders.OrderBy(x => x.Price).ToList<Order>(); // modify this line

            return View("Display", model);
        }

        public ActionResult FilterednSorted(double id)
        {
            ViewBag.Title = $"All orders with price > {id} and sorted by price asc and ProductId desc";
            Sales sales = new Sales();
            List<Order> orders = sales.GetOrderHistorySimplified();

            // TODO Task 4c: Use LINQ method chaining to filter orders list by price > id and sort orders list by price in ascending order and then by ProductId in descending order
            List<Order> model = orders.Where(x => x.Price>id)
									.OrderBy(x => x.Price)
									.ThenByDescending(x => x.ProductId).ToList<Order>(); // modify this line

            return View("Display", model);
        }

    }
}