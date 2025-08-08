using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;
using static RapidApiConsume.Models.BookingApiLocationSearchViewModel;

namespace RapidApiConsume.Controllers
{
    public class SearchLocationIDController : Controller
    {
        public async Task<IActionResult> Index(string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                List<LocationItem> model = new List<LocationItem>();
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com18.p.rapidapi.com/stays/auto-complete?query={cityName}"),
                    Headers =
                {
                    { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
                    { "x-rapidapi-host", "booking-com18.p.rapidapi.com" },
                },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<BookingApiResponse>(body);
                    model = apiResponse?.data ?? new List<LocationItem>();

                    return View(model.Take(1).ToList());
                }
            }
            else
            {
                List<LocationItem> model = new List<LocationItem>();
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com18.p.rapidapi.com/stays/auto-complete?query=Paris"),
                    Headers =
                {
                    { "x-rapidapi-key", "eaa8321078msh8aa48935ef5e1d8p1cb46fjsn6493e1885e8e" },
                    { "x-rapidapi-host", "booking-com18.p.rapidapi.com" },
                },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<BookingApiResponse>(body);
                    model = apiResponse?.data ?? new List<LocationItem>();

                    return View(model.Take(1).ToList());
                }
            }

        }
    }
}