using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelProject.WebUI.Dtos.ContactDto
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int MessageCategoryID { get; set; }
        public List<SelectListItem> MessageCategories { get; set; } = new List<SelectListItem>();
    }
}
