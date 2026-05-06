using IPTV_Website.Models.ApiModels;

namespace IPTV_Website.Models
{
    public class ChannelPlayViewModel
    {
        public long ServiceNo { get; set; }
        public string ChannelUrl { get; set; } = string.Empty;
        public List<EpgData> EpgData { get; set; } = new List<EpgData>();
        public List<ChannelListItemDto> AllChannels { get; set; } = new List<ChannelListItemDto>();
        public long? PrevServiceNo { get; set; }
        public long? NextServiceNo { get; set; }
    }
}
