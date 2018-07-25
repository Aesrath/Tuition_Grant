using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P12.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P12.Controllers
{
    public class AppointmentController : Controller
    {
        private AppDbContext _dbContext;

        public AppointmentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            DbSet<Appointment> dbs = _dbContext.Appointment;
            List<Appointment> model = dbs.OrderBy(a => a.AppDate)
                                           .ThenBy(a => a.PatientId)
                                           .ToList();
            return View(model);
        }

        [Authorize]
        public IActionResult Create(string Id)
        {
            DbSet<Patient> dbs = _dbContext.Patient;
            Patient patient = dbs.Where(p => p.Id == Id)
                                 .FirstOrDefault();
            if (patient == null)
            {
                TempData["Msg"] = String.Format("Patient {0} not found!", Id);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["patient"] = patient;
                Appointment model = new Appointment();
                model.PatientId = patient.Id;
                model.AppDate = DateTime.Today;
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Appointment newAppointment)
        {
            DbSet<Appointment> dbs = _dbContext.Appointment;
            dbs.Add(newAppointment);

            int result = _dbContext.SaveChanges();
            if (result == 1)
            {
                TempData["Msg"] = String.Format("New appointment created for {0} created!", newAppointment.PatientId);
            }
            else
            {
                TempData["Msg"] = String.Format("Failed to create new appointment for {0}!", newAppointment.PatientId);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SearchByPatientId(string Id)
        {
            DbSet<Appointment> dbs = _dbContext.Appointment;
            List<Appointment> model = dbs.Where(a => a.PatientId == Id)
                                         .OrderBy(a => a.AppDate)
                                         .ThenBy(a => a.PatientId)
                                         .ToList();
            ViewData["byPatient"] = true;
            ViewData["patientId"] = Id;
            return View(model);
        }

        [Authorize]
        public IActionResult GetApptByPatientId(string Id)
        {
            DbSet<Appointment> dbs = _dbContext.Appointment;
            IQueryable<Appointment> q = from a in dbs
                                        where a.PatientId == Id
                                        orderby a.AppDate, a.PatientId
                                        select a;
            List<Appointment> model = dbs.Where(a => a.PatientId == Id)
                                         .OrderBy(a => a.AppDate)
                                         .ThenBy(a => a.PatientId)
                                         .ToList();
            ViewData["byPatient"] = true;
            return PartialView("_Appointments", model);
        }

        [Authorize]
        public IActionResult SearchByDate(string Id)
        {
            DateTime appt;
            List<Appointment> model;
            if (Utils.TryParseDate(Id, out appt))
            {
                DbSet<Appointment> dbs = _dbContext.Appointment;
                model = dbs.Where(a => a.AppDate == appt)
                                        .OrderBy(a => a.PatientId)
                                        .ToList();
            }
            else
            {
                model = new List<Appointment>();
            }

            ViewData["byPatient"] = false;
            ViewData["apptDate"] = Id;
            return View(model);
        }

        [Authorize]
        public IActionResult GetApptByDate(string Id)
        {
            DateTime appt;
            List<Appointment> model;
            if (Utils.TryParseDate(Id, out appt))
            {
                DbSet<Appointment> dbs = _dbContext.Appointment;
                model = dbs.Where(a => a.AppDate == appt)
                           .OrderBy(a => a.PatientId)
                           .ToList();
            }
            else
            {
                model = new List<Appointment>();
            }

            ViewData["byPatient"] = false;
            return PartialView("_Appointments", model);
        }
    }
}
