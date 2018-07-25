using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P03.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace P03.Controllers
{
   public class CandidateController : Controller
   {
      private IHostingEnvironment _env;

      public CandidateController(IHostingEnvironment environment)
      {
         _env = environment;
      }

      public IActionResult Index()
      {
         ViewData["Title"] = "List of Candidates";

         List<Candidate> modelList =
            DBUtl.GetList<Candidate>("SELECT * FROM Candidate");
         return View(modelList);
      }

      public IActionResult Display(int id)
      {
         ViewData["Title"] = $"Candidate {id}";

         string sql = @"SELECT * FROM Candidate 
                         WHERE RegNo = {0}";
         List<Candidate> modelList = DBUtl.GetList<Candidate>(sql, id);
         if (modelList.Count == 0)
         {
            ViewData["Message"] = $"No Candidate found with Reg #{id}";
            return View("Error");
         }
         else
         {
            // Get the FIRST element of the List
            Candidate cdd = modelList[0];
            return View(cdd);
         }
      }

      // To Present An Emtpy Form
      [HttpGet]
      public IActionResult Create()
      {
         return View(new Candidate());
      }

      // To Handle Post Back Input Data 
      [HttpPost]
      public IActionResult Create(Candidate cdd, 
                                  IFormFile photo)
      {
         if (!IsValidCandidate(cdd))
         {
            return View(cdd);
         }
         else if (photo == null)
         {
            ViewData["Msg"] = "Please Upload a Photo";
            return View(cdd);
         }
         else
         {
            string sql = @"INSERT Candidate(RegNo, Name, 
                                            Gender, Height, 
                                            BirthDate, Race, 
                                            Clearance, PicFile) 
                           VALUES ({0},'{1}','{2}',{3},'{4}',
                                   '{5}','{6}','{7}')";
            cdd.PicFile = Path.GetFileName(photo.FileName);
            if (DBUtl.ExecSQL(sql,
                              cdd.RegNo, cdd.Name, cdd.Gender, cdd.Height,
                              String.Format("{0:yyyy-MM-dd}", cdd.BirthDate),
                              cdd.Race, cdd.Clearance, cdd.PicFile) != 1)
            {
               // Database Insert Error
               ViewData["Msg"] = DBUtl.DB_Message;
               return View(cdd);
            }
            else
            {
               // Database Insert Successful
               string fname = "candidates/" + cdd.PicFile;
               if (UploadFile(photo, fname))
               {
                  // File Upload Successful
                  return RedirectToAction("Index");
               }
               else
               {
                  ViewData["Msg"] = "File Upload Error";
                  return View(cdd);
               }
            }
         }
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

      private bool IsValidCandidate(Candidate cdd)
      {
         if ((cdd.RegNo == null) || (cdd.Name == null) || (cdd.Height == null) ||
             (cdd.Gender == null) || (cdd.Race == null) || (cdd.BirthDate == null))
         {
            ViewData["Msg"] = "Invalid data or Non-entry";
            return false;
         }

         // Must be 18 and above
         if (DateTime.Compare(cdd.BirthDate.AddYears(18), DateTime.Now) > 0)
         {
            ViewData["Msg"] = "Must be 18 years and above";
            return false;
         }

         return true;
      }
   }
}
