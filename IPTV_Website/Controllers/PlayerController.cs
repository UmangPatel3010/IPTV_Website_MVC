using IPTV_Website.Helpers;
using IPTV_Website.Models;
using IPTV_Website.Models.ApiModels;
using IPTV_Website.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IPTV_Website.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IMockTvDataService _tvDataService;

        public PlayerController( IMockTvDataService tvDataService)
        {
            _tvDataService = tvDataService;
        }
        public IActionResult Index(int? channelId)
        {
            return TestEndpoint();
        }

        public IActionResult TestEndpoint()
        {
            var model = TestData();

            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> License()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            using var client = new HttpClient(handler);

            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://182.237.14.221/AcquireLicense");

            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            request.Headers.Add("IsApplication", "app");
            request.Headers.Add("Token", "xd2jsyci5WV+XGedvEfxOg==");

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
        public IActionResult ChannelPlay(long serviceNo)
        {
            var now = DateTime.Now;
            
            var selectedChannel = TestData()
                .SelectMany(category => category.LanguageChannels ?? new List<LanguageWiseChannel>())
                .SelectMany(language => language.SubscriberChannels ?? new List<SubscriberChannelsModel>())
                .FirstOrDefault(ch => ch.ServiceID == serviceNo);

            // If channel not found, return not found
            if (selectedChannel == null)
                return NotFound();

            var epg = new EpgDataCommonResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Success",
                Data = new List<EpgData>
                {
                    new EpgData
                    {
                        Channel = $"Service {serviceNo}",
                        DisplayName = selectedChannel.AppDisplayName ?? selectedChannel.ChannelName,
                        LcnNo = serviceNo,
                        BeforeDisplayName = "Previous Show",
                        AfterDisplayName = "Next Show",
                        Logo = null,
                        Programs = new List<Programs>
                        {
                            new Programs
                            {
                                Name = "Morning News",
                                Description = "Sample program from mock EPG response",
                                StartDate = now.AddMinutes(-30),
                                StopDate = now.AddMinutes(30)
                            },
                            new Programs
                            {
                                Name = "Midday Bulletin",
                                Description = "Upcoming sample program",
                                StartDate = now.AddMinutes(30),
                                StopDate = now.AddMinutes(90)
                            },
                            new Programs
                            {
                                Name = "Evening Update",
                                Description = "Later sample program",
                                StartDate = now.AddMinutes(90),
                                StopDate = now.AddMinutes(150)
                            }
                        }
                    }
                }
            };

            var orderedChannels = TestData()
                .SelectMany(category => category.LanguageChannels ?? new List<LanguageWiseChannel>())
                .SelectMany(language => language.SubscriberChannels ?? new List<SubscriberChannelsModel>())
                .OrderBy(channel => channel.LcnNo)
                .ToList();

            var currentIndex = orderedChannels.FindIndex(channel => channel.ServiceID == serviceNo);
            var prevServiceNo = currentIndex > 0 ? orderedChannels[currentIndex - 1].ServiceID : orderedChannels.LastOrDefault()?.ServiceID;
            var nextServiceNo = currentIndex >= 0 && currentIndex < orderedChannels.Count - 1
                ? orderedChannels[currentIndex + 1].ServiceID
                : orderedChannels.FirstOrDefault()?.ServiceID;

            // Map all channels to DTO with only required fields
            var allChannels = orderedChannels
                .Select(channel => new ChannelListItemDto
                {
                    ServiceID = channel.ServiceID,
                    AppDisplayName = channel.AppDisplayName ?? channel.ChannelName ?? "Unknown",
                    LcnNo = channel.LcnNo,
                    Category = channel.Category ?? string.Empty
                })
                .ToList();

            var viewModel = new ChannelPlayViewModel
            {
                ServiceNo = serviceNo,
                ChannelUrl = selectedChannel.ChannelUrl ?? string.Empty,
                EpgData = epg.Data,
                AllChannels = allChannels,
                PrevServiceNo = prevServiceNo,
                NextServiceNo = nextServiceNo
            };

            return View(viewModel);
        }

        public static List<CategoryWiseChannel> TestData()
        {
            return new List<CategoryWiseChannel>
            {
                new CategoryWiseChannel
                {
                    Category = "Most Viewed Channels",
                    CategoryLogo = "Logo/Category/MostViewedChannel.png",
                    LanguageChannels = new List<LanguageWiseChannel>
                    {
                        new LanguageWiseChannel
                        {
                            Language = "HINDI",
                            SubscriberChannels = new List<SubscriberChannelsModel>
                            {
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Discovery Channel",
                                    AppDisplayName = "Discovery Channel",
                                    LcnNo = 6001,
                                    ChannelUrl = "https://182.237.14.221/dash/6001/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6001,
                                    IsSubscribed = true,
                                    Category = "INFOTAINMENT",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "National Geographic",
                                    AppDisplayName = "Nat Geo",
                                    LcnNo = 6002,
                                    ChannelUrl = "https://182.237.14.221/dash/6002/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6002,
                                    IsSubscribed = true,
                                    Category = "INFOTAINMENT",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Animal Planet",
                                    AppDisplayName = "Animal Planet",
                                    LcnNo = 6003,
                                    ChannelUrl = "https://182.237.14.221/dash/6003/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6003,
                                    IsSubscribed = false,
                                    Category = "INFOTAINMENT",
                                    Language = "ENGLISH",
                                    SignalType = "SD",
                                    Broadcaster = "FTA"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "History TV18",
                                    AppDisplayName = "History TV18",
                                    LcnNo = 6004,
                                    ChannelUrl = "https://182.237.14.221/dash/6004/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6004,
                                    IsSubscribed = true,
                                    Category = "INFOTAINMENT",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "MTV India",
                                    AppDisplayName = "MTV India",
                                    LcnNo = 6005,
                                    ChannelUrl = "https://182.237.14.221/dash/6005/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6005,
                                    IsSubscribed = true,
                                    Category = "MUSIC",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "9XM",
                                    AppDisplayName = "9XM",
                                    LcnNo = 6006,
                                    ChannelUrl = "https://182.237.14.221/dash/6006/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6006,
                                    IsSubscribed = false,
                                    Category = "MUSIC",
                                    Language = "HINDI",
                                    SignalType = "SD",
                                    Broadcaster = "FTA"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "VH1",
                                    AppDisplayName = "VH1",
                                    LcnNo = 6007,
                                    ChannelUrl = "https://182.237.14.221/dash/6007/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6007,
                                    IsSubscribed = true,
                                    Category = "MUSIC",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Sony SAB",
                                    AppDisplayName = "Sony SAB",
                                    LcnNo = 6008,
                                    ChannelUrl = "https://182.237.14.221/dash/6008/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6008,
                                    IsSubscribed = true,
                                    Category = "ENTERTAINMENT",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Colors TV",
                                    AppDisplayName = "Colors",
                                    LcnNo = 6009,
                                    ChannelUrl = "https://182.237.14.221/dash/6009/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6009,
                                    IsSubscribed = true,
                                    Category = "ENTERTAINMENT",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Star Plus",
                                    AppDisplayName = "Star Plus",
                                    LcnNo = 6010,
                                    ChannelUrl = "https://182.237.14.221/dash/6010/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 6010,
                                    IsSubscribed = true,
                                    Category = "ENTERTAINMENT",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "DD National",
                                    AppDisplayName = "DD National",
                                    LcnNo = 1001,
                                    ChannelUrl = "https://182.237.14.221/dash/1001/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 1001,
                                    IsSubscribed = true,
                                    Category = "Most Viewed Channels",
                                    Language = "HINDI",
                                    SignalType = "SD",
                                    Broadcaster = "FTA"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Aaj Tak",
                                    AppDisplayName = "Aaj Tak",
                                    LcnNo = 1002,
                                    ChannelUrl = "https://182.237.14.221/dash/1002/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 1002,
                                    IsSubscribed = true,
                                    Category = "Most Viewed Channels",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Zee News",
                                    AppDisplayName = "Zee News",
                                    LcnNo = 1003,
                                    ChannelUrl = "https://182.237.14.221/dash/1003/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 1003,
                                    IsSubscribed = true,
                                    Category = "Most Viewed Channels",
                                    Language = "HINDI",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "India TV",
                                    AppDisplayName = "India TV",
                                    LcnNo = 1004,
                                    ChannelUrl = "https://182.237.14.221/dash/1004/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 1004,
                                    IsSubscribed = false,
                                    Category = "Most Viewed Channels",
                                    Language = "HINDI",
                                    SignalType = "SD",
                                    Broadcaster = "FTA"
                                }
                            }
                        }
                    }
                },

                new CategoryWiseChannel
                {
                    Category = "KIDS",
                    CategoryLogo = null,
                    LanguageChannels = new List<LanguageWiseChannel>
                    {
                        new LanguageWiseChannel
                        {
                            Language = "ENGLISH",
                            SubscriberChannels = new List<SubscriberChannelsModel>
                            {
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Cartoon Network",
                                    AppDisplayName = "Cartoon Network",
                                    LcnNo = 2001,
                                    ChannelUrl = "https://182.237.14.221/dash/2001/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 2001,
                                    IsSubscribed = true,
                                    Category = "KIDS",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Pogo",
                                    AppDisplayName = "Pogo",
                                    LcnNo = 2002,
                                    ChannelUrl = "https://182.237.14.221/dash/2002/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 2002,
                                    IsSubscribed = false,
                                    Category = "KIDS",
                                    Language = "ENGLISH",
                                    SignalType = "SD",
                                    Broadcaster = "FTA"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Nickelodeon",
                                    AppDisplayName = "Nickelodeon",
                                    LcnNo = 2003,
                                    ChannelUrl = "https://182.237.14.221/dash/2003/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 2003,
                                    IsSubscribed = true,
                                    Category = "KIDS",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                },
                                new SubscriberChannelsModel
                                {
                                    ChannelName = "Disney Channel",
                                    AppDisplayName = "Disney Channel",
                                    LcnNo = 2004,
                                    ChannelUrl = "https://182.237.14.221/dash/2004/manifest.mpd",
                                    IsFpEnable = true,
                                    IsEncrypt = true,
                                    ServiceID = 2004,
                                    IsSubscribed = true,
                                    Category = "KIDS",
                                    Language = "ENGLISH",
                                    SignalType = "HD",
                                    Broadcaster = "Paid"
                                }
                            }
                        }
                    }
                },

                new CategoryWiseChannel
                {
                    Category = "SPORTS",
                    CategoryLogo = null,
                    LanguageChannels = new List<LanguageWiseChannel>
                    {
                        new LanguageWiseChannel
                        {
                            Language = "ENGLISH",
                            SubscriberChannels = new List<SubscriberChannelsModel>
                            {
                                new SubscriberChannelsModel { ChannelName = "Star Sports 1", LcnNo = 3001, ChannelUrl = "https://182.237.14.221/dash/3001/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 3001, IsSubscribed = true, Category = "SPORTS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "Sony Sports Ten 1", LcnNo = 3002, ChannelUrl = "https://182.237.14.221/dash/3002/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 3002, IsSubscribed = true, Category = "SPORTS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "DD Sports", LcnNo = 3003, ChannelUrl = "https://182.237.14.221/dash/3003/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 3003, IsSubscribed = false, Category = "SPORTS", Language = "ENGLISH", SignalType = "SD", Broadcaster = "FTA" },
                                new SubscriberChannelsModel { ChannelName = "Eurosport", LcnNo = 3004, ChannelUrl = "https://182.237.14.221/dash/3004/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 3004, IsSubscribed = true, Category = "SPORTS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" }
                            }
                        }
                    }
                },

                new CategoryWiseChannel
                {
                    Category = "MOVIES",
                    CategoryLogo = null,
                    LanguageChannels = new List<LanguageWiseChannel>
                    {
                        new LanguageWiseChannel
                        {
                            Language = "HINDI",
                            SubscriberChannels = new List<SubscriberChannelsModel>
                            {
                                new SubscriberChannelsModel { ChannelName = "Zee Cinema", LcnNo = 4001, ChannelUrl = "https://182.237.14.221/dash/4001/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 4001, IsSubscribed = true, Category = "MOVIES", Language = "HINDI", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "Sony Max", LcnNo = 4002, ChannelUrl = "https://182.237.14.221/dash/4002/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 4002, IsSubscribed = true, Category = "MOVIES", Language = "HINDI", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "Star Gold", LcnNo = 4003, ChannelUrl = "https://182.237.14.221/dash/4003/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 4003, IsSubscribed = false, Category = "MOVIES", Language = "HINDI", SignalType = "SD", Broadcaster = "FTA" },
                                new SubscriberChannelsModel { ChannelName = "Colors Cineplex", LcnNo = 4004, ChannelUrl = "https://182.237.14.221/dash/4004/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 4004, IsSubscribed = true, Category = "MOVIES", Language = "HINDI", SignalType = "HD", Broadcaster = "Paid" }
                            }
                        }
                    }
                },

                new CategoryWiseChannel
                {
                    Category = "NEWS",
                    CategoryLogo = null,
                    LanguageChannels = new List<LanguageWiseChannel>
                    {
                        new LanguageWiseChannel
                        {
                            Language = "ENGLISH",
                            SubscriberChannels = new List<SubscriberChannelsModel>
                            {
                                new SubscriberChannelsModel { ChannelName = "NDTV 24x7", LcnNo = 5001, ChannelUrl = "https://182.237.14.221/dash/5001/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 5001, IsSubscribed = true, Category = "NEWS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "Times Now", LcnNo = 5002, ChannelUrl = "https://182.237.14.221/dash/5002/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 5002, IsSubscribed = true, Category = "NEWS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" },
                                new SubscriberChannelsModel { ChannelName = "CNN News18", LcnNo = 5003, ChannelUrl = "https://182.237.14.221/dash/5003/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 5003, IsSubscribed = false, Category = "NEWS", Language = "ENGLISH", SignalType = "SD", Broadcaster = "FTA" },
                                new SubscriberChannelsModel { ChannelName = "Republic TV", LcnNo = 5004, ChannelUrl = "https://182.237.14.221/dash/5004/manifest.mpd", IsFpEnable = true, IsEncrypt = true, ServiceID = 5004, IsSubscribed = true, Category = "NEWS", Language = "ENGLISH", SignalType = "HD", Broadcaster = "Paid" }
                            }
                        }
                    }
                }
            };
        }
    }
}
