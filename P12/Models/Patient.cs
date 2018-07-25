using System;
using System.Collections.Generic;

namespace P12.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
        }

        public string Id { get; set; }
        public string Icpassport { get; set; }
        public string Name { get; set; }
        public int Citizenship { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
