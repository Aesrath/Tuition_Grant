using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P11.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace P11.Controllers
{

    public class HESSAPController : Controller
    {
        private AppDbContext _dbContext;

        public HESSAPController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Student curStudent()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DbSet<Student> dbs = _dbContext.Student;
            Student student = dbs.Where(s => s.Id == Int32.Parse(userid)).FirstOrDefault();
            return student;
        }

        // GET: HESSAP
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewResult()
        {
            //prepare model
            AcadPerfViewer model = new AcadPerfViewer();

            model.CurrentStudent = curStudent();
            model.Semester = 0;
            model.AcadResult = new List<Result>();

            if (model.CurrentStudent.IsAdmin)
            {
                DbSet<Student> dbs = _dbContext.Student;
                var lstStudents =
                    dbs.OrderBy(s => s.Id)
                       .ToList()
                       .Select(
                           s =>
                           {
                               dynamic d = new ExpandoObject();
                               d.value = s.Id;
                               d.text = $"{s.Id} {s.Name}";
                               return d;
                           }
                       )
                       .ToList<dynamic>();
                ViewData["students"] = lstStudents;
            }

            if (User.IsInRole("Admin"))
            {
                ViewData["semesters"] = new List<dynamic>();
            }
            else
            {
                DbSet<Result> dbsS = _dbContext.Result;
                var lstSemesters = dbsS.Where(s => s.StudentId == curStudent().Id)
                                             .OrderBy(s => s.Semester)
                                             .GroupBy(s => s.Semester)
                                             .Select(g => g.Key)
                                             .ToList()
                                             .Select(
                                                s =>
                                                   {
                                                       dynamic d = new ExpandoObject();
                                                       d.value = s;
                                                       d.text = s;
                                                       return d;
                                                   }
                                              )
                                              .ToList();
                ViewData["semesters"] = lstSemesters;
            }

            return View(model);
        }

        [Authorize]
        public IActionResult GetResult(int Id, int Semester)
        {
            DbSet<Result> dbs = _dbContext.Result;
            List<Result> model = dbs.Where(r => r.StudentId == Id &&
                                   r.Semester == Semester)
                       .OrderBy(r => r.ModuleId)
                       .Include("Module")
                       .ToList();

            return PartialView("_ResultsTable", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddResult()
        {
            DbSet<Student> dbsStudent = _dbContext.Student;
            var lstStudents =
                dbsStudent.OrderBy(s => s.Id)
                          .ToList()
                          .Select(
                            s =>
                               {
                                   dynamic d = new ExpandoObject();
                                   d.value = s.Id;
                                   d.text = $"{s.Id} {s.Name}";
                                   return d;
                               }
                           )
                          .ToList<dynamic>();
            ViewData["students"] = lstStudents;

            DbSet<Module> dbsModule = _dbContext.Module;
            var lstModules =
                dbsModule.OrderBy(m => m.Id)
                         .ToList()
                         .Select(
                            m =>
                            {
                                dynamic d = new ExpandoObject();
                                d.value = m.Id;
                                d.text = $"{m.Id} {m.Description}";
                                return d;
                            }
                          )
                           .ToList<dynamic>();
            ViewData["modules"] = lstModules;
            return View();
        }

        [HttpPost]
        public IActionResult AddResult(Result result)
        {
            string msg = "";
            Result model = result;
            if (ModelState.IsValid)
            {
                // To be implemented: Search in database to check whether the result of this module has been added before for the selected student
                // HINT: Query against _dbContext.Result
                // HINT: Use Where and FirstOrDefault method to check whether such result exists or not

                // To be implemented:
                // If such result is found in database, display message
                //      "Semester result for this module has been added before!"
                // If such result is not found then proceeds to insert the record
                // HINT: Use Add and SaveChanges method to insert
                // If insert is successful display:
                //      "New result added successfully
                // IF insert fails display:
                //      "Failed to update database"
            }
            else
            {
                msg = "Invalid information entered";
            }
            ViewData["msg"] = msg;

            // To be implemented: prepare ViewData["students"] and ViewData["modules"]
            // HINT: Refer to AddResult HttpGet action

            return View(model);
        }
    }
}