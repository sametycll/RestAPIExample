using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestAPIExample.Models;
using System.Diagnostics;
using System.Xml;

namespace RestAPIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //JSON ÝLE VERÝ ÇEKME
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://instagram130.p.rapidapi.com/account-info?username=smtycll"),
                Headers =
    {
        { "X-RapidAPI-Key", "7db658643amsh8b1ec5325443a0ep1ddf2djsn5c6d2e6ef889" },
        { "X-RapidAPI-Host", "instagram130.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                InstagramBio iModel = JsonConvert.DeserializeObject<InstagramBio>(body);
                
                return View(iModel);
            }

           // return View();
        }


        public async Task<IActionResult> Index2()
        {
            //JSON ÝLE VERÝ ÇEKME

            string key = "ed7e4ffd1577e625d9215150e74d5298";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=istanbul&appid=" + key
                + "&units=metric")
            };
            using(var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                WeatherInfo _weather = JsonConvert.DeserializeObject<WeatherInfo>(body);
                return View(_weather);
            }
        }




        public IActionResult Privacy()
        {

            //XML ÝLE VERÝ ÇEKME
            string link = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);
            string USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            ViewBag.usd = USD;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
