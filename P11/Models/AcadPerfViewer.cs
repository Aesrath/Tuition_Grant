using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P11.Models
{
    public class AcadPerfViewer
    {
        public Student CurrentStudent { get; set; }
        public int Semester { get; set; }
        public List<Result> AcadResult { get; set; }
        public string Feedback { get; set; }
    }
}