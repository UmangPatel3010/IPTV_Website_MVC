using IPTV_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPTV_Website.Services
{
    public class MockTvDataService : IMockTvDataService
    {
        private readonly List<Channel> _channels = new List<Channel>
        {
            new Channel { Id = 101, Name = "Premier League TV",Url="https://amg00627-amg00627c23-samsung-au-4110.playouts.now.amagi.tv/1080p-vtt-ar/index.m3u8", Category = "Sports", Description = "Experience world-class sports in stunning 4K. Don't miss the action.", ImageUrl = "/assets/mock/sports.svg", ViewerLabel = "3.5M Watching", Language = "English", Quality = "4K", IsFavorite = true, ChannelNumber = 130, CurrentProgram = "The Kapil Sharma Show", CurrentProgramTime = "Now: Housefull 4 Time:12:30 To 16:30", NextProgramTime = "Housefull 4 Time : 16:30 To 18:30" },
            new Channel { Id = 102, Name = "USA News Network",Url="https://mumt04.tangotv.in/SHEMAROOUMANG/tracks-v1a1/mono.m3u8", Category = "News", Description = "Breaking headlines and live studio analysis for the evening audience.", ImageUrl = "/assets/mock/news.svg", ViewerLabel = "2.5M", Language = "English", Quality = "HD", IsFavorite = true, ChannelNumber = 201, CurrentProgram = "Prime Desk", CurrentProgramTime = "Now: Prime Desk 18:00 To 19:00", NextProgramTime = "World Brief 19:00 To 20:00" },
            new Channel { Id = 103, Name = "USA Sports Central",Url="http://ajkiptv.ridsys.in/riptv/live/HUNGAMA/index.m3u8", Category = "Sports", Description = "Big match build-up, fixtures, and studio highlights all day.", ImageUrl = "/assets/mock/sports.svg", ViewerLabel = "3.0M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 202, CurrentProgram = "Match Central", CurrentProgramTime = "Now: Match Central 17:00 To 19:00", NextProgramTime = "Sportsline 19:00 To 20:00" },
            new Channel { Id = 104, Name = "Hollywood Movies",Url="https://cdn-2.pishow.tv/live/415/415_1.m3u8", Category = "Movies", Description = "Movie premieres and studio favourites in a premium demo layout.", ImageUrl = "/assets/mock/cinema.svg", ViewerLabel = "4.1M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 303, CurrentProgram = "Movie Night", CurrentProgramTime = "Now: Movie Night 18:30 To 20:30", NextProgramTime = "Late Show 20:30 To 22:00" },
            new Channel { Id = 105, Name = "USA Entertainment",Url="https://mumt05.tangotv.in/UNIQUETV/tracks-v1a1/mono.m3u8", Category = "Entertainment", Description = "Reality shows, celebrity chat, and night programming.", ImageUrl = "/assets/mock/music.svg", ViewerLabel = "2.7M", Language = "English", Quality = "HD", IsFavorite = true, ChannelNumber = 404, CurrentProgram = "Tonight Live", CurrentProgramTime = "Now: Tonight Live 18:00 To 19:30", NextProgramTime = "After Hours 19:30 To 21:00" },
            new Channel { Id = 106, Name = "BBC World News",Url="https://d2l4ar6y3mrs4k.cloudfront.net/live-streaming/abpnews-livetv/master_720.m3u8", Category = "News", Description = "Global reporting with rolling live coverage throughout the day.", ImageUrl = "/assets/mock/news.svg", ViewerLabel = "3.2M", Language = "English", Quality = "HD", IsFavorite = true, ChannelNumber = 505, CurrentProgram = "BBC Now", CurrentProgramTime = "Now: BBC Now 18:00 To 19:00", NextProgramTime = "The World Today 19:00 To 20:00" },
            new Channel { Id = 107, Name = "British Cinema",Url="https://cdnb4u.wiseplayout.com/B4U_Movies/HD1080/HD1080.m3u8", Category = "Movies", Description = "Critically acclaimed cinema and curated features.", ImageUrl = "/assets/mock/cinema.svg", ViewerLabel = "3.5M", Language = "English", Quality = "4K", IsFavorite = false, ChannelNumber = 606, CurrentProgram = "Classic Cinema", CurrentProgramTime = "Now: Classic Cinema 18:00 To 20:00", NextProgramTime = "Directors Hour 20:00 To 21:00" },
            new Channel { Id = 108, Name = "UK Comedy Central",Url="https://livestream.unlimitedcdn.com/agm-dc/desi-channel/tracks-v1a1/mono.m3u8", Category = "Entertainment", Description = "Stand-up, sketches, and easy evening viewing.", ImageUrl = "/assets/mock/music.svg", ViewerLabel = "3.1M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 707, CurrentProgram = "Laugh House", CurrentProgramTime = "Now: Laugh House 18:30 To 19:30", NextProgramTime = "Sketch Night 19:30 To 20:30" },
            new Channel { Id = 109, Name = "Canadian Movies",Url="https://cdn-1.pishow.tv/live/235/235_1.m3u8", Category = "Movies", Description = "Premium movie selection with late-night favourites.", ImageUrl = "/assets/mock/cinema.svg", ViewerLabel = "4.3M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 808, CurrentProgram = "Evening Feature", CurrentProgramTime = "Now: Evening Feature 18:00 To 20:00", NextProgramTime = "Thriller Block 20:00 To 22:00" },
            new Channel { Id = 110, Name = "Canadian Entertainment",Url="https://cdn-3.pishow.tv/live/230/230_1.m3u8", Category = "Entertainment", Description = "Lifestyle, celebrity, and variety programming.", ImageUrl = "/assets/mock/music.svg", ViewerLabel = "4.2M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 809, CurrentProgram = "Variety Tonight", CurrentProgramTime = "Now: Variety Tonight 18:00 To 19:30", NextProgramTime = "Music Mix 19:30 To 21:00" },
            new Channel { Id = 111, Name = "ABC News Australia",Url="https://mumt01.tangotv.in/DDGIRNAR/tracks-v1a1/mono.m3u8", Category = "News", Description = "Live rolling news and analysis from Australia and abroad.", ImageUrl = "/assets/mock/news.svg", ViewerLabel = "4.0M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 901, CurrentProgram = "ABC Live", CurrentProgramTime = "Now: ABC Live 18:00 To 19:00", NextProgramTime = "Newsline 19:00 To 20:00" },
            new Channel { Id = 112, Name = "Australia Today",Url="https://mumt04.tangotv.in/DY365/tracks-v2a1/mono.m3u8", Category = "News", Description = "Headlines and current affairs with local focus.", ImageUrl = "/assets/mock/news.svg", ViewerLabel = "3.0M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 902, CurrentProgram = "Today Australia", CurrentProgramTime = "Now: Today Australia 18:00 To 19:00", NextProgramTime = "Evening Report 19:00 To 20:00" },
            new Channel { Id = 113, Name = "India News Live",Url="https://cdn-6.pishow.tv/live/10011/10011_0.m3u8", Category = "News", Description = "Fast headlines and live panels in prime time.", ImageUrl = "/assets/mock/news.svg", ViewerLabel = "3.6M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 903, CurrentProgram = "News Hour", CurrentProgramTime = "Now: News Hour 18:00 To 19:00", NextProgramTime = "Prime Debate 19:00 To 20:00" },
            new Channel { Id = 114, Name = "Cricket World India",Url="https://d3qs3d2rkhfqrt.cloudfront.net/out/v1/b17adfe543354fdd8d189b110617cddd/index_7.m3u8", Category = "Sports", Description = "Cricket coverage, match highlights, and expert commentary.", ImageUrl = "/assets/mock/sports.svg", ViewerLabel = "4.1M", Language = "English", Quality = "HD", IsFavorite = false, ChannelNumber = 904, CurrentProgram = "Cricket Extra", CurrentProgramTime = "Now: Cricket Extra 18:00 To 19:30", NextProgramTime = "World Match 19:30 To 21:00" }
        };

        private readonly List<SettingOption> _settingOptions = new List<SettingOption>
        {
            new SettingOption { Section = "Playback", Name = "Video Quality", Description = "Choose a demo playback quality preset.", Choices = new List<string> { "Auto", "4K", "HD", "SD" }, SelectedChoice = "Auto" },
            new SettingOption { Section = "Playback", Name = "Autoplay", Description = "Automatically continue to the next recommended channel.", IsToggle = true, IsEnabled = true },
            new SettingOption { Section = "Notifications", Name = "Push Notifications", Description = "Show placeholder on-screen alerts.", IsToggle = true, IsEnabled = true },
            new SettingOption { Section = "Notifications", Name = "Email Updates", Description = "Receive demo email updates.", IsToggle = true, IsEnabled = true },
            new SettingOption { Section = "Notifications", Name = "New Content Alerts", Description = "Get notified when new demo content appears.", IsToggle = true, IsEnabled = true },
            new SettingOption { Section = "Appearance", Name = "Theme", Description = "Preview the theme selection.", Choices = new List<string> { "Red Gradient", "Graphite", "Cinema" }, SelectedChoice = "Red Gradient" },
            new SettingOption { Section = "Language", Name = "Language", Description = "Demo language selection for the TV UI.", Choices = new List<string> { "English", "Hindi", "Spanish" }, SelectedChoice = "English" }
        };

        public IReadOnlyList<Channel> GetTrendingChannels() => _channels;
        public IReadOnlyList<Channel> GetContinueWatchingChannels() => _channels.Skip(8).Take(4).ToList();
        public IReadOnlyList<Channel> GetFavoriteChannels() => _channels.Where(channel => channel.IsFavorite).Take(4).ToList();
        public IReadOnlyList<Channel> GetAllChannels() => _channels;
        public IReadOnlyList<SettingOption> GetSettingOptions() => _settingOptions;
        public Channel GetHeroChannel() => _channels.First();
        public Channel GetChannel(int? channelId) => _channels.FirstOrDefault(channel => channel.Id == channelId) ?? _channels.First();

        public IReadOnlyList<Channel> SearchChannels(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return _channels;
            }

            return _channels.Where(channel => channel.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || channel.Category.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
