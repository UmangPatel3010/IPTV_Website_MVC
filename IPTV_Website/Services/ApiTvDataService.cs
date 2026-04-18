using IPTV_Website.Helpers;
using IPTV_Website.Models.ApiModels;
using Newtonsoft.Json;

public class ApiTvDataService 
{
    private readonly HttpClient _httpClient;

    public ApiTvDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task EnsureToken()
    {
        var token = CommonVariables.getToken();

        // If no token stored, fetch a new one
        if (string.IsNullOrEmpty(token))
        {
            var response = await _httpClient.GetAsync(ApiEndpoints.GenerateToken);
            token = await response.Content.ReadAsStringAsync();

            CommonVariables.setToken(token);
        }

        // Add header only if not already present
        if (!_httpClient.DefaultRequestHeaders.Contains("Token"))
        {
            _httpClient.DefaultRequestHeaders.Add("Token", token);
        }
    }
    public async Task<ViewLogModelCommonResponse> APPViewLogs(ViewLogModel viewLogModel)
    {
        await EnsureToken();
        var response = _httpClient.PostAsJsonAsync($"{ApiEndpoints.App_ViewLogs}",viewLogModel).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var viewLogModelCommonResponse = JsonConvert.DeserializeObject<ViewLogModelCommonResponse>(jsonData);

        return viewLogModelCommonResponse;
    }

    public async Task<CategoryWiseChannelCommonResponse> APPChannels()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.App_Channels,CommonVariables.getSubNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var categoryWiseChannelCommonResponse = JsonConvert.DeserializeObject<CategoryWiseChannelCommonResponse>(jsonData);

        return categoryWiseChannelCommonResponse;
    }
    public async Task<CmnResponse<string>> DashboardImages()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(ApiEndpoints.Images).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<EpgDataCommonResponse> EPGGet(int serviceID)
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.EPG_Get,serviceID)).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var epgDataCommonResponse = JsonConvert.DeserializeObject<EpgDataCommonResponse>(jsonData);

        return epgDataCommonResponse;
    }

    public async Task<CmnResponse<string>> FingerPrintGet()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.FingerPrint_Get, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<DeviceStatusModelCommonResponse> APPDeviceStatus()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.APP_DeviceStatus, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var deviceStatusModelCommonResponse = JsonConvert.DeserializeObject<DeviceStatusModelCommonResponse>(jsonData);

        return deviceStatusModelCommonResponse;
    }
    public async Task<CmnResponse<string>> OSDGet()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.OSD_Get, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<SubscriberChannelsModelCommonResponse> APPSubscribedChannels()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.APP_SubscribedChannels, CommonVariables.getSubNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var subscriberChannelsModelCommonResponse = JsonConvert.DeserializeObject<SubscriberChannelsModelCommonResponse>(jsonData);

        return subscriberChannelsModelCommonResponse;
    }
    public async Task<SubscriptionDetailModelCommonResponse> APPSubscribed()
    {
        await EnsureToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.GetSubcriptionBySubsciber, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var subscriptionDetailModelCommonResponse = JsonConvert.DeserializeObject<SubscriptionDetailModelCommonResponse>(jsonData);

        return subscriptionDetailModelCommonResponse;
    }
}