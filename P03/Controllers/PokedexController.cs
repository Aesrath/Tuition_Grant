using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P03.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace P03.Controllers
{
   public class PokedexController : Controller
   {
      private IHostingEnvironment _env;

      public PokedexController(IHostingEnvironment environment)
      {
         _env = environment;
      }

      public IActionResult Index()
      {
         ViewData["Title"] = "Pokedex Index";
         List<Pokedex> modelList = PokedexMgr.GetAllPokedex();
         return View(modelList);
      }

      public IActionResult Add()
      {
         return View(new Pokedex());
      }

      [HttpPost]
      public IActionResult Add(Pokedex poke, IFormFile picture)
      {
         if (!IsValidPokedex(poke))
         {
            return View(poke);
         }
         else if (picture == null)
         {
            ViewData["Msg"] = "Please Upload a Picture";
            return View(poke);
         }
         else
         {
            string sql = @"INSERT Pokedex(Id, Name, Type1, Type2, 
                                             Attack, Defence, Stamina) 
                              VALUES ({0},'{1}','{2}','{3}',{4},{5},{6})";


            int rowsAffected = 0;
				//TODO: P03 Task 5 - Call DBUtl.ExecSQL to INSERT record to Database
				// rowsAffected = DBUtl.ExecSQL(.........
				rowsAffected = DBUtl.ExecSQL(sql, poke.Id, poke.Name, poke.Type1, poke.Type2, poke.Attack, poke.Defence, poke.Stamina);

            if (rowsAffected != 1)
            {
               // Database Insert Error
               ViewData["Msg"] = DBUtl.DB_Message;
               return View(poke);
            }
            else
            {
               // Database Insert Successful
               string fname = $"images/{poke.Id}-200.png";
               if (UploadFile(picture, fname))
               {
                  // File Upload Successful
                  return RedirectToAction("Index");
               }
               else
               {
                  ViewData["Msg"] = "File Upload Error";
                  return View(poke);
               }
            }
         }
      }

      private bool IsValidPokedex(Pokedex poke)
      {
         if ((poke.Id == null) || (poke.Name == null) || (poke.Type1 == null) ||
             (poke.Attack == null) || (poke.Defence == null) || (poke.Stamina == null))
         {
            ViewData["Msg"] = "Please enter all fields";
            return false;
         }

         if (poke.Type1.Equals(poke.Type2))
         {
            ViewData["Msg"] = "Type2 cannot be the same as Type1";
            return false;
         }

         if ((poke.Id <= 0) || (poke.Attack <= 0) || (poke.Defence <= 0) ||   (poke.Stamina <= 0))
         {
            ViewData["Msg"] = "Invalid stats";
            return false;
         }

         return true;
      }

      private bool UploadFile(IFormFile ufile, string fname)
      {
         if (ufile.Length > 0)
         {
            string fullpath = Path.Combine(_env.WebRootPath, fname);
            using (var fileStream =
                   new FileStream(fullpath, FileMode.Create))
            {
               ufile.CopyToAsync(fileStream);
            }
            return true;
         }
         return false;
      }

      public IActionResult Number(int num)
      {
         ViewData["Title"] = "Pokedex Number";
         Pokedex modelPoke = PokedexMgr.GetPokedex(num);

         if (modelPoke == null)
         {
            ViewData["Message"] =
               String.Format("No Pokedex found with #{0}", num.ToString());
            return View("Error");
         }
         else
         {
            return View(modelPoke);
         }
      }

      public IActionResult Type(string type1, string type2)
      {
         ViewData["Title"] = "Pokedex Type";
         List<Pokedex> modelList = null;
         if (type1 != null && type2 != null)
         {
            modelList = PokedexMgr.FindPokedex(type1, type2);
            ViewData["PokeType"] = $"{type1} and {type2}";

         }
         else if (type1 != null)
         {
            modelList = PokedexMgr.FindPokedex(type1);
            ViewData["PokeType"] = $"{type1}";

         }
         return View(modelList);

      }
   }
}
