using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace P03.Controllers
{
   public class HomeController : Controller
   {
      public IActionResult Index()
      {
         return RedirectToAction("Index", "Candidate");
      }

      public IActionResult About()
      {
         return View();
      }

   }
}
