using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class EpgData
    {
        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("lcnNo")]
        public long LcnNo { get; set; }

        [JsonPropertyName("beforeDisplayName")]
        public string? BeforeDisplayName { get; set; }

        [JsonPropertyName("afterDisplayName")]
        public string? AfterDisplayName { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("programs")]
        public List<Programs>? Programs { get; set; }
    }
}