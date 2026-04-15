using IPTV_Website.Models;
using IPTV_Website.Services;
using Microsoft.AspNetCore.Mvc;
using IPTV_Website.Models.ApiModels;
using System.Diagnostics;

namespace IPTV_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMockTvDataService _tvDataService;

        public HomeController(ILogger<HomeController> logger, IMockTvDataService tvDataService)
        {
            _logger = logger;
            _tvDataService = tvDataService;
        }
        public IActionResult Index(string data)
        {
            //if (data == null)
            //     return RedirectToAction("Error");

            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
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

        public IActionResult SubscriberDetails()
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
    }
}
