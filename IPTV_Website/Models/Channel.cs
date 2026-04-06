namespace IPTV_Website.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Quality { get; set; } = "HD";
        public string ViewerLabel { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public int ChannelNumber { get; set; }
        public string CurrentProgram { get; set; } = string.Empty;
        public string CurrentProgramTime { get; set; } = string.Empty;
        public string NextProgramTime { get; set; } = string.Empty;
    }
}