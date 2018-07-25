using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESE.Models
{
	public partial class Competitor
	{
		public string Id { get; set; }
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter competitor's name.")]
		public int SkillAreaId { get; set; }
		[Range(1, 26, ErrorMessage = "Invalid skill area selected.")]
		public bool IsFirstTime { get; set; }
	}
}
