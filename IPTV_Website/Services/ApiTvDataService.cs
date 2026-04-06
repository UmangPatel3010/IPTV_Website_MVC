using IPTV_Website.Models;
using IPTV_Website.Services;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiTvDataService : IMockTvDataService
{
    private readonly HttpClient _httpClient;

    public ApiTvDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IReadOnlyList<Channel> GetAllChannels()
    {
        throw new NotImplementedException();
    }

    public Channel GetChannel(int? channelId)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Channel> GetContinueWatchingChannels()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Channel> GetFavoriteChannels()
    {
        throw new NotImplementedException();
    }

    public Channel GetHeroChannel()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<SettingOption> GetSettingOptions()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Channel> GetTrendingChannels()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Channel>> GetTrendingChannelsAsync()
    {
        var response = await _httpClient.GetAsync("api/channels/trending");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Channel>>(json);
    }

    public IReadOnlyList<Channel> SearchChannels(string? query)
    {
        throw new NotImplementedException();
    }

    // Add other methods for fetching data as needed
}