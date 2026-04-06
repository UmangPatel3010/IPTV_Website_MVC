using System.Text.Json.Serialization;

namespace IPTV_Website.Models.ApiModels
{
    public class ViewLogModel
    {
        [JsonPropertyName("deviceNo")]
        public string? DeviceNo { get; set; }

        [JsonPropertyName("serviceID")]
        public long ServiceID { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}