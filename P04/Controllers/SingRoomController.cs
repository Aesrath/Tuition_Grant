using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P04.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P04.Controllers
{
	public class SingRoomController : Controller
	{
		// GET: SingRoom
		public IActionResult Index()
		{
			List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking");
			return View(model);
		}

		public IActionResult CreateBooking()
		{
			ViewData["PackageTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRPackageType ORDER BY Description");
			ViewData["Slots"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");
			return View();
		}

		[HttpPost]
		public IActionResult CreateBooking(SRBooking newSRBooking)
		{
			// TODO Task 3d Use ModelState.IsValid to guard against invalid input. Pass the message "Invalid information entered" to Index action when ModelState.IsValid is false
			if (ModelState.IsValid)
			{

				if (DBUtl.ExecSQL(@"INSERT INTO SRBooking (Name, SlotId, PackageTypeId, BookingDate, Hours, AOSnack, AODrink) 
                                    VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', '{6}')",
									newSRBooking.Name, newSRBooking.SlotId, newSRBooking.PackageTypeId, $"{newSRBooking.BookingDate:yyyy-MM-dd}", newSRBooking.Hours, newSRBooking.AOSnack, newSRBooking.AODrink) == 1)
					TempData["Msg"] = "New booking added.";
				else
					TempData["Msg"] = "Failed to add new booking.";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["Msg"] = "Invalid information entered";
				return RedirectToAction("Index");
			}
		}

		public IActionResult ViewBookingsByPackage()
		{
			ViewData["PackageTypes"] = DBUtl.GetList("SELECT * FROM SRPackageType ORDER BY Description");
			List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking");
			return View(model);
		}
	}
}
