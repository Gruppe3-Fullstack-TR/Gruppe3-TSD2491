// PollenRegistering.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace GruppeX.Models
{
    public class PollenRegistering
    {
        public int Id { get; set; }
        
        [Required]
        public string PollenType { get; set; }
        
        [Required]
        public int Concentration { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        
        public string Location { get; set; }
        
        public string RegisteredBy { get; set; }
    }
}

