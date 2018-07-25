using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using P05.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P05.Controllers
{
    public class PetHotelController : Controller
    {
		

		[Authorize]
        public IActionResult Index()
        {
			string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<PHBooking> model = DBUtl.GetList<PHBooking>("SELECT * FROM PHBooking WHERE BookedBy={0}", userID);
            return View(model);
        }


		[Authorize]
        public IActionResult AddBooking()
        {
            ViewData["PetTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM PHPetType ORDER BY Description");

            return View("Booking");
        }

		[Authorize()]
        [HttpPost]
        public IActionResult AddBooking(PHBooking newBook)
        {
            if (ModelState.IsValid)
            {
				string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				string sql = @"INSERT INTO PHBooking 
                                    (NRIC, OwnerName, PetName, Days, PetTypeId, 
                                     FeedFreq, FTCanned, FTDry, FtSoft, CheckInDate, BookedBy) 
                                    VALUES ('{0}', '{1}', '{2}', {3}, {4}, 
                                            {5},'{6}','{7}','{8}','{9}',{10})";

                if (DBUtl.ExecSQL(sql,
                                    newBook.NRIC, newBook.OwnerName, newBook.PetName,
                                    newBook.Days, newBook.PetTypeId, newBook.FeedFreq,
                                    newBook.FTCanned, newBook.FTDry, newBook.FTSoft,
                                    $"{newBook.CheckInDate:yyyy-MM-dd}",userID) == 1)
                    TempData["Msg"] = "New booking added.";
                else
                    TempData["Msg"] = "Failed to add new booking.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "Invalid information entered!";
                return RedirectToAction("Index");
            }
        }

		[Authorize]
        public IActionResult ViewBookingsByPetTypes()
        {
            ViewData["PetTypes"] =
                DBUtl.GetList(@"SELECT * 
                                      FROM PHPetType 
                                     ORDER BY Id");
			string userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<PHBooking> model = DBUtl.GetList<PHBooking>("SELECT * FROM PHBooking WHERE BookedBy={0}",userID);
            return View(model);
        }

		[Authorize]
		public IActionResult UpdateBooking(int Id, bool? isDelete)
		{
			ViewData["PetTypes"] = DBUtl.GetList(@"SELECT ID as value, Description as text from PetType ORDER BY Description");

			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<PHBooking> lstBooking = DBUtl.GetList<PHBooking>(@"SELECT * FROM PHBooking WHERE Id = {0} AND BookedBy={1}", Id, userId);

			PHBooking model = null;
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
		public IActionResult UpdateBooking(PHBooking newBook)
		{
			if (ModelState.IsValid)
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				if (DBUtl.ExecSQL(@"UPDATE PHBooking 
SET NRIC='{0}', OwnerName='{1}', PetName='{2}', PetTypeId={3}, CheckInDate='{4}', Days={5}, FeedFreq='{6}', FTCanned='{7}', FTDry='{8}', FTSoft='{9}' WHERE Id = {10} AND BookedBy={11}",
newBook.NRIC, newBook.OwnerName, newBook.PetName, newBook.PetTypeId, $"{newBook.CheckInDate:yyyy-MM-dd}", newBook.Days, newBook.FeedFreq, newBook.FTCanned, newBook.FTDry, newBook.FTSoft) == 1)
				{
					TempData["Msg"] = $"Booking{newBook.Id} updated";
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
		public IActionResult DeleteBooking(PHBooking newBook)
		{
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (DBUtl.ExecSQL(@"DELETE FROM PHBooking WHERE Id ={0} AND BookedBy={1}", newBook.Id, userId) == 1)
			{
				TempData["Msg"] = $"Booking{newBook.Id} deleted";

			}
			else
			{
				TempData["Msg"] = DBUtl.DB_Message;
			}
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }

    }
}
