using System.Text;
using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.WebUI.Dtos.SubscribeDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult AddBooking()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Status = "Onay Bekliyor";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5297/api/Booking", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Booking");
            }

            // Hata varsa kullanıcıya bilgi ver
            var error = await response.Content.ReadAsStringAsync();
            ViewBag.Error = $"Rezervasyon sırasında hata oluştu: {response.StatusCode} - {error}";
            return View("Index");
        }
    }
}

