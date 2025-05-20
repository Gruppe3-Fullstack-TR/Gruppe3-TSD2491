using System.Collections.Generic;

namespace Gruppe3.Models
{
    public class PollenApiResponse
    {
        public List<PollenDay> dailyInfo { get; set; }
    }

    public class PollenDay
    {
        public string date { get; set; }
        public List<PollenIndex> indexes { get; set; }
    }

    public class PollenIndex
    {
        public string code { get; set; }
        public string displayName { get; set; }
        public int value { get; set; }
        public string category { get; set; }
        public string indexDescription { get; set; }
        public PollenColor color { get; set; }
    }

    public class PollenColor
    {
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }
    }
}