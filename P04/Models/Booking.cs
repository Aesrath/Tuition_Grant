using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P04.Models
{
    // TODO Task 5a: Data annotate the properties. BookingDate has been done for you as an example. Refer to worksheet for the validation requirements.
    public class Booking
    {
        public int Id { get; set; }
		[Required(ErrorMessage ="NRIC cannot be empty!")]
		[RegularExpression(@"[STFG]\d{7}[A-Z]",ErrorMessage ="Invalid NRIC format!")]
        public string NRIC { get; set; }
		[Required(ErrorMessage ="Owner name cannot be empty!")]
        public string OwnerName { get; set; }
		[Required(ErrorMessage ="Pet name cannot be empty!")]
        public string PetName { get; set; }
		[Required(ErrorMessage ="Days cannot be empty!")]
		[Range(1,5,ErrorMessage ="Days must be between 1 to 5!")]
        public int Days { get; set; }
		[Required(ErrorMessage ="Pet Type ID cannot be empty!")]
        public int PetTypeId { get; set; }
		[Required(ErrorMessage ="Feeding frequency cannot be empty!")]
		[Range(0,2,ErrorMessage ="Feeding frequency must be between 0,2!")]
        public int FeedFreq { get; set; }
        public bool FTCanned { get; set; }
        public bool FTDry { get; set; }
        public bool FTSoft { get; set; }
		[Required(ErrorMessage ="Check-in date cannot be empty")]
		[DataType(DataType.Date,ErrorMessage ="Invalid Date!")]
        public DateTime CheckInDate { get; set; }
    }
}
