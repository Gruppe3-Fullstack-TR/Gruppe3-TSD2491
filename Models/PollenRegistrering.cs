// PollenRegistering.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace Gruppe3.Models
{
    public class PollenRegistering
    {
        public int Id { get; set; }
        
        [Required]
        public string TypeOfPollen { get; set; }
        
        [Required]
        public int Level { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}

