using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P11.Models
{
   public class LoginUser
   {
      [Required(ErrorMessage = "User ID cannot be empty!")]
        [RegularExpression(@"\d{3}",ErrorMessage ="Please use your 3-digits student Id to login!")]
      public string UserId { get; set; }

      [Required(ErrorMessage = "Empty password not allowed!")]
      [DataType(DataType.Password)]
      public string Password { get; set; }
   }
}
