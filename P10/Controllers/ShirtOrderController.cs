using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P10.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P10.Controllers
{
    public class ShirtOrderController : Controller
    {
        private AppDbContext _dbContext;

        public ShirtOrderController(AppDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            // modify this code so that only admin see all orders and normal users only see their own orders
            DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;
			List<ShirtOrder> model = dbs.ToList();

			if (User.IsInRole("Admin"))
			{
				model = dbs.ToList();
			}
			else
			{
				var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				model = dbs.Where(so => so.CreatedBy == userid)
					.ToList();
			}
			return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            var lstPokes =
                _dbContext.Pokedex
                    .ToList<Pokedex>()
                    .OrderBy(p => p.Name)
                    .Select(
                        p =>
                        {
                            dynamic d = new ExpandoObject();
                            d.value = p.Id;
                            d.text = p.Name;
                            return d;
                        }
                    )
                    .ToList<dynamic>();
            ViewData["pokes"] = lstPokes;

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ShirtOrder shirtOrder)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ShirtOrder.Add(shirtOrder);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shirtOrder);
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;
            ShirtOrder tOrder = dbs.Where(mo => mo.Id == id).FirstOrDefault();

            if (tOrder != null)
            {
                var lstPokes =
                    _dbContext.Pokedex
                        .ToList<Pokedex>()
                        .OrderBy(p => p.Name)
                        .Select(
                            p =>
                            {
                                dynamic d = new ExpandoObject();
                                d.value = p.Id;
                                d.text = p.Name;
                                return d;
                            }
                        )
                        .ToList<dynamic>();
                ViewData["pokes"] = lstPokes;

                return View(tOrder);
            }
            else
            {
                TempData["Msg"] = "Shirt order not found!";
                return RedirectToAction("Index");
            }
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Update(ShirtOrder shirtOrder)
        {
            if (ModelState.IsValid)
            {
                DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;
                ShirtOrder tOrder = dbs.Where(mo => mo.Id == shirtOrder.Id).FirstOrDefault();

                if (tOrder != null)
                {
                    tOrder.Name = shirtOrder.Name;
                    tOrder.Color = shirtOrder.Color;
                    tOrder.PokedexId = shirtOrder.PokedexId;
                    tOrder.Qty = shirtOrder.Qty;
                    tOrder.Price = shirtOrder.Price;
                    tOrder.FrontPosition = shirtOrder.FrontPosition;

                    if (_dbContext.SaveChanges() == 1)
                        TempData["Msg"] = "Shirt order updated!";
                    else
                        TempData["Msg"] = "Failed to update database!";
                }
                else
                {
                    TempData["Msg"] = "Shirt order not found!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Msg"] = "Invalid information entered";
            }
            return RedirectToAction("Index");
        }


		// Implement Delete action and LoadShirt action
		[Authorize]
		public IActionResult LoadShirt(string color, int pokedexid)
		{
			// TODO P10 Task 3-1 Complete the following code to create a new MugOrder and assign the pokedexid and color to the model properties
			//MugOrder model = null; // this line needs to be modified to achieve Task 3-1
			ShirtOrder model = new ShirtOrder();
			model.Color = color;
			model.PokedexId = pokedexid;

			// TODO P10 Task 3-2 Use PartialView method to return _ShowMug partial view and pass model object as the model
			//return null; // this line needs to be modified to achieve Task 3-2
			return PartialView("_ShowShirt", model);
		}
	}
}
