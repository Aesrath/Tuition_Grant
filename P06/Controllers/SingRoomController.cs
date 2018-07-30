using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P06.Models;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace P06.Controllers
{
    public class SingRoomController : Controller
    {
        // TODO P06 Task 1b: Modify the authorization requirement so that users with different roles can access different actions.
        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking WHERE BookedBy = {0}", userid);
            return View(model);
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult IndexAdmin()
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking WHERE BookedBy = {0}", userid);
            return View(model);
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult CreateBooking()
        {
            ViewData["PackageTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRPackageType ORDER BY Description");
            ViewData["Slots"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");
            ViewData["PostTo"] = "CreateBooking";
            ViewData["ButtonText"] = "Create";
            return View("Booking");
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateBooking()
        {
            ViewData["PackageTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRPackageType ORDER BY Description");
            ViewData["Slots"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");
            ViewData["PostTo"] = "UpdateBooking";
            ViewData["ButtonText"] = "Update";
            return View("Edit");
        }


        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult CreateBooking(SRBooking newSRBooking)
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (DBUtl.ExecSQL(@"INSERT INTO SRBooking (Name, SlotId, PackageTypeId, BookingDate, Hours, AOSnack, AODrink,BookedBy) 
                                    VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', '{6}',{7})",
                                    newSRBooking.Name, newSRBooking.SlotId, newSRBooking.PackageTypeId, $"{newSRBooking.BookingDate:dd MMMM yyyy}", newSRBooking.Hours, newSRBooking.AOSnack, newSRBooking.AODrink, userid) == 1)
                    TempData["Msg"] = "New booking added.";
                else
                    TempData["Msg"] = DBUtl.DB_Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "Invalid information entered!";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult UpdateBooking(SRBooking newSRBooking)
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (DBUtl.ExecSQL(@"INSERT INTO SRBooking (Name, SlotId, PackageTypeId, BookingDate, Hours, AOSnack, AODrink,BookedBy) 
                                    VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', '{6}',{7})",
                                    newSRBooking.Name, newSRBooking.SlotId, newSRBooking.PackageTypeId, $"{newSRBooking.BookingDate:dd MMMM yyyy}", newSRBooking.Hours, newSRBooking.AOSnack, newSRBooking.AODrink, userid) == 1)
                    TempData["Msg"] = "New booking added.";
                else
                    TempData["Msg"] = DBUtl.DB_Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "Invalid information entered!";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateBooking(int Id, bool? isDelete)
        {
            ViewData["PackageTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRPackageType ORDER BY Description");
            ViewData["Slots"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");

            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<SRBooking> lstBooking = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking WHERE Id = {0} AND BookedBy={1}", Id, userid);
            SRBooking model = null;
            if (lstBooking.Count > 0)
            {
                if (isDelete.HasValue == false || isDelete == false)
                {
                    ViewData["PostTo"] = "UpdateBooking";
                    ViewData["ButtonText"] = "Update";
                }
                else
                {
                    ViewData["PostTo"] = "DeleteBooking";
                    ViewData["ButtonText"] = "Delete";
                }
                model = lstBooking[0];
                return View("Booking", model);
            }
            else
            {
                TempData["Msg"] = $"Booking {Id} not found!";
                return RedirectToAction("Index");
            }
        }

     

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult DeleteBooking(SRBooking uBook)
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (DBUtl.ExecSQL(@"DELETE SRBooking 
                                    WHERE Id = {0} AND BookedBy={1}",
                                uBook.Id, userid) == 1)
                TempData["Msg"] = $"Booking {uBook.Id} deleted.";
            else
                TempData["Msg"] = DBUtl.DB_Message;
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult ViewBookingsByPackage()
        {
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewData["PackageTypes"] = DBUtl.GetList("SELECT * FROM SRPackageType ORDER BY Description");
            List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking WHERE BookedBy = {0}", userid);
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult UsersIndex()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SalesSummary(int ryear, int rmonth)
        {
            List<SRBooking> data = null;

            ViewData["ryear"] = ryear;
            ViewData["rmonth"] = rmonth;

            if (ryear <= 0)
            {
                data = DBUtl.GetList<SRBooking>(@"SELECT * FROM SRBooking");
                ViewData["reportheader"] = "Overall Sales Summary by Year";
				// TODO P06 Task 2a: Use LINQ query provided in worksheet to retrieve summary data grouped by Year
				// Note: delete the original List<dynamic> model = null statement
				var model = data.GroupBy(b => b.BookingDate.Year)
					.OrderBy(g => g.Key)
					.Select(g =>
					{
						dynamic d = new ExpandoObject();
						d.Group = g.Key;
						d.Total = g.Sum(b => b.Cost);
						d.Average = g.Average(b => b.Cost);
						d.Lowest = g.Min(b => b.Cost);
						d.Highest = g.Max(b => b.Cost);
						return d;
					}).ToList();

                // TODO P06 Task 2b: Verfication - Run SingRoom, login as admin user and click on the Reports Summary to ensure that the report will appear now

                return View(model);
            }
            else if (rmonth <= 0 || rmonth > 12)
            {
                data = DBUtl.GetList<SRBooking>(@"SELECT * FROM SRBooking
                                                            WHERE YEAR(BookingDate) = {0}", ryear);
                ViewData["reportheader"] = $"Annual Sales Summary for {ryear} by Month";
				// TODO P06 Task 2c: Use LINQ query provided in worksheet to retrieve summary data grouped by Month for a given year
				// Note: delete the original List<dynamic> model = null statement
				var model = data.GroupBy(b => b.BookingDate.Month)
					.OrderByDescending(g => g.Key)
					.Select(g =>
					{
						dynamic d = new ExpandoObject();
						d.Group = g.Key;
						d.Total = g.Sum(b => b.Cost);
						d.Average = g.Average(b => b.Cost);
						d.Lowest = g.Min(b => b.Cost);
						d.Highest = g.Max(b => b.Cost);
						return d;
					}).ToList();

                // TODO P06 Task 2d: Verfication - Run SingRoom, login as admin user and click on the Reports Summary and click on year 2016 to view sales summary by month for year 2016

                return View(model);
            }
            else
            {
                data = DBUtl.GetList<SRBooking>(@"SELECT * FROM SRBooking
                                                            WHERE YEAR(BookingDate) = {0}
                                                                    AND MONTH(BookingDate) = {1}",
                                                                    ryear, rmonth);
                ViewData["reportheader"] = $"Monthly Sales for {ryear} Month {rmonth} by Day";

                // TODO P06 Task 2e: modify this line to get the proper result
				var model = data.GroupBy(b => b.BookingDate.Day)
					.OrderByDescending(g => g.Key)
					.Select(g =>
					{
						dynamic d = new ExpandoObject();
						d.Group = g.Key;
						d.Total = g.Sum(b => b.Cost);
						d.Average = g.Average(b => b.Cost);
						d.Lowest = g.Min(b => b.Cost);
						d.Highest = g.Max(b => b.Cost);
						return d;
					}).ToList();
				// TODO P06 Task 2f: Verfication - Run SingRoom, login as admin user and click on the Reports Summary and click on 2016, then click on any month to view sales summary by day

				return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Reports()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}

