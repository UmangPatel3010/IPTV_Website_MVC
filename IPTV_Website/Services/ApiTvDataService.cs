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

    public async void GenerateToken()
    {
        var response = _httpClient.GetAsync($"{ApiEndpoints.GenerateToken}").Result;
        var token = await response.Content.ReadAsStringAsync();
        _httpClient.DefaultRequestHeaders.Add("Token", token);
        CommonVariables.setToken(token);
        //return token;
    }
    public async Task<ViewLogModelCommonResponse> APPViewLogs(ViewLogModel viewLogModel)
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.PostAsJsonAsync($"{ApiEndpoints.App_ViewLogs}",viewLogModel).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var viewLogModelCommonResponse = JsonConvert.DeserializeObject<ViewLogModelCommonResponse>(jsonData);

        return viewLogModelCommonResponse;
    }

    public async Task<CategoryWiseChannelCommonResponse> APPChannels()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.App_Channels,CommonVariables.getSubNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var categoryWiseChannelCommonResponse = JsonConvert.DeserializeObject<CategoryWiseChannelCommonResponse>(jsonData);

        return categoryWiseChannelCommonResponse;
    }
    public async Task<CmnResponse<string>> DashboardImages()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(ApiEndpoints.Images).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<EpgDataCommonResponse> EPGGet(int serviceID)
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.EPG_Get,serviceID)).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var epgDataCommonResponse = JsonConvert.DeserializeObject<EpgDataCommonResponse>(jsonData);

        return epgDataCommonResponse;
    }

    public async Task<CmnResponse<string>> FingerPrintGet()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.FingerPrint_Get, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<DeviceStatusModelCommonResponse> APPDeviceStatus()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.APP_DeviceStatus, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var deviceStatusModelCommonResponse = JsonConvert.DeserializeObject<DeviceStatusModelCommonResponse>(jsonData);

        return deviceStatusModelCommonResponse;
    }
    public async Task<CmnResponse<string>> OSDGet()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.OSD_Get, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var cmnResponse = JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData);

        return cmnResponse;
    }
    public async Task<SubscriberChannelsModelCommonResponse> APPSubscribedChannels()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.APP_SubscribedChannels, CommonVariables.getSubNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var subscriberChannelsModelCommonResponse = JsonConvert.DeserializeObject<SubscriberChannelsModelCommonResponse>(jsonData);

        return subscriberChannelsModelCommonResponse;
    }
    public async Task<SubscriptionDetailModelCommonResponse> APPSubscribed()
    {
        if (CommonVariables.GetToken() == null)
            GenerateToken();
        var response = _httpClient.GetAsync(string.Format(ApiEndpoints.GetSubcriptionBySubsciber, CommonVariables.getDeviceNo())).Result;
        var jsonData = await response.Content.ReadAsStringAsync();
        var subscriptionDetailModelCommonResponse = JsonConvert.DeserializeObject<SubscriptionDetailModelCommonResponse>(jsonData);

        return subscriptionDetailModelCommonResponse;
    }
}