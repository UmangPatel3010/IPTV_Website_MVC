using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class LanguageWiseChannel
    {
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("subscriberChannels")]
        public List<SubscriberChannelsModelCommonResponse>? SubscriberChannels { get; set; }
    }
}
