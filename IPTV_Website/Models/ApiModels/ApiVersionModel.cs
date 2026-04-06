using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class AppVersionModel
    {
        [Required]
        [MinLength(1)]
        [RegularExpression(@"^[0-9a-fA-F]{12}$|^[0-9a-fA-F]{16}$")]
        [JsonPropertyName("deviceNo")]
        public string DeviceNo { get; set; } = string.Empty;

        [Range(1, 2147483647)]
        [JsonPropertyName("version")]
        public int Version { get; set; }
    }
}