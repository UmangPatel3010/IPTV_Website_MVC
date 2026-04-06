namespace IPTV_Website.Models
{
    public class HomeViewModel
    {
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public List<Channel> TrendingChannels { get; set; } = new List<Channel>();
        public Channel HeroChannel { get; set; }
    }
}