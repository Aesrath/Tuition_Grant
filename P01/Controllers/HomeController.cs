using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P01.Models;

namespace P01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			Greeting greet =
				new Greeting("SOI Students",
							  "C286 Module Chair",
							  "Welcome to Adv Web App Dev in .NET",
							  @"This is a very useful module for your FYP.  
                                 Study hard and All the BEST!");

			ViewData["Hello"] = greet;

			return View();
        }
    }
}