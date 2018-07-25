using System;
using System.Collections.Generic;

namespace P13.Models
{
    public partial class HappinessLevel
    {
        public HappinessLevel()
        {
            Member = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Level { get; set; }

        public virtual ICollection<Member> Member { get; set; }
    }
}
