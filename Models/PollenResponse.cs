using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gruppe3.Models
{
    public class PollenResponse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DateInfo")]
        public int DateInfoId { get; set; }
        public DateInfo DateInfo { get; set; }

        [ForeignKey("PlantInfo")]
        public int PlantInfoId { get; set; }
        public PlantInfo PlantInfo { get; set; }
    }
}
