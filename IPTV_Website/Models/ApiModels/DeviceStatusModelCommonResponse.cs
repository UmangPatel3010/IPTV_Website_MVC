using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class DeviceStatusModelCommonResponse
    {
        [JsonPropertyName("data")]
        public List<DeviceStatusModel>? Data { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}