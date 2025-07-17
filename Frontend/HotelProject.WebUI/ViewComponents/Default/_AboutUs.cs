using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _AboutUs : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
