using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P13.Models
{
    public partial class Member
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Membership name cannot be blank")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please select at least one happiness level")]
		[Range(1,5,ErrorMessage ="Must be between 1 to 5")]
		public int HappinessLevelId { get; set; }
		[Required(ErrorMessage = "Phone cannot be blank")]
		[RegularExpression(@"[3689]\d{7}")]
		public string Phone { get; set; }

        public virtual HappinessLevel HappinessLevel { get; set; }
    }
}
