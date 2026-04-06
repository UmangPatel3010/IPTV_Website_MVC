using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class CategoryWiseChannel
    {
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("categoryLogo")]
        public string? CategoryLogo { get; set; }

        [JsonPropertyName("languageChannels")]
        public List<LanguageWiseChannel>? LanguageChannels { get; set; }
    }
   
}