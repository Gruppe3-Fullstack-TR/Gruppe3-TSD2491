// DateInfo.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace Gruppe3.Models
{
    public class DateInfo
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public ICollection<PollenResponse> PollenResponses { get; set; }
    }
}