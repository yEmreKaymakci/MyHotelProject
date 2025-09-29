using HotelProject.BusinessLayer.Abstract;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserWorkLocationController : ControllerBase
    {
        private readonly Context _context;

        public AppUserWorkLocationController(Context context)
        {
            _context = context; // DI ile inject edilmeli
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = _context.Users
                .Include(x => x.WorkLocation)
                .Select(y => new AppUserWorkLocationViewModel
                {
                    Name = y.Name,
                    SurName = y.SurName,
                    WorkLocationID = y.WorkLocationID,
                    WorkLocationName = y.WorkLocation.WorkLocationName,
                    City = y.City,
                    Country = y.Country,
                    Gender = y.Gender,
                    ImageUrl = y.ImageUrl

                }).ToList();

            return Ok(values);
        }
    }
}
