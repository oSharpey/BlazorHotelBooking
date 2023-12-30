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



        //Bookking for Hotels


        [HttpGet("hotel/userbooking")]
        public async Task<ActionResult<List<HotelBookingViewModel>>> GetAllBookingsWithUserId(string userId)
        {
            var query = from hotel in _context.Hotels
                        join booking in _context.HotelBookings on hotel.Id equals booking.HotelId
                        where booking.UserId == userId
                        select new HotelBookingViewModel { 
                            bookingId = booking.Id,
                            hotelName = hotel.Name,
                            RoomType = booking.RoomType,  
                            CheckIn = booking.CheckIn,  
                            CheckOut = booking.CheckOut,
                            NumberOfNights = booking.NumberOfNights,  
                            TotalPrice = booking.TotalPrice,
                            DepositAmountPaid = booking.DepositAmountPaid,
                            BookingDate = booking.BookingDate,
                            paidInfull = booking.PaidInfull,
                            IsCancelled = booking.IsCancelled
                        };
            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpPut("hotel/payment/{id}")]
        public async Task<ActionResult<string>> PayRemainder(string id)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            if (dbHotel.PaidInfull == true)
            {
                return BadRequest("You have already paid in full");
            }

            dbHotel.DepositAmountPaid = dbHotel.TotalPrice;
            dbHotel.PaidInfull = true;

            _context.HotelBookings.Update(dbHotel);
            await _context.SaveChangesAsync();
            
            return Ok("Payment Successful");
        }


        [HttpPut("hotel/{id}")]
        public async Task<ActionResult<string>> UpdateHotel(string id, HotelBooking hotl)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            dbHotel.Id = hotl.Id;
            dbHotel.HotelId = hotl.HotelId;
            dbHotel.RoomType = hotl.RoomType;
            dbHotel.CheckIn = hotl.CheckIn;
            dbHotel.CheckOut = hotl.CheckOut;
            dbHotel.NumberOfNights = hotl.NumberOfNights;
            dbHotel.TotalPrice = hotl.TotalPrice;
            dbHotel.DepositAmountPaid = hotl.DepositAmountPaid;
            dbHotel.BookingDate = hotl.BookingDate;
            dbHotel.UserId = hotl.UserId;

            _context.HotelBookings.Update(dbHotel);
            await _context.SaveChangesAsync();

            return Ok("Booking Updated Successfuly");
        }


        [HttpGet("hotel/{id}")]
        public async Task<ActionResult<HotelBooking>> GetHotelBookingById(string id)
        {
            var dbhotel = await _context.HotelBookings.FindAsync(id);

            if (dbhotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            return Ok(dbhotel);
        }



        //check if bookings overlap
        [HttpGet("hotel/overlap")]
        public ActionResult<int> CheckIfBookingOverlaps(DateTime checkIn, DateTime checkOut, int hotelId, string roomType)
        {
            var overlap =  _context.HotelBookings.Where(x => 
                x.CheckIn <= checkOut && 
                x.CheckOut >= checkIn && 
                x.RoomType == roomType &&
                x.HotelId == hotelId
            ).ToList().Count();

            return Ok(overlap);
        }

        [HttpDelete("hotel/{id}")]
        public async Task<ActionResult<List<Hotel>>> DeleteHotelBooking(string id)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);
            var today = DateTime.Now;
            var datediff = dbHotel.CheckIn - today;

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            if (datediff.Days < 5)
            {
                return BadRequest("You cannot cancel this booking");
            }
           
            _context.HotelBookings.Remove(dbHotel);
            await _context.SaveChangesAsync();

            return Ok("Booking Deleted");
        }

        [HttpPost("hotel/book")]
        public async Task<ActionResult<List<HotelBooking>>> AddBooking(HotelBooking hotl)
        {
            _context.HotelBookings.Add(hotl);
            await _context.SaveChangesAsync();

            return Ok("Booking Successful");
        }
    }
}
