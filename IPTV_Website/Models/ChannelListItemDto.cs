namespace IPTV_Website.Models
{
    public class ChannelListItemDto
    {
        public long ServiceID { get; set; }
        public string AppDisplayName { get; set; } = string.Empty;
        public int LcnNo { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
