using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace RapidAPIWeb.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://open-weather13.p.rapidapi.com/city?lang=TR&city={city}"),
                Headers =
                        {
                            { "x-rapidapi-key", "1e80e4991amsh29d0e12604c6659p1f02f9jsn76372245dea2" },
                            { "x-rapidapi-host", "open-weather13.p.rapidapi.com" },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var result = System.Text.Json.JsonSerializer.Deserialize<Models.WeatherViewModel>(body);
                return View(result);

            }

        }
    }
}
