using System;

namespace P03.Models
{
   // TODO: P03 Task 1 - Change the properties RegNo and Height to Nullable 
   public class Candidate
   {
      public int? RegNo { get; set; }
      public string Name { get; set; }
      public string Gender { get; set; }
      public double? Height { get; set; }
      public DateTime BirthDate { get; set; }
      public string Race { get; set; }
      public bool Clearance { get; set; }
      public string PicFile { get; set; }
   }
}


