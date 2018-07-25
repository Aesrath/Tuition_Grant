using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P12.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P12.Controllers
{
    public class PatientController : Controller
    {
        private AppDbContext _dbContext;
        private IHostingEnvironment _env;

        public PatientController(AppDbContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _env = environment;
        }

        [Authorize]
        public IActionResult Index()
        {
            DbSet<Patient> dbs = _dbContext.Patient;
            List<Patient> model = dbs.OrderBy(p => p.Id)
                                     .ToList();
            return View(model);
        }

        [Authorize]
        public IActionResult Update(string id)
        {
            DbSet<Patient> dbs = _dbContext.Patient;
            Patient tPatient = dbs.Where(p => p.Id == id)
                                  .FirstOrDefault();

            if (tPatient != null)
            {
                return View(tPatient);
            }
            else
                TempData["Msg"] = "Patient not found!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(Patient patient,
                                  IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                DbSet<Patient> dbs = _dbContext.Patient;
                Patient tPatient = dbs.Where(p => p.Id == patient.Id)
                                      .FirstOrDefault();

                if (tPatient != null)
                {
                    tPatient.Name = patient.Name;
                    tPatient.Citizenship = patient.Citizenship;
                    tPatient.Icpassport = patient.Icpassport;

                    string msg = "";
                    if (_dbContext.SaveChanges() == 1)
                        msg = "Patient info updated. ";

                    // To be implemented：Save image if photo is not null
                    // Use 6P Part 1 slides to help complete this part

                    TempData["Msg"] = msg;
                }
                else
                    TempData["Msg"] = "Patient not found!";

            }
            else
                TempData["Msg"] = "Invalid input!";

            return RedirectToAction("Index");
        }

        private bool UploadFile(IFormFile ufile, string fname)
        {
            if (ufile.Length > 0)
            {
                string fullpath = Path.Combine(_env.WebRootPath, fname);
                using (var fileStream =
                           new FileStream(fullpath, FileMode.Create))
                {
                    ufile.CopyTo(fileStream);
                }
                return true;
            }
            return false;
        }

    }
}
