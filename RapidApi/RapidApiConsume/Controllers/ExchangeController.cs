using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;
using System.Net.Http;
using System.Text;

namespace RapidApiConsume.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExchangeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.collectapi.com/economy/exchange"),
                Headers =
                {
                    { "authorization", "apikey seninkeyburayayaz" }
                }
            };

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return View(new List<Exchange_Rates>());

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ExchangeApiResponse>(jsonString);

            return View(result?.data ?? new List<Exchange_Rates>());
        }
    }
}
