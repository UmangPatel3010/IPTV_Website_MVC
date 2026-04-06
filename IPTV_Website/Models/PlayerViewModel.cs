using System.Collections.Generic;

namespace IPTV_Website.Models
{
    public class PlayerViewModel
    {
        public Channel Channel { get; set; }
        public List<Channel> AllChannels { get; set; } = new List<Channel>();
        public int? NextChannelId { get; set; }
        public int? PrevChannelId { get; set; }
    }
}