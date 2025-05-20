// IndexInfo.cs
namespace GruppeX.Models
{
    public class IndexInfo
    {
        public int Id { get; set; }
        public string PollenType { get; set; }
        public int Value { get; set; }
        public int ColorInfoId { get; set; }
        public ColorInfo ColorInfo { get; set; }
    }
}
