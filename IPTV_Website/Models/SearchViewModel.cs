using System.Collections.Generic;

namespace IPTV_Website.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; } = string.Empty;
        public IReadOnlyList<Channel> Results { get; set; } = new List<Channel>();
        public IReadOnlyList<string> PopularTerms { get; set; } = new List<string>();
    }
}