using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class SubscriptionDetailModelCommonResponse
    {
        [JsonPropertyName("data")]
        public List<SubscriptionDetailModel>? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}