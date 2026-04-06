using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class AppVersion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("apkPath")]
        public string? ApkPath { get; set; }

        [JsonPropertyName("isForceUpdate")]
        public bool? IsForceUpdate { get; set; }

        [JsonPropertyName("createDate")]
        public DateTime? CreateDate { get; set; }

        [JsonPropertyName("createById")]
        public int? CreateById { get; set; }

        [JsonPropertyName("updateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonPropertyName("updateById")]
        public int? UpdateById { get; set; }

        [JsonPropertyName("deleteDate")]
        public DateTime? DeleteDate { get; set; }

        [JsonPropertyName("deleteById")]
        public int? DeleteById { get; set; }

        /// <summary>
        /// Marked as required in Swagger
        /// </summary>
        [JsonPropertyName("currentVersion")]
        public int CurrentVersion { get; set; }

        /// <summary>
        /// Marked as required in Swagger
        /// </summary>
        [JsonPropertyName("latestVersion")]
        public int LatestVersion { get; set; }
    }
}