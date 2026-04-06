using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class SubscriptionDetailModel
    {
        [Required]
        [MinLength(1)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [JsonPropertyName("subNo")]
        public string SubNo { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [Required]
        [Range(100000, 999999)]
        [JsonPropertyName("pincode")]
        public long Pincode { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Required]
        [MinLength(1)]
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        [Range(1, 2)]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("deviceNo")]
        public string? DeviceNo { get; set; }

        [JsonPropertyName("serialNo")]
        public string? SerialNo { get; set; }

        [JsonPropertyName("noOfChannels")]
        public int NoOfChannels { get; set; }

        [JsonPropertyName("subscriptions")]
        public List<SubscriptionList>? Subscriptions { get; set; }
    }
}