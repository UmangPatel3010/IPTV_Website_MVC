using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class Programs
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("stopDate")]
        public DateTime StopDate { get; set; }
    }
}

