using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class SubscriptionList
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("serviceID")]
        public int ServiceID { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }
}
