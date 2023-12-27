using BlazorHotelBooking.Server.Data;
using BlazorHotelBooking.Shared;
using BlazorHotelBooking.Shared.Models;
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
        public async Task<ActionResult<List<HotelBookingViewModel>>> GetAllBookingsWithId(string userId)
        {
            var query = from hotel in _context.Hotels
                        join booking in _context.HotelBookings on hotel.Id equals booking.HotelId
                        where booking.UserId == userId
                        select new HotelBookingViewModel { 
                            hotelName = hotel.Name,
                            RoomType = booking.RoomType,  
                            CheckIn = booking.CheckIn,  
                            CheckOut = booking.CheckOut,
                            NumberOfNights = booking.NumberOfNights,  
                            TotalPrice = booking.TotalPrice,
                            DepositAmountPaid = booking.DepositAmountPaid,
                            BookingDate = booking.BookingDate
                        };
            var result = await query.ToListAsync();

            return Ok(result);
        }



        [HttpPost("hotel")]
        public async Task<ActionResult<List<HotelBooking>>> AddBooking(HotelBooking hotl)
        {
            _context.HotelBookings.Add(hotl);
            await _context.SaveChangesAsync();

            return Ok("Booking Successful");
        }
    }
}
