using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSA.Models
{
	// Task to do: Annotate the properties based on the given validation requirements

	public class Donation

	{
		
		public int Id { get; set; }
		[Required(ErrorMessage = "Only registered donator can donate")]
		[RegularExpression("")]
		public string DonorCode { get; set; }

		public double Amount { get; set; }

		public string Encouragement { get; set; }

	}

}

