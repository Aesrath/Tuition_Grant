using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P07.Controllers
{
    public class NumberController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IfElseOddOrEven(int num)
        {
            return View(num);
        }

        public IActionResult IfElseCheckPrice(double price)
        {
            return View(price);
        }

        public IActionResult ExCanYouDrive(int age)
        {
            return View(age);
        }

        public IActionResult ExHouses(int numHouses)
        {
            return View(numHouses);
        }

        public IActionResult LoopForSnackStock()
        {
            return View();
        }

        public IActionResult LoopWhileSnackStock()
        {
            return View();
        }

        public IActionResult ExForEachSnackStock()
        {
            return View();
        }

        public IActionResult ExShowLines(int lastLine)
        {
            return View(lastLine);
        }

        public IActionResult LoopIfOddNumbersUnder(int upperLimit)
        {
            return View(upperLimit);
        }

        
    }
}
