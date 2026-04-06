using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class SubscriberChannelsModel
    {
        [JsonPropertyName("channelName")]
        public string? ChannelName { get; set; }

        [JsonPropertyName("appDisplayName")]
        public string? AppDisplayName { get; set; }

        [JsonPropertyName("lcnNo")]
        public int LcnNo { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("channelUrl")]
        public string? ChannelUrl { get; set; }

        [JsonPropertyName("isFpEnable")]
        public bool IsFpEnable { get; set; }

        [JsonPropertyName("isEncrypt")]
        public bool IsEncrypt { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("serviceID")]
        public long ServiceID { get; set; }

        [JsonPropertyName("isSubscribed")]
        public bool IsSubscribed { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("signalType")]
        public string? SignalType { get; set; }

        [JsonPropertyName("broadcaster")]
        public string? Broadcaster { get; set; }

        [JsonPropertyName("categoryLogo")]
        public string? CategoryLogo { get; set; }
    }
}
