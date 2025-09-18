using HotelProject.DataAccessLayer.Concrete;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageCategoryController : ControllerBase
    {
        private readonly Context _context;

        public MessageCategoryController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageCategory>>> GetMessageCategories()
        {
            return await _context.MessageCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageCategory>> GetMessageCategory(int id)
        {
            var category = await _context.MessageCategories.FindAsync(id);
            if (category == null)
                return NotFound();
            return category;
        }
    }
}
