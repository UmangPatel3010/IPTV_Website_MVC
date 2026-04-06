using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class AppVersionModelCommonResponse
    {
        [JsonPropertyName("data")]
        public List<AppVersionModel>? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}