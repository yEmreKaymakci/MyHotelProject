using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;

namespace RapidApiConsume.Controllers
{
    public class BookingByCityController : Controller
    {
        public async Task<IActionResult> Index(string cityID)
        {
            if (!string.IsNullOrEmpty(cityID))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com18.p.rapidapi.com/stays/search?locationId={Uri.EscapeDataString(cityID)}&checkinDate=2025-08-08&checkoutDate=2025-08-17&units=metric&temperature=c&currencyCode=EUR"),


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
                    var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);
                    return View(values.data.ToList());
                }
            }
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com18.p.rapidapi.com/stays/search?locationId=eyJjaXR5X25hbWUiOiJQYXJpcyIsImNvdW50cnkiOiJGcmFuY2UiLCJkZXN0X2lkIjoiLTE0NTY5MjgiLCJkZXN0X3R5cGUiOiJjaXR5In0%3D&checkinDate=2025-08-08&checkoutDate=2025-08-17&units=metric&temperature=c&currencyCode=EUR"),
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
                    var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);
                    return View(values.data.ToList());
                }
            }
        }
    }
}
