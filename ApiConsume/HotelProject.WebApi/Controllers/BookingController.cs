﻿using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddBooking(Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Booking verileri eksik");
            }

            try
            {
                _bookingService.TInsert(booking);
                return Ok(new { success = true, message = "Rezervasyon başarıyla kaydedildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Rezervasyon kaydedilemedi", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var values = _bookingService.TGetByID(id);
            _bookingService.TDelete(values);
            return Ok();
        }
        [HttpPut("UpdateBooking")]
        public IActionResult UpdateBooking(Booking booking)
        {
            _bookingService.TUpdate(booking);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var values = _bookingService.TGetByID(id);
            return Ok(values);
        }
        [HttpPut("aaaa")]
        public IActionResult aaaa(Booking booking)
        {
            _bookingService.TBookingStatusChangeApproved(booking);
            return Ok();
        }
        [HttpPut("bbbb")]
        public IActionResult bbbb(int id)
        {
            _bookingService.TBookingStatusChangeApproved2(id);
            return Ok();
        }
    }
}
