using System.Text;
using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.MessageCategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HotelProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IHttpClientFactory httpClientFactory, ILogger<ContactController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactViewModel();
            viewModel.MessageCategories = await GetMessageCategories();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<PartialViewResult> SendMessage()
        {
            var viewModel = new ContactViewModel();
            viewModel.MessageCategories = await GetMessageCategories();
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactViewModel model)
        {
            try
            {
                _logger.LogInformation("SendMessage POST başlatıldı");

                // Form verilerini logla
                _logger.LogInformation($"Form Verileri - Name: {model.Name}, Mail: {model.Mail}, Subject: {model.Subject}, MessageCategoryID: {model.MessageCategoryID}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState geçersiz");
                    foreach (var modelError in ModelState)
                    {
                        _logger.LogWarning($"ModelState Error - Key: {modelError.Key}, Errors: {string.Join(", ", modelError.Value.Errors.Select(e => e.ErrorMessage))}");
                    }

                    model.MessageCategories = await GetMessageCategories();
                    return View("Index", model);
                }

                // ContactViewModel'den CreateContactRequestDto'ya dönüştür
                var createContactRequestDto = new CreateContactRequestDto
                {
                    Name = model.Name,
                    Mail = model.Mail,
                    Subject = model.Subject,
                    Message = model.Message,
                    MessageCategoryID = model.MessageCategoryID,
                    Date = DateTime.Now
                };

                _logger.LogInformation($"RequestDTO oluşturuldu: {JsonConvert.SerializeObject(createContactRequestDto)}");

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createContactRequestDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                _logger.LogInformation($"API'ye gönderilecek JSON: {jsonData}");

                var response = await client.PostAsync("http://localhost:5297/api/Contact", stringContent);

                _logger.LogInformation($"API Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Mesaj başarıyla gönderildi");
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"API Hatası - Status: {response.StatusCode}, Content: {error}");

                    model.MessageCategories = await GetMessageCategories();
                    ViewBag.ErrorMessage = $"API Hatası: {response.StatusCode} - {error}";

                    return View("Index", model);
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP Exception: {httpEx.Message}");
                model.MessageCategories = await GetMessageCategories();
                ViewBag.ErrorMessage = $"Bağlantı hatası: {httpEx.Message}";
                return View("Index", model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Genel Exception: {ex.Message} - StackTrace: {ex.StackTrace}");
                model.MessageCategories = await GetMessageCategories();
                ViewBag.ErrorMessage = $"Genel hata: {ex.Message}";
                return View("Index", model);
            }
        }

        private async Task<List<SelectListItem>> GetMessageCategories()
        {
            try
            {
                _logger.LogInformation("MessageCategories yükleniyor...");

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("http://localhost:5297/api/MessageCategory");

                _logger.LogInformation($"MessageCategory API Response: {responseMessage.StatusCode}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    _logger.LogInformation($"MessageCategory JSON: {jsonData}");

                    var values = JsonConvert.DeserializeObject<List<ResultMessageCategoryDto>>(jsonData);

                    var selectList = values?.Select(x => new SelectListItem
                    {
                        Text = x.MessageCategoryName,
                        Value = x.MessageCategoryID.ToString()
                    }).ToList() ?? new List<SelectListItem>();

                    _logger.LogInformation($"MessageCategories sayısı: {selectList.Count}");

                    return selectList;
                }

                _logger.LogWarning("MessageCategory API başarısız response");
                return new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetMessageCategories Exception: {ex.Message}");
                return new List<SelectListItem>();
            }
        }

        // Test endpoint'i güncelle
        [HttpGet]
        public async Task<IActionResult> TestAPI()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // GET test
                var getResponse = await client.GetAsync("http://localhost:5297/api/Contact");
                ViewBag.GetStatus = getResponse.StatusCode;
                ViewBag.GetContent = await getResponse.Content.ReadAsStringAsync();

                // POST test - CreateContactRequestDto kullan
                var testRequestDto = new CreateContactRequestDto
                {
                    Name = "Test User",
                    Mail = "test@test.com",
                    Subject = "Test Subject",
                    Message = "Test Message",
                    MessageCategoryID = 1,
                    Date = DateTime.Now
                };

                var jsonData = JsonConvert.SerializeObject(testRequestDto);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var postResponse = await client.PostAsync("http://localhost:5297/api/Contact", stringContent);

                ViewBag.PostStatus = postResponse.StatusCode;
                ViewBag.PostContent = await postResponse.Content.ReadAsStringAsync();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}