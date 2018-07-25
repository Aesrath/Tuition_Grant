using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P04.Models;

namespace P04.Controllers
{
    public class PetHotelController : Controller
    {
        public IActionResult Index()
        {
            List<Booking> model = DBUtl.GetList<Booking>("SELECT * FROM Booking");
            return View(model);
			
        }

        // TODO Task 5b: Create a View for AddBooking action with propery client-side validation. User problem statement Appendix A as reference.
        public IActionResult AddBooking()
        {
            ViewData["PetTypes"] = DBUtl.GetList("SELECT Id as value, Description as text FROM PetType ORDER BY Description");
			return View();


        }

		// TODO Task 5c: Implement AddBooking HttpPost action to insert new booking record. Must use ModelState.IsValid to ensure valid input are entered and redirect to input with proper messages. Refer to worksheet for messages to be used for different scenarios.
		[HttpPost]

		public IActionResult AddBooking(Booking newBook)
		{
			if (ModelState.IsValid)
			{

				if (DBUtl.ExecSQL(@"INSERT INTO Booking (NRIC, OwnerName, PetName, PetTypeId, CheckInDate, Days, FeedFreq, FTCanned, FTDry, FTSoft)
                                    VALUES ('{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}', '{7}', '{8}', '{9}')",
									newBook.NRIC, newBook.OwnerName,newBook.PetName,newBook.PetTypeId,$"{newBook.CheckInDate:yyyy-MM-dd}",newBook.Days,newBook.FeedFreq,newBook.FTCanned,newBook.FTDry,newBook.FTSoft) == 1)
					TempData["Msg"] = "New booking added.";
				else
					TempData["Msg"] = "Failed to add new booking."+DBUtl.DB_Message;
				return RedirectToAction("Index");
			}
			else
			{
				TempData["Msg"] = "Invalid information entered";
				return RedirectToAction("Index");
			}
		}



		public IActionResult ViewBookingsByPetTypes()
        {
            ViewData["PetTypes"] = DBUtl.GetList("SELECT * FROM PetType ORDER BY Id");
            List<Booking> model = DBUtl.GetList<Booking>("SELECT * FROM Booking");
            return View(model);
        }
    }
}
