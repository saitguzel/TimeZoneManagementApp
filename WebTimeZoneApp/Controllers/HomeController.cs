using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebTimeZoneApp.Models;

namespace WebTimeZoneApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list = TimeZoneInfo.GetSystemTimeZones().ToList();
            ViewBag.localTimeZone = JsonSerializer.Serialize(TimeZoneInfo.Local);
            
            //var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            //return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);

            //Hawaiian Standard Time
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time");
            ViewBag.HawaiianStandardTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime() , userTimeZone);
            //"Turkey Standard Time"

            return View(list);
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
    }
}
