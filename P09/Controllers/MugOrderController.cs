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
            List<MugOrder> model = dbs.ToList();

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

	[Authorize]
        [HttpPost]
        public IActionResult Create(MugOrder mugOrder)
        {
            if (ModelState.IsValid)
            {
                DbSet<MugOrder> dbs = _dbContext.MugOrder;
				mugOrder.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            // TODO P09 Task 3-3: Assign dbs to _dbContext.MugOrder
            DbSet<MugOrder> dbs = _dbContext.MugOrder;

            // TODO P09 Task 3-4: User dbs.Where and FirstOrDefault methods to retrieve the targetted MugOrder
            MugOrder tOrder = dbs.Where(mo => mo.Id == id).FirstOrDefault();

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
                TempData["Msg"] = "Mug order not found!";
                return RedirectToAction("Index");
            }
        }

	[Authorize]
        [HttpPost]
        public IActionResult Update(MugOrder mugOrder)
        {
            if (ModelState.IsValid)
            {
                // TODO P09 Task 3-5: Repeat task 3-3 and 3-4
                DbSet<MugOrder> dbs = _dbContext.MugOrder;
				MugOrder tOrder = dbs.Where(mo => mo.Id == mugOrder.Id).FirstOrDefault();

                if (tOrder != null)
                {
                    // TODO P09 Task 3-6: Assign mugOrder's property values to tOrder's properties
					 // Note: Assign current login user's Id to CreatedBy column
                    tOrder.Color = mugOrder.Color; // this is done for you
					tOrder.PokedexId = mugOrder.PokedexId;
					tOrder.Qty = mugOrder.Qty;
					tOrder.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    // TODO P09 Task 3-7: Update database using _dbContext and display appropriate message
					if (_dbContext.SaveChanges() == 1)
					{
						TempData["Msg"] = "Mug order updated!";
					}
					else
					{
						TempData["Msg"] = "Failed to update database!";
					}

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
    }
}
