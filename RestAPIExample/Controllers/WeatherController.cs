using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestAPIExample.Models;

namespace RestAPIExample.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string key = "ed7e4ffd1577e625d9215150e74d5298";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=istanbul&appid=" + key
                + "&units=metric")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                WeatherInfo _weather = JsonConvert.DeserializeObject<WeatherInfo>(body);
                return View(_weather);
            }
        }
    }
}
