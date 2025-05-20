// PollenRegistering.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace Gruppe3.Models
{
    public class PollenRegistering
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Pollen type")]
        public string TypeOfPollen { get; set; }
        
        [Required]
        [Display(Name = "Nivå")]
        public int Level { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; }

    }
}

