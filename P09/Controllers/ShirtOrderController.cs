using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P09.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace P09.Controllers
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
			List<ShirtOrder> model = _dbContext.ShirtOrder.ToList();
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
				DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;
				shirtOrder.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				dbs.Add(shirtOrder);
				if (_dbContext.SaveChanges() == 1)
					TempData["Msg"] = "New order added!";
				else
					TempData["Msg"] = "Failed to update database!";
			}
			else
			{
				TempData["Msg"] = "Invalid information entered";
			}
			return RedirectToAction("Index");
		}

		// Update actions yet to be implemented

		[Authorize]
		public IActionResult Update(int id)
		{
			// TODO P09 Task 3-3: Assign dbs to _dbContext.ShirtOrder
			DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;

			// TODO P09 Task 3-4: User dbs.Where and FirstOrDefault methods to retrieve the targetted ShirtOrder
			ShirtOrder tOrder = dbs.Where(mo => mo.Id == id).FirstOrDefault();

			if (tOrder != null)
			{
				DbSet<Pokedex> dbsPoke = _dbContext.Pokedex;
				var lstPokes =
						dbsPoke.ToList<Pokedex>()
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
				// TODO P09 Task 3-5: Repeat task 3-3 and 3-4
				DbSet<ShirtOrder> dbs = _dbContext.ShirtOrder;
				ShirtOrder tOrder = dbs.Where(mo => mo.Id == shirtOrder.Id).FirstOrDefault();

				if (tOrder != null)
				{
					// TODO P09 Task 3-6: Assign ShirtOrder's property values to tOrder's properties
					// Note: Assign current login user's Id to CreatedBy column
					tOrder.Name = shirtOrder.Name;
					tOrder.Qty = shirtOrder.Qty;
					tOrder.Price = shirtOrder.Price;
					tOrder.Color = shirtOrder.Color; // this is done for you
					tOrder.PokedexId = shirtOrder.PokedexId;
					tOrder.FrontPosition = shirtOrder.FrontPosition;
					// TODO P09 Task 3-7: Update database using _dbContext and display appropriate message
					if (_dbContext.SaveChanges() == 1)
					{
						TempData["Msg"] = "Shirt order updated!";
					}
					else
					{
						TempData["Msg"] = "Failed to update database!";
					}

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
	}

}

