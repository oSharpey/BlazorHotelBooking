using BlazorHotelBooking.Server.Data;
using BlazorHotelBooking.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class BookingsController : ControllerBase
    {

        private readonly DataContext _context;


        public BookingsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("hotel/userbooking")]
        public async Task<ActionResult<List<HotelBooking>>> GetAllBookingsWithId(string userId)
        {

            List<HotelBooking> list = _context.HotelBookings.Where(x => x.UserId == userId).ToList();

            return Ok(list);
        }



        [HttpPost("hotel")]
        public async Task<ActionResult<List<HotelBooking>>> AddBooking(HotelBooking hotl)
        {
            _context.HotelBookings.Add(hotl);
            await _context.SaveChangesAsync();

            return await GetAllBookingsWithId(hotl.UserId);
        }
    }
}
