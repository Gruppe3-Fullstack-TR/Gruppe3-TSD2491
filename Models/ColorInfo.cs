using System.ComponentModel.DataAnnotations;

namespace Gruppe3.Models
{
    public class ColorInfo
    {
        [Key]
        public int Id { get; set; }
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }

        public ICollection<IndexInfo> IndexInfos { get; set; }
    }
}
