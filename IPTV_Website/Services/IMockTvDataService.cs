using IPTV_Website.Models;
using System.Collections.Generic;

namespace IPTV_Website.Services
{
    public interface IMockTvDataService
    {
        IReadOnlyList<Channel> GetTrendingChannels();
        IReadOnlyList<Channel> GetContinueWatchingChannels();
        IReadOnlyList<Channel> GetFavoriteChannels();
        IReadOnlyList<Channel> GetAllChannels();
        IReadOnlyList<Channel> SearchChannels(string? query);
        IReadOnlyList<SettingOption> GetSettingOptions();
        Channel GetHeroChannel();
        Channel GetChannel(int? channelId);
    }
}