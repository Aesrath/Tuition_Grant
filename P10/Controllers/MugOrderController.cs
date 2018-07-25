using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P10.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P10.Controllers
{
	public class MugOrderController : Controller
	{
		private AppDbContext _dbContext;

		public MugOrderController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[Authorize]
		public IActionResult Index()
		{
			DbSet<MugOrder> dbs = _dbContext.MugOrder;

			// TODO P10 Task 1-2 Modify code below so that only admin will see all orders and normal user will only see their own orders		
			// use the following code to help you:
			List<MugOrder> model = dbs.ToList(); // this code needs to be modified

			if (User.IsInRole("Admin"))
			{
				model = dbs.ToList();
			}
			else
			{
				var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				model = dbs.Where(mo => mo.CreatedBy == userid)
					 .ToList();
			}

			return View(model);
		}

		[Authorize]
		public IActionResult Create()
		{
			DbSet<Pokedex> dbs = _dbContext.Pokedex;
			var lstPokes =
				dbs.ToList<Pokedex>()
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

		[HttpPost]
		[Authorize]
		public IActionResult Create(MugOrder mugOrder)
		{
			if (ModelState.IsValid)
			{
				DbSet<MugOrder> dbs = _dbContext.MugOrder;
				var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				mugOrder.CreatedBy = userid;
				dbs.Add(mugOrder);
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

		[Authorize]
		public IActionResult Update(int id)
		{
			DbSet<MugOrder> dbs = _dbContext.MugOrder;
			MugOrder tOrder = dbs.Where(mo => mo.Id == id).FirstOrDefault();

			if (tOrder != null)
			{
				// other code...
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
				TempData["Msg"] = "Mug order not found!";
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[Authorize]
		public IActionResult Update(MugOrder mugOrder)
		{
			if (ModelState.IsValid)
			{
				DbSet<MugOrder> dbs = _dbContext.MugOrder;
				MugOrder tOrder = dbs.Where(mo => mo.Id == mugOrder.Id)
									 .FirstOrDefault();

				if (tOrder != null)
				{
					tOrder.Color = mugOrder.Color;
					tOrder.PokedexId = mugOrder.PokedexId;
					tOrder.Qty = mugOrder.Qty;

					if (_dbContext.SaveChanges() == 1)
						TempData["Msg"] = "Mug order updated!";
					else
						TempData["Msg"] = "Failed to update database!";
				}
				else
				{
					TempData["Msg"] = "Mug order not found!";
					return RedirectToAction("Index");
				}
			}
			else
			{
				TempData["Msg"] = "Invalid information entered";
			}
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult Delete(int id)
		{
			DbSet<MugOrder> dbs = _dbContext.MugOrder;

			// TODO P10 Task 2-2 Use Where and FirstOrDefault LINQ-to-Entities method to retrieve the target MugOrder object
			//MugOrder tOrder = null; // this line needs to be modified to achieve Task 2-2
			MugOrder tOrder = dbs.Where(mo => mo.Id == id)
				.FirstOrDefault();

			if (tOrder != null)
			{
				// TODO P10 Task 2-3 Use Remove method to remove the found MugOrder object from DbSet
				dbs.Remove(tOrder);
				if (_dbContext.SaveChanges() == 1)
					TempData["Msg"] = "Mug order deleted!";
				else
					TempData["Msg"] = "Failed to update database!";
			}
			else
			{
				TempData["Msg"] = "Mug order not found!";
			}
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult LoadMug(string color, int pokedexid)
		{
			// TODO P10 Task 3-1 Complete the following code to create a new MugOrder and assign the pokedexid and color to the model properties
			//MugOrder model = null; // this line needs to be modified to achieve Task 3-1
			MugOrder model = new MugOrder();
			model.Color = color;
			model.PokedexId = pokedexid;

			// TODO P10 Task 3-2 Use PartialView method to return _ShowMug partial view and pass model object as the model
			//return null; // this line needs to be modified to achieve Task 3-2
			return PartialView("_ShowMug", model);
		}
	}
}
