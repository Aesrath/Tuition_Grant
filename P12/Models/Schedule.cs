using System;
using System.Collections.Generic;

namespace P12.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string Weekday { get; set; }
    }
}
