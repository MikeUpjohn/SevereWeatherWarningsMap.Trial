using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SevereWeatherWarnings.Map;
using System.Net;
using System.Net.Http.Headers;
using System.Linq;

namespace SevereWeatherWarningsMap.Controllers {
    public class SevereWarningsController : Controller {
        public IActionResult Index() {
            return View();
        }

        public async Task<SevereWeatherWarningList> FloodWarnings() {
            #region read from url
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("confessions-of-a-storm-geek.co.uk", ""));
            //HttpResponseMessage response = await client.GetAsync("https://api.weather.gov/alerts/active/");
            //response.EnsureSuccessStatusCode();

            //var json = await response.Content.ReadAsStringAsync();

            #endregion

            #region read from file
            var json = "";

            using (var streamReader = new StreamReader("C:\\inetpub\\wwwroot\\SevereWeatherWarningsMap.Trial\\SevereWeatherWarningsMap.Web\\sample-warning.json")) {
                var raw = streamReader.ReadToEnd();
                json = raw;
            }
            #endregion

            SevereWeatherWarningList list = JsonConvert.DeserializeObject<SevereWeatherWarningList>(json);
            var list2 = list.Features.Where(x => x.geometry != null).ToList();
            list2.AddRange(list.Features.Where(x => x.properties.@event == "Tornado Warning").ToList());

            var test = list2.Where(x => x.geometry == null || x.geometry.coordinates == null).ToList();
            list.Features = list2;
            return list;
        }
    }
}
