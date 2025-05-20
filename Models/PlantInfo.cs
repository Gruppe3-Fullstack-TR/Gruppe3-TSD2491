using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gruppe3.Models
{
    public class PlantInfo
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public bool InSeason { get; set; }

        [ForeignKey("IndexInfo")]
        public int IndexInfoId { get; set; }
        public IndexInfo IndexInfo { get; set; }

        public ICollection<PollenResponse> PollenResponses { get; set; }
    }
}
