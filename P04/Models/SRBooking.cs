using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// TODO Task 3b: Include the namespace for data annotations

namespace P04.Models
{
	// TODO Task 3a: Edit bower.json to include jQuery validation library.

	// TODO Task 3c: Data annotate the properties. BookingDate has been done for you as an example. Refer to worksheet for the validation requirements.

	public class SRBooking
    {
        public int Id { get; set; }
		[Required(ErrorMessage ="Booking Date cannot be empty!")]
		[DataType(DataType.Date,ErrorMessage ="Invalid date!")]
        public DateTime BookingDate { get; set; }
		[Required(ErrorMessage ="Must select a slot!")]
        public int SlotId { get; set; }
		[Required(ErrorMessage ="Name cannot be empty!")]
        public string Name { get; set; }
		[Required(ErrorMessage ="PackageID cannot be empty!")]
		[Range(1,3,ErrorMessage = "PackageID must be between 1 to 3!")]
        public int PackageTypeId { get; set; }
		[Required(ErrorMessage ="Hours cannot be empty!")]
		[Range(1,3,ErrorMessage ="Hours must be between 1 to 3!")]
        public int Hours { get; set; }
        public bool AOSnack { get; set; }
        public bool AODrink { get; set; }
    }
}