using System;
using System.Collections.Generic;

namespace P11.Models
{
    public partial class Module
    {
        public Module()
        {
            Result = new HashSet<Result>();
        }

        public string Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Result> Result { get; set; }
    }
}
