using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace P07.Controllers
{
	public class PokemonController : Controller
	{
		static List<dynamic> data = DBUtl.GetList("SELECT * FROM Pokedex");

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AllRecordsProperties()
		{
			ViewData["Title"] = "All Records with All Properties";
			ViewData["Query"] = "No Query";
			return View("Results", data);
		}


		public IActionResult SortingLevels()
		{
			var result =
			   data.OrderBy(s => s.Type1)
				   .ThenBy(s => s.Type2)
				   .ThenByDescending(s => s.Name)
				   .ToList<dynamic>();

			ViewData["Title"] = "Sorting: Type1, Type2, Name(desc)";
			ViewData["Query"] = @"
var result =
   data.OrderBy(s => s.Type1)
       .ThenBy(s => s.Type2)
       .ThenByDescending(s => s.Name)
       .ToList<dynamic>();
";

			return View("Results", result);
		}

		public IActionResult Projection()
		{
			var model =
			   data.Select(
					   a =>
						  {
							  dynamic x = new ExpandoObject();
							  x.Name = a.Name;
							  x.Attack = a.Attack;
							  x.Defence = a.Defence;
							  x.Stamina = a.Stamina;
							  return x;
						  }
					)
				   .ToList<dynamic>();

			ViewData["Title"] = "Projection: Name, Attack, Defence, Stamina";
			ViewData["Query"] = @"
var model =
   data.Select(
           a =>
              {
                 dynamic x = new ExpandoObject();
                 x.Name = a.Name;
                 x.Attack = a.Attack;
                 x.Defence = a.Defence;
                 x.Stamina = a.Stamina;
                 return x;
              }
        )
       .ToList<dynamic>();
";
			return View("Results", model);
		}

		public IActionResult ProjectionSorting()
		{
			var result =
			   data.Select(
					   a =>
						  {
							  dynamic x = new ExpandoObject();
							  x.Name = a.Name;
							  x.Attack = a.Attack;
							  x.Defence = a.Defence;
							  x.Stamina = a.Stamina;
							  return x;
						  }
					)
				   .OrderBy(s => s.Name)
				   .ToList<dynamic>();

			ViewData["Title"] = "Projection with Sorting by Name";
			ViewData["Query"] = @"
var result =
   data.Select(
           a =>
              {
                 dynamic x = new ExpandoObject();
                 x.Name = a.Name;
                 x.Attack = a.Attack;
                 x.Defence = a.Defence;
                 x.Stamina = a.Stamina;
                 return x;
              }
        )
       .OrderBy(s => s.Name)
       .ToList<dynamic>();
";

			return View("Results", result);
		}

		public IActionResult Filter1()
		{
			var result =
			   data.Where(x => x.Defence == x.Attack)
				   .OrderByDescending(s => s.Stamina)
				   .ToList<dynamic>();

			ViewData["Title"] = "Filter and Sort Descending";
			ViewData["Query"] = @"
var result =
   data.Where(x => x.Defence == x.Attack)
       .OrderByDescending(s => s.Stamina)
       .ToList<dynamic>();
";

			return View("Results", result);
		}


		public IActionResult Filter2()
		{
			var result =
			   data.Where(x => x.Type1 == "Ice" ||
							   x.Type2 == "Ice")
				   .OrderBy(s => s.Name)
				   .ToList<dynamic>();
			ViewData["Title"] = "Filter and Sort Ascending";
			ViewData["Query"] = @"
var result =
   data.Where(x => x.Type1 == ""Ice"" || 
                   x.Type2 == ""Ice"")
       .OrderBy(s => s.Name)
       .ToList<dynamic>();
";

			return View("Results", result);
		}

		public IActionResult FilterProjectionSort()
		{
			var result =
			   data.Where(x => x.Defence == x.Attack)
				   .Select(
					   a =>
						  {
							  dynamic x = new ExpandoObject();
							  x.Name = a.Name;
							  x.AttackDefence = a.Attack;
							  x.Type1 = a.Type1;
							  x.Type2 = a.Type2;
							  return x;
						  }
					)
				   .OrderBy(s => s.Name)
				   .ToList<dynamic>();
			ViewData["Title"] = "Filter, Projection and Sort";
			ViewData["Query"] = @"
var result =
   data.Where(x => x.Defence == x.Attack)
       .Select(
          a =>
             {
                dynamic x = new ExpandoObject();
                x.Name          = a.Name;
                x.AttackDefence = a.Attack;
                x.Type1         = a.Type1;
                x.Type2         = a.Type2;
                return x;
             }
        )
       .OrderBy(s => s.Name)
       .ToList<dynamic>();
";
			return View("Results", result);
		}

		public IActionResult Grouping()
		{
			var result =
			   data.GroupBy(g => g.Type1)
				   .OrderByDescending(s => s.Key)
				   .Select(
					   p =>
						  {
							  dynamic x = new ExpandoObject();
							  x.PokeType = p.Key;
							  x.PokemonCount = p.Count();
							  x.MaxAttack = p.Max(a => a.Attack);
							  x.MinAttack = p.Min(a => a.Attack);
							  return x;
						  }
					)
				   .ToList<dynamic>();
			ViewData["Title"] = "Grouping, Projection, Aggregates and Sort";
			ViewData["Query"] = @"
var result =
   data.GroupBy(g => g.Type1)
       .OrderByDescending(s => s.Key)
       .Select(
          p =>
             {
                dynamic x = new ExpandoObject();
                x.PokeType     = p.Key;
                x.PokemonCount = p.Count();
                x.MaxAttack    = p.Max(a => a.Attack);
                x.MinAttack    = p.Min(a => a.Attack);
                return x;
             }
        )
       .ToList<dynamic>();
";
			return View("Results", result);
		}

		public IActionResult GroupingFilter()
		{
			var result =
			   data.GroupBy(g => g.Type1)
				   .OrderByDescending(s => s.Key)
				   .Select(
					   p =>
					   {
						   dynamic x = new ExpandoObject();
						   x.Name = p.Key;
						   x.PokemonCount = p.Count();
						   x.MaxAttack = p.Max(a => a.Attack);
						   x.MinAttack = p.Min(a => a.Attack);
						   return x;
					   }
					)
				   .Where(m => m.PokemonCount > 10)
				   .ToList<dynamic>();

			ViewData["Title"] = "Grouping, Projection, Aggregates, Filter and Sort";
			ViewData["Query"] = @"
var result =
   data.GroupBy(g => g.Type1)
       .OrderByDescending(s => s.Key)
       .Select(
          p =>
             {
                dynamic x = new ExpandoObject();
                x.Name         = p.Key;
                x.PokemonCount = p.Count();
                x.MaxAttack    = p.Max(a => a.Attack);
                x.MinAttack    = p.Min(a => a.Attack);
                return x;
             }
        )
       .Where(m => m.PokemonCount > 10)
       .ToList<dynamic>();
";
			return View("Results", result);
		}

		// TODO P07 LINQ Ex01: For only Pokemon with BOTH Type1 and Type2, sorted Type1, Type2, Name(Desc) 
		public IActionResult Ex01()
		{
			var result = data.Where(p => p.Type1 != "" && p.Type2 != "")
				.OrderBy(s => s.Type1)
				.ThenBy(m => m.Type2)
				.ThenBy(n => n.Name)
				.ToList();

			ViewData["Title"] = "Ex01: Pokemon with BOTH Type1 and Type2, sorted Type1, Type2, Name(Desc)";
			ViewData["Query"] = @"data.Where(P=>p.Type1 !="" && p.Type2 !="")
									.OrderBy(s => s.Type1)
									.ThenBy(m => m.Type2)
									.ThenBy(b => n.Name)
									.ToList();";


			return View("Results", result);
		}

		// TODO P07 LINQ Ex02: Pokemon with Stamina in between Attack and Defence OR in between Defence and Attack, sorted by Id  
		public IActionResult Ex02()
		{
			var result = data.Where(p => (p.Attack > p.Stamina && p.Stamina > p.Defence) || (p.Defence > p.Stamina && p.Stamina > p.Attack))
				.OrderBy(s => s.Id)
				.ToList();

			ViewData["Title"] = "Ex02: Pokemon with Stamina in between Attack and Defence OR in between Defence and Attack, sorted by Id";
			ViewData["Query"] = @"data.Where(p => (p.Attack > p.Stamina && p.Stamina > p.Defence) || (p.Defemce > p.Stamina && p.Stamina > p.Attack))
				.OrderBy(s => s.Id)
				.ToList();";

			return View("Results", result);
		}

		// TODO P07 LINQ Ex03: List of Electric Pokemon with Name, 'Electric', TotalStat sorted by TotalStat (desc)   
		public IActionResult Ex03()
		{
			var result = data.Where(p => p.Type1 == "Electric" || p.Type2 == "Electric")
				.Select(p =>
				{
					dynamic x = new ExpandoObject();
					x.Name = p.Name;
					x.Type = "Electric";
					x.TotalStat = p.Attack + p.Defence + p.Stamina;
					return x;
				})
				.OrderByDescending(s => s.TotalStat).ToList();

			ViewData["Title"] = "Ex03: List of Electric Pokemon with Name, 'Electric', TotalStat sorted by TotalStat (desc)";
			ViewData["Query"] = @"data.Where(p => p.Type1 == 'Electric' || p.Type2 == 'Electric')
				.Select(p =>
				{
					dynamic x = new ExpandoObject();
					x.Name = p.Name;
					x.Type = 'Electric';
					x.TotalStat = p.Attack + p.Defence + p.Stamina;
					return x;
				})
				.OrderByDescending(s => s.TotalStat).ToList();";

			return View("Results", result);
		}

		// TODO P07 LINQ EX04: For only Pokemon with Type2, group by Type2, display Type2, Count and Average Stamina    
		public IActionResult Ex04()
		{
			var result = data.Where(p => p.Type2 != "")
				.GroupBy(g => g.Type2)
				.OrderBy(s => s.Key)
				.Select(p =>
				{
					dynamic d = new ExpandoObject();
					d.Type2 = p.Key;
					d.PokemonCount = p.Count();
					d.AvgStamina = String.Format("{0:0.00}", p.Average(a => a.Stamina));
					return d;
				}).ToList();

			ViewData["Title"] = "Ex04: Group Pokemon with Type2 by Type2, display Type2, Count and AvgStamina ";
			ViewData["Query"] = @"data.Where(p => p.Type2 != "")
				.GroupBy(g => g.Type2)
				.OrderBy(s => s.Key)
				.Select(p =>
				{
					dynamic d = new ExpandoObject();
					d.Type2 = p.Key;
					d.PokemonCount = p.Count();
					d.AvgStamina = String.Format('{ 0:0.00}', p.Average(a => a.Stamina));
					return d;
		}).ToList();";
			return View("Results", result);
		}

	}
}
