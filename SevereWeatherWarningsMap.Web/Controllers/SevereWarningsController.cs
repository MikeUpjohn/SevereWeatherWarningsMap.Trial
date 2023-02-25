using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SevereWeatherWarnings.Map;
using System.Net;
using System.Net.Http.Headers;
using System.Linq;

namespace SevereWeatherWarningsMap.Controllers
{
    public class SevereWarningsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<SevereWeatherWarningList> FloodWarnings()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("confessions-of-a-storm-geek.co.uk", ""));
            HttpResponseMessage response = await client.GetAsync("https://api.weather.gov/alerts/active/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            SevereWeatherWarningList list = JsonConvert.DeserializeObject<SevereWeatherWarningList>(json);
            list.Features = list.Features.Where(x => x.geometry != null).ToList();

            return list;
        }
    }
}
