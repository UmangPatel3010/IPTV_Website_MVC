using IPTV_Website.Helpers;
using IPTV_Website.Models;
using IPTV_Website.Models.ApiModels;
using Newtonsoft.Json;
using System.Text;

public class ApiTvDataService
{
    private readonly HttpClient _httpClient;

    public ApiTvDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> License(string requestBody)
    {
        await EnsureToken();
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        using var client = new HttpClient(handler)
        {
            DefaultRequestHeaders =
            {
                { "IsApplication", "app" },
                { "token", CommonVariables.getToken() }
            }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, ApiEndpoints.LicenseURL)
        {
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(request);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task EnsureToken()
    {
        var token = CommonVariables.getToken();
        try
        {

            // If no token stored, fetch a new one
            if (string.IsNullOrEmpty(token))
            {
                var response = await _httpClient.GetAsync(ApiEndpoints.GenerateToken);
                response.EnsureSuccessStatusCode();
                token = await response.Content.ReadAsStringAsync();

                CommonVariables.setToken(token);
            }

            // Add header only if not already present
            if (!_httpClient.DefaultRequestHeaders.Contains("token"))
            {
                _httpClient.DefaultRequestHeaders.Add("token", token);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
    }

    private static TResponse CreateErrorResponse<TResponse>(HttpResponseMessage response) where TResponse : new()
    {
        var errorResponse = new TResponse();
        var responseType = typeof(TResponse);

        responseType.GetProperty("Data")?.SetValue(errorResponse, null);
        responseType.GetProperty("Message")?.SetValue(errorResponse, response.ReasonPhrase);
        responseType.GetProperty("StatusCode")?.SetValue(errorResponse, (long)response.StatusCode);
        responseType.GetProperty("IsSuccess")?.SetValue(errorResponse, false);

        return errorResponse;
    }

    public async Task<ViewLogModelCommonResponse> APPViewLogs(ViewLogModel viewLogModel)
    {
        await EnsureToken();
        var response = await _httpClient.PostAsJsonAsync(ApiEndpoints.App_ViewLogs, viewLogModel);

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ViewLogModelCommonResponse>(jsonData) ?? new ViewLogModelCommonResponse();
        }

        return CreateErrorResponse<ViewLogModelCommonResponse>(response);
    }

    public async Task<CategoryWiseChannelCommonResponse> APPChannels()
    {
        await EnsureToken();
        try
        {
            var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.App_Channels, CommonVariables.getSubNo()));

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CategoryWiseChannelCommonResponse>(jsonData) ?? new CategoryWiseChannelCommonResponse();
            }

            return CreateErrorResponse<CategoryWiseChannelCommonResponse>(response);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<CmnResponse<string>> DashboardImages()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(ApiEndpoints.Images);

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData) ?? new CmnResponse<string>();
        }

        return CreateErrorResponse<CmnResponse<string>>(response);
    }

    public async Task<EpgDataCommonResponse> EPGGet(long serviceID)
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.EPG_Get, serviceID));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EpgDataCommonResponse>(jsonData) ?? new EpgDataCommonResponse();
        }

        return CreateErrorResponse<EpgDataCommonResponse>(response);
    }

    public async Task<CmnResponse<string>> FingerPrintGet()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.FingerPrint_Get, CommonVariables.getDeviceNo()));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData) ?? new CmnResponse<string>();
        }

        return CreateErrorResponse<CmnResponse<string>>(response);
    }

    public async Task<DeviceStatusModelCommonResponse> APPDeviceStatus()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.APP_DeviceStatus, CommonVariables.getDeviceNo()));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeviceStatusModelCommonResponse>(jsonData) ?? new DeviceStatusModelCommonResponse();
        }

        return CreateErrorResponse<DeviceStatusModelCommonResponse>(response);
    }

    public async Task<CmnResponse<string>> OSDGet()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.OSD_Get, CommonVariables.getDeviceNo()));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CmnResponse<string>>(jsonData) ?? new CmnResponse<string>();
        }

        return CreateErrorResponse<CmnResponse<string>>(response);
    }

    public async Task<SubscriberChannelsModelCommonResponse> APPSubscribedChannels()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.APP_SubscribedChannels, CommonVariables.getSubNo()));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SubscriberChannelsModelCommonResponse>(jsonData) ?? new SubscriberChannelsModelCommonResponse();
        }

        return CreateErrorResponse<SubscriberChannelsModelCommonResponse>(response);
    }

    public async Task<SubscriptionDetailModelCommonResponse> APPSubscribed()
    {
        await EnsureToken();
        var response = await _httpClient.GetAsync(string.Format(ApiEndpoints.GetSubscriptionBySubscriber, CommonVariables.getDeviceNo()));

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SubscriptionDetailModelCommonResponse>(jsonData) ?? new SubscriptionDetailModelCommonResponse();
        }

        return CreateErrorResponse<SubscriptionDetailModelCommonResponse>(response);
    }
}