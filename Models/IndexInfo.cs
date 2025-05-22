using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gruppe3.Models
{
    public class IndexInfo
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public int Value { get; set; }
        public string Category { get; set; }
        public string IndexDescription { get; set; }

        [ForeignKey("ColorInfo")]
        public int ColorInfoId { get; set; }
        public ColorInfo ColorInfo { get; set; }

        public ICollection<PlantInfo> PlantInfos { get; set; }
        public DateTime Date { get; set; } // <-- Legg til denne
    }
}
