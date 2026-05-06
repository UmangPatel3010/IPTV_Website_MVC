using IPTV_Website.Helpers;
using IPTV_Website.Models;
using IPTV_Website.Models.ApiModels;
using IPTV_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace IPTV_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMockTvDataService _tvDataService;
        private readonly ApiTvDataService _apiTvDataService;

        public HomeController(IMockTvDataService tvDataService, ApiTvDataService apiTvDataService)
        {
            _tvDataService = tvDataService;
            _apiTvDataService = apiTvDataService;
        }
        public IActionResult Index(string data)
        {
            CommonVariables.setDeviceNo(string.IsNullOrWhiteSpace(data) ? "4C05A102A1A2" : data);
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {

            //CategoryWiseChannelCommonResponse categoryWiseChannelModel = await _apiTvDataService.APPChannels();

            //if (categoryWiseChannelModel != null && categoryWiseChannelModel.StatusCode == 200)
            //{
            //    return View(categoryWiseChannelModel.Data);
            //}

            return RedirectToAction("Dashboard_copy");
        }

        public IActionResult Dashboard_copy()
        {
            var channels = _tvDataService.GetAllChannels().ToList();
            var trendingChannels = channels.ToList(); // Example logic for trending channels
            var heroChannel = channels.FirstOrDefault(); // Example logic for hero channel

            var viewModel = new HomeViewModel
            {
                Channels = channels,
                TrendingChannels = trendingChannels,
                HeroChannel = heroChannel
            };

            return View(viewModel);
        }

        public IActionResult Favorites()
        {
            var favoriteChannels = _tvDataService.GetFavoriteChannels().ToList();

            var viewModel = new FavoritesViewModel
            {
                Empty = !favoriteChannels.Any(),
                FavoriteChannels = favoriteChannels
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SubscriberDetailsAsync()
        {
            
            // Example data for demonstration
            var subscriber = new SubscriptionDetailModel
            {
                Name = "John Doe",
                SubNo = "SUB12345",
                Address = "123 Main Street",
                Pincode = 123456,
                Email = "john.doe@example.com",
                Mobile = "9876543210",
                Status = 1,
                DeviceNo = "DEV123",
                SerialNo = "SER456",
                NoOfChannels = 120,
                Subscriptions = new List<SubscriptionList>
                {
                    new SubscriptionList{
                        Name = "Premium Plan",
                        ServiceID = 101,
                        Type = "Monthly",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(1)
                    }
                }
            };


            SubscriptionDetailModelCommonResponse subscriptionDetailModel = await _apiTvDataService.APPSubscribed();

            if (subscriptionDetailModel != null && subscriptionDetailModel.StatusCode == 200)
            {
                return View(subscriptionDetailModel.Data.FirstOrDefault());
            }
            return View(subscriber);
        }

        public IActionResult Search(string query)
        {
            var popularTerms = new List<string> { "Sports", "News", "Movies", "Entertainment", "Music" };
            var results = string.IsNullOrWhiteSpace(query) ? new List<Channel>() : _tvDataService.SearchChannels(query);

            var viewModel = new SearchViewModel
            {
                Query = query,
                PopularTerms = popularTerms,
                Results = results
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Player(int? channelId)
        {
            var channel = _tvDataService.GetChannel(channelId);
            var allChannels = _tvDataService.GetAllChannels().ToList();

            var currentIndex = allChannels.ToList().FindIndex(c => c.Id == channel.Id);
            var nextChannelId = currentIndex < allChannels.Count - 1 ? allChannels[currentIndex + 1].Id : allChannels[0].Id;
            var prevChannelId = currentIndex > 0 ? allChannels[currentIndex - 1].Id : allChannels[allChannels.Count - 1].Id;

            var viewModel = new PlayerViewModel
            {
                Channel = channel,
                AllChannels = allChannels,
                NextChannelId = nextChannelId,
                PrevChannelId = prevChannelId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> License()
        {
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();
            var content = await _apiTvDataService.License(requestBody);

            return Content(content, "application/json");
        }
        public async Task<IActionResult> ChannelPlay(long serviceNo, string channelURL)
        {
            var now = DateTime.Now;
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
                        DisplayName = "Sample Channel",
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

            var viewModel = new ChannelPlayViewModel
            {
                ServiceNo = serviceNo,
                ChannelUrl = channelURL,
            };
            EpgDataCommonResponse epgDataCommonResponse = await _apiTvDataService.EPGGet(serviceNo);

            if (epgDataCommonResponse != null && epgDataCommonResponse.StatusCode == 200)
            {
                viewModel.EpgData = epgDataCommonResponse.Data;
            }
            else
            {
                viewModel.EpgData = epg.Data;

            }


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ViewLog([FromBody] ViewLogModel model)
        {
            var response = await _apiTvDataService.APPViewLogs(model);
            if (response != null)
            {
                return Json(response);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> HandleSignal(string eventKey)
        {
            // map only known keys (whitelist)
            switch (eventKey)
            {
                default:
                    return RedirectToAction("Dashboard");
            }
        }
    }
}
