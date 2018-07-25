using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P05.Models;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace P05.Controllers
{
	public class SingRoomController : Controller
	{
		// TODO Task 1a: Add the LoginUser model class, drag the LoginUser.cs from code/Task1 into Models folder
		// TODO Task 1b: Add the Login.cshtml view, drag the Login.cshtml from code/Task1 into Views/Account folder

		// TODO Task 1d: Use the Authorize attribute to ensure only authenticated user can access the Index, CreateBooking and ViewBookingsByPackage actions. Use AllowAnonymous attribute to allow visitors to access AboutUs action

		// TODO Task 1h: Verification - run the SingRoom web app and ensure the authentication process work and only logged-user can access restricted actions

		// TODO Task 2a: Modify the GetList so that only records of current-logged in user is retrieved.
		// TODO Task 2b: Verification: Navigate to SingRoom/Index to verify that only records of current user is displayed.        
		[Authorize]
		public IActionResult Index()
		{
			string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<SRBooking> model = DBUtl.GetList<SRBooking>
										("SELECT * FROM SRBooking WHERE BookedBy = {0}", userID);
			return View(model);
		}

		//}
		[Authorize]
		public IActionResult CreateBooking()
		{
			ViewData["PackageTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRPackageType ORDER BY Description");
			ViewData["Slots"] = DBUtl.GetList("SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");
			// TODO Task 4g: Pass "CreateBooking" and "Create" as value of ViewData["PostTo"] and ViewData["ButtonText"] to Booking.cshtml

			// TODO Task 4h: Verification: Navigate to SingRoom/CreateBooking to ensure the CreateBooking functionality still work.
			return View("Booking");
		}

		[Authorize()]
		[HttpPost]
		public IActionResult CreateBooking(SRBooking newBk)
		{
			if (ModelState.IsValid)
			{
				// TODO Task 2c: Modify the ExecSQL so that BookedBy field is updated with current logged-user id.
				// TODO Task 2d: Verification: Navigate to SingRoom/CreateBooking to verify that new records created can be displayed in the index view
				string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				if (DBUtl.ExecSQL(@"INSERT INTO SRBooking 
                                    (Name, SlotId, PackageTypeId, BookingDate, Hours, 
                                        AOSnack, AODrink, BookedBy) 
                                    VALUES ('{0}',{1},{2},'{3}',{4},'{5}','{6}',{7})",
									   newBk.Name, newBk.SlotId, newBk.PackageTypeId,
									   $"{newBk.BookingDate:dd MMMM yyyy}", newBk.Hours,
									   newBk.AOSnack, newBk.AODrink, userID) == 1)
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

		[Authorize]
		public IActionResult ViewBookingsByPackage()
		{
			ViewData["PackageTypes"] = DBUtl.GetList("SELECT * FROM SRPackageType ORDER BY Description");
			// TODO Task 2e: Modify the GetList so that only records of current-logged in user is retrieved.
			// TODO Task 2f: Verification: Navigate to SingRoom/ViewBooingsByPackage to verify that only records of current user is displayed.
			string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<SRBooking> model = DBUtl.GetList<SRBooking>("SELECT * FROM SRBooking where BookedBy = {0}", userID);
			
			return View(model);


		}

		// TODO Task 4i: Use code fragment shown in worksheet to create UpdateBooking HttpGet action. Make sure this is a restricted action.
		[Authorize]
		public IActionResult UpdateBooking(int Id, bool? isDelete)
		{
			ViewData["PackageTypes"] = DBUtl.GetList(@"SELECT ID as value, Description as text from SRPackageType ORDER BY Description");
			ViewData["Slots"] = DBUtl.GetList(@"SELECT Id as value, Description as text FROM SRSlot ORDER BY Description");

			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<SRBooking> lstBooking = DBUtl.GetList<SRBooking>(@"SELECT * FROM SRBooking WHERE Id = {0} AND BookedBy={1}", Id, userId);

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


		// TODO Task 4j: Verification: Navigate to SingRoom/Index and click on the Update hyperlink to ensure the update view is properly displayed.


		// TODO Task 4k: Use code fragment shown in worksheet to create UpdateBooking HttpPost action. Make sure it is an restricted action.
		[Authorize()]
		[HttpPost]
		public IActionResult UpdateBooking(SRBooking uBook)
		{
			if (ModelState.IsValid)
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				if (DBUtl.ExecSQL(@"UPDATE SRBooking 
SET Name='{0}', SlotId={1}, PackageTypeId={2},BookingDate='{3}',Hours={4},AOSnack='{5}',AODrink='{6}' WHERE Id = {7} AND BookedBy={8}",
uBook.Name, uBook.SlotId, uBook.PackageTypeId, $"{uBook.BookingDate:dd MMMM yyyy}", uBook.Hours, uBook.AOSnack, uBook.AODrink, uBook.Id, userId) == 1)
				{
					TempData["Msg"] = $"Booking{uBook.Id} updated";
				}
				else
				{
					TempData["Msg"] = DBUtl.DB_Message;
				}
				return RedirectToAction("Index");

			}
			else
			{
				TempData["Msg"] = "Invalid information entered!";
				return RedirectToAction("Index");
			}
		}

		// TODO Task 4l: Verification: Navigate to SingRoom/Index and click on the Update hyperlink, update the information and click on the Update button. Verify that the information is updated to database.

		// TODO Task 4m: Use code fragment shown in worksheet to create DeleteBooking HttpPost action. Make sure it is an restricted action.
		[Authorize()]
		[HttpPost]
		public IActionResult DeleteBooking(SRBooking uBook)
		{
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (DBUtl.ExecSQL(@"DELETE FROM SRBooking WHERE Id ={0} AND BookedBy={1}", uBook.Id, userId) == 1)
			{
				TempData["Msg"] = $"Booking{uBook.Id} deleted";
				
			}
			else
			{
				TempData["Msg"] = DBUtl.DB_Message;
			}
			return RedirectToAction("Index");
		}

		// TODO Task 4n: Verification: Navigate to SingRoom/Index and click on the Delete hyperlink to ensure the delete view is properly displayed and click on the Delete button. Verify that the record is successfully deleted.

		[AllowAnonymous]
		public IActionResult AboutUs()
		{
			return View();
		}

	}
}

