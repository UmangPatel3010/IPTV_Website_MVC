using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class SubscriberChannelsModelCommonResponse
    {
        [JsonPropertyName("data")]
        public List<SubscriberChannelsModel>? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}