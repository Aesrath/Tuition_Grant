using System;
using System.Collections.Generic;

namespace P11.Models
{
    public partial class Result
    {
        public int Id { get; set; }
        public int Semester { get; set; }
        public int StudentId { get; set; }
        public string ModuleId { get; set; }
        public string Grade { get; set; }

        public virtual Module Module { get; set; }
        public virtual Student Student { get; set; }
    }
}
