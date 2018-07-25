using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P12.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace P12.Controllers
{
    // To be implemented: Use attribute routing to configure the routing for ApptApiController
    // Example: http://localhost:59582/api/Appt/Doctors/03-02-2018
    // the return result is ["LYM","FC"]

    public class ApptApiController : Controller
    {
        private AppDbContext _dbContext;

        public ApptApiController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult GetDoctors(string apptDate)
        {
            DateTime appt;
            if (Utils.TryParseDate(apptDate, out appt))
            {
                string weekday = appt.ToString("ddd");
                DbSet<Schedule> dbs = _dbContext.Schedule;
                List<string> schedules = dbs.Where(s => s.Weekday == weekday)
                                            .Select(s => s.DoctorId)
                                            .ToList();
                return Ok(schedules);
            }
            else
            {
                return Ok("'Invalid date format!'");
            }
        }
    }
}