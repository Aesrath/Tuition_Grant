using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P06.Models
{
    public class SRBooking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Booking date cannot be empty!")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date!")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Must select a slot!")]
        public int SlotId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PackageId cannot be empty!")]
        [Range(1, 3, ErrorMessage = "PackageId must be between 1 to 3!")]
        public int PackageTypeId { get; set; }

        [Required(ErrorMessage = "Hours cannot be empty!")]
        [Range(1, 3, ErrorMessage = "Hours must be between 1 to 3!")]
        public int Hours { get; set; }

        public int? BookedBy { get; set; }

        public bool AOSnack { get; set; }
        public bool AODrink { get; set; }

        public double Cost { get; set; }
    }
}
