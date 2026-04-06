namespace IPTV_Website.Models
{
    public class FavoritesViewModel
    {
        public bool Empty { get; set; }
        public List<Channel> FavoriteChannels { get; set; } = new List<Channel>();
    }
}