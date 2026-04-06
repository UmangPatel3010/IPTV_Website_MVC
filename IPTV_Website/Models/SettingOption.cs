using System.Collections.Generic;

namespace IPTV_Website.Models
{
    public class SettingOption
    {
        public string Section { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsToggle { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> Choices { get; set; } = new List<string>();
        public string SelectedChoice { get; set; } = string.Empty;
    }
}