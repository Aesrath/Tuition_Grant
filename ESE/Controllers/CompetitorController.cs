using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using ESE.Models;

namespace ESE.Controllers
{
	public class CompetitorController : Controller
	{

		private AppDbContext _dbContext;



		public CompetitorController(AppDbContext dbContext)

		{

			_dbContext = dbContext;

		}



		public IActionResult Index()

		{

			DbSet<Competitor> dbs = _dbContext.Competitor;

			List<Competitor> model = dbs.ToList();

			return View(model);

		}



		public IActionResult Update(string id)

		{

			// Task A: Implement Update HttpGet action
			DbSet<Competitor> dbs = _dbContext.Competitor;
			Competitor competitor = dbs.Where(c => c.Id == id).FirstOrDefault();

			if (competitor == null)
			{
				TempData["Msg"] = "No such competitor";
				return RedirectToAction("index");
			}
			else
			{
				DbSet<SkillArea> dbsSkillArea = _dbContext.SkillArea;
				var lstSkillArea = dbsSkillArea.OrderBy(o => o.Id).ToList().Select(o =>
				{
					dynamic n = new ExpandoObject();
					n.value = o.Id;
					n.text = o.Area;
					return n;

				}).ToList();
				ViewData["levels"] = lstSkillArea;
				return View(competitor);
			}



		}



		[HttpPost]

		public IActionResult Update(Competitor cpt)

		{

			if (ModelState.IsValid)

			{

				// Task B: Complete Update HttpPost action
				DbSet<Competitor> dbs = _dbContext.Competitor;
				Competitor competitor = dbs.Where(a => a.Id == cpt.Id).FirstOrDefault();
				if (competitor == null)
				{
					TempData["Msg"] = "No such competitor";
					return RedirectToAction("Index");
				}
				else
				{
					competitor.Name = cpt.Name;
					competitor.SkillAreaId = cpt.SkillAreaId;
					competitor.IsFirstTime = cpt.IsFirstTime;
					if (_dbContext.SaveChanges() == 1)
						TempData["Msg"] = "Competitor updated!";
					else
						TempData["Msg"] = "Competitor update failed!";

				}



			}

			else

				TempData["Msg"] = "Invalid input entered";

			return RedirectToAction("Index");

		}
	}


}