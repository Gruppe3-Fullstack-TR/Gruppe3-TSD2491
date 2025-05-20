using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gruppe3.Models
{
    public class PollenApiResponse
    {
        [JsonPropertyName("dailyInfo")]
        public List<PollenDay> DailyInfo { get; set; }
    }

    public class PollenDay
    {
        [JsonPropertyName("date")]
        public PollenDate Date { get; set; }

        [JsonPropertyName("pollenTypeInfo")]
        public List<PollenTypeInfo> PollenTypeInfo { get; set; }
    }

    public class PollenTypeInfo
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("inSeason")]
        public bool? InSeason { get; set; }

        [JsonPropertyName("indexInfo")]
        public PollenIndex IndexInfo { get; set; }
    }

    public class PollenIndex
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("indexDescription")]
        public string IndexDescription { get; set; }

        [JsonPropertyName("color")]
        public PollenColor Color { get; set; }
    }

    public class PollenColor
    {
        [JsonPropertyName("red")]
        public float? Red { get; set; }

        [JsonPropertyName("green")]
        public float? Green { get; set; }

        [JsonPropertyName("blue")]
        public float? Blue { get; set; }
    }

    public class PollenDate
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("day")]
        public int Day { get; set; }
    }
}