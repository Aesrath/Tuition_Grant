using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P01.Models;

namespace P01.Controllers
{
    public class PokedexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Number(int id)
		{
			Pokedex poke = PokedexMgr.GetPokemon(id);
			ViewData["Pokemon"] = poke;
			return View();			
		}
    }
}