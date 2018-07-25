using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P07PT.Models;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P07PT.Controllers
{
	public class AdminController : Controller
	{
		// TODO P07 Task 2: Implement user security for this web app
		// Feature to complete include:
		//  1. Secure all actions following requirements as described in problem statement
		//  2. Login and logout functionality, should display the current user name
		//  3. Dynamic menu - following requirements as described in problem statement
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult DishesIndex()
		{
			List<dynamic> model = DBUtl.GetList(
											@"SELECT d.Id, d.Name, d.Price, 
                                                d.Recommended, d.Availability, c.Name as CuisineName
                                                FROM Dish d, Cuisine c
                                                WHERE d.CuisineId = c.Id");
			ViewBag.Message = TempData["Message"];

			return View(model);
		}

		// TODO P07 Task 1b: Implement AddDish HttpGet action
		// The following AddDish HttpGet action is to be replaced by your own implementation
		[HttpPost]
		public IActionResult AddDish(Dish newDish)
		{
			if (ModelState.IsValid)
			{

				if (DBUtl.ExecSQL(@"INSERT INTO Dish (Id, Name, Price, Recommended, Availability, CuisineId) VALUES 
({0}, '{1}', {2}, {3}, {4}, {5})", newDish.Id, newDish.Name, newDish.Price, newDish.Recommended, newDish.Availability, newDish.CuisineId) == 1)
				{
					TempData["Msg"] = "New dish added.";
				}
				else
				{
					TempData["Msg"] = "Failed to add new dish" + DBUtl.DB_Message;
				}
			}
			else
			{
				TempData["Msg"] = "Invalid information entered!";
			}
			return RedirectToAction("Index");
		}
		
		// TODO P07 Task 1c: Create and implement AddDish.cshtml view
		// Right .... to add new....

		// TODO P07 Task 1d: Implement AddDish HttpPost action

		public IActionResult Reports()
		{
			return View();
		}
	}
}

