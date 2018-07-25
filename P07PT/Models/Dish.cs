using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P07PT.Models
{
    public class Dish
    {
        public int Id { get; set; }

		// TODO P07 Task 1a: Data annotate Dish class' properties based on data validation requirements
		[Required(ErrorMessage ="Name cannot be blank.")]
		[StringLength(50,MinimumLength =5,ErrorMessage ="Name has to be 5 to 50 Characters long")]
		public string Name { get; set; }
		[Required(ErrorMessage ="Price is required")]
		[Range(1,200,ErrorMessage ="Price must be at least SGD 1, but cannot exceed SGD 200")]
        public double Price { get; set; }
		[Required(ErrorMessage ="Please select availability - Full day, Breakfast, Lunch and Dinner")]
        public int Availability { get; set; }
		[Required(ErrorMessage ="Please select a cuisine.")]
        public int CuisineId { get; set; }
        public bool Recommended { get; set; }

    }
}

