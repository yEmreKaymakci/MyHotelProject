using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.AppUserDto;
using HotelProject.WebUI.Dtos.RoomDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
    public class AdminUsersController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:5297/api/AppUser");
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultAppUserDto>>(jsonData);
            //    return View(values);  
            //}
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("http://localhost:5297/api/AppUser/AppUser");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultAppUserListDto>>(jsonData);

                    // Null check ekle
                    return View(values ?? new List<ResultAppUserListDto>());
                }
                else
                {
                    // API çağrısı başarısız - boş liste gönder
                    ViewBag.ErrorMessage = $"API çağrısı başarısız: {responseMessage.StatusCode}";
                    return View(new List<ResultAppUserListDto>());
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda boş liste gönder
                ViewBag.ErrorMessage = $"Hata oluştu: {ex.Message}";
                return View(new List<ResultAppUserListDto>());
            }
        }
    }
}
