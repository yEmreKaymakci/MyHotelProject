using System.Text;
using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.EntityLayer.Concrete; // Entity için gerekli
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
        public async Task<IActionResult> AddBooking([FromForm] CreateBookingDto createBookingDto)
        {
            Console.WriteLine("=== WEBUI DEBUG BAŞLADI ===");
            Console.WriteLine($"Form geldi: {createBookingDto?.Name ?? "NULL"}");
            Console.WriteLine($"Mail: {createBookingDto?.Mail ?? "NULL"}");

            // Boş alanları default değerlerle doldur
            createBookingDto.Status = createBookingDto.Status ?? "Onay Bekliyor";
            createBookingDto.Description = createBookingDto.Description ?? "";
            createBookingDto.SpecialRequest = createBookingDto.SpecialRequest ?? "";

            Console.WriteLine("🔧 Default değerler atandı");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState geçersiz!");

                // Tüm hataları göster
                foreach (var modelError in ModelState)
                {
                    var key = modelError.Key;
                    var errors = modelError.Value.Errors;

                    Console.WriteLine($"🔍 Field: {key}");
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"   ❌ Error: {error.ErrorMessage}");
                        if (error.Exception != null)
                        {
                            Console.WriteLine($"   💥 Exception: {error.Exception.Message}");
                        }
                    }
                }

                // Gelen değerleri de kontrol edelim
                Console.WriteLine("=== FORM DEĞERLERİ ===");
                Console.WriteLine($"Name: '{createBookingDto.Name}'");
                Console.WriteLine($"Mail: '{createBookingDto.Mail}'");
                Console.WriteLine($"Checkin: '{createBookingDto.Checkin}'");
                Console.WriteLine($"Checkout: '{createBookingDto.Checkout}'");
                Console.WriteLine($"AdultCount: '{createBookingDto.AdultCount}'");
                Console.WriteLine($"ChildCount: '{createBookingDto.ChildCount}'");
                Console.WriteLine($"RoomCount: '{createBookingDto.RoomCount}'");

                return View("Index");
            }

            try
            {
                // DTO'yu Entity'ye çevir (API'nin beklediği format)
                var booking = new Booking
                {
                    Name = createBookingDto.Name,
                    Mail = createBookingDto.Mail,
                    // Tarih string'den DateTime'a çevir
                    Checkin = DateTime.TryParse(createBookingDto.Checkin, out var checkinDate) ? checkinDate : DateTime.Now,
                    Checkout = DateTime.TryParse(createBookingDto.Checkout, out var checkoutDate) ? checkoutDate : DateTime.Now.AddDays(1),
                    AdultCount = createBookingDto.AdultCount,
                    ChildCount = createBookingDto.ChildCount,
                    RoomCount = createBookingDto.RoomCount,
                    SpecialRequest = createBookingDto.SpecialRequest ?? "",
                    Description = createBookingDto.Description ?? "",
                    Status = "Onay Bekliyor"
                };

                Console.WriteLine($"Entity oluşturuldu: {booking.Name}");
                Console.WriteLine($"Checkin: {booking.Checkin}");
                Console.WriteLine($"Checkout: {booking.Checkout}");

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(booking);
                Console.WriteLine($"📤 Gönderilen JSON: {jsonData}");

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                Console.WriteLine("🔗 API çağrısı başlıyor...");
                var response = await client.PostAsync("http://localhost:5297/api/Booking", stringContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"📥 API Status: {response.StatusCode}");
                Console.WriteLine($"📥 API Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ API başarılı");
                    TempData["Success"] = "Rezervasyon başarıyla gönderildi!";
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    Console.WriteLine("❌ API başarısız");
                    TempData["Error"] = $"API Hatası: {response.StatusCode} - {responseContent}";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Exception: {ex.Message}");
                Console.WriteLine($"💥 Stack Trace: {ex.StackTrace}");
                TempData["Error"] = $"Bağlantı hatası: {ex.Message}";
                return View("Index");
            }
        }
    }
}