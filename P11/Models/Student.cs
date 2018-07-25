using System;
using System.Collections.Generic;

namespace P11.Models
{
    public partial class Student
    {
        public Student()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] Password { get; set; }

        public virtual ICollection<Result> Result { get; set; }
    }
}
