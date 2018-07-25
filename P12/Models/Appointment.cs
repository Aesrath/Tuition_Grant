using System;
using System.Collections.Generic;

namespace P12.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public DateTime AppDate { get; set; }
        public string DoctorId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
