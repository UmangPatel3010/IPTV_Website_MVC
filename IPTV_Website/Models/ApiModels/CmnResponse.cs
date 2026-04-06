using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    /// <summary>
    /// A generic wrapper for all API responses.
    /// </summary>
    /// <typeparam name="T">The type of the data object or list being returned.</typeparam>
    public class CmnResponse<T>
    {
        [JsonPropertyName("data")]
        public List<T>? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}