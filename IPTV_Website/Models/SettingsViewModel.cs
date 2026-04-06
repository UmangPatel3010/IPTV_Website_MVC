namespace IPTV_Website.Models
{
    public class SettingsViewModel
    {
        public List<SettingOption> PlaybackOptions { get; set; } = new List<SettingOption>();
        public List<SettingOption> NotificationOptions { get; set; } = new List<SettingOption>();
        public List<SettingOption> AppearanceOptions { get; set; } = new List<SettingOption>();
        public List<SettingOption> LanguageOptions { get; set; } = new List<SettingOption>();
    }
}