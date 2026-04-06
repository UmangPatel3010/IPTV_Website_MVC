using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class DeviceStatusModel
    {
        [JsonPropertyName("subName")]
        public string? SubName { get; set; }

        [JsonPropertyName("subNo")]
        public string? SubNo { get; set; }

        [JsonPropertyName("deviceNo")]
        public string? DeviceNo { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}