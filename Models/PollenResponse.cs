// PollenResponse.cs
namespace GruppeX.Models
{
    public class PollenResponse
    {
        public int Id { get; set; }
        public int DateInfoId { get; set; }
        public DateInfo DateInfo { get; set; }
        public int IndexInfoId { get; set; }
        public IndexInfo IndexInfo { get; set; }
    }
}
