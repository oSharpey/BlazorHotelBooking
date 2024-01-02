using BlazorHotelBooking.Server.Data;
using BlazorHotelBooking.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }


        // Admin for Hotels

        [HttpGet("hotel")]
        public async Task<ActionResult<List<Hotel>>> GetAllHotels()
        {
            List<Hotel> list = await _context.Hotels.ToListAsync();

            return Ok(list);
        }


        [HttpGet("hotel/{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            Hotel? dbhotel = await _context.Hotels.FindAsync(id);

            if (dbhotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            return Ok(dbhotel);
        }


        [HttpPost("hotel")]
        public async Task<ActionResult<List<Hotel>>> AddHotel(Hotel hotl)
        {
            _context.Hotels.Add(hotl);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }


        [HttpPut("hotel/{id}")]
        public async Task<ActionResult<List<Hotel>>> UpdateHotel(int id, Hotel hotl)
        {
            Hotel? dbHotel = await _context.Hotels.FindAsync(id);

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            dbHotel.Id = hotl.Id;
            dbHotel.Name = hotl.Name;
            dbHotel.SBPrice = hotl.SBPrice;
            dbHotel.DBPrice = hotl.DBPrice;
            dbHotel.FamPrice = hotl.FamPrice;
            dbHotel.Description = hotl.Description;

            _context.Hotels.Update(dbHotel);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }


        [HttpDelete("hotel/{id}")]
        public async Task<ActionResult<List<Hotel>>> DeleteHotel(int id)
        {
            Hotel? dbHotel = await _context.Hotels.FindAsync(id);

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            _context.Hotels.Remove(dbHotel);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }



        //Admin For tours
        [HttpGet("tour")]
        public async Task<ActionResult<List<Tour>>> GetAllTours()
        {
            List<Tour> list = await _context.Tours.ToListAsync();

            return Ok(list);
        }

        [HttpGet("tour/{id}")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
            Tour? dbtour = await _context.Tours.FindAsync(id);

            if (dbtour == null)
            {
                return NotFound("This tour does not exist");
            }

            return Ok(dbtour);
        }


        [HttpPost("tour")]
        public async Task<ActionResult<List<Tour>>> AddTour(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            return await GetAllTours();
        }


        [HttpPut("tour/{id}")]
        public async Task<ActionResult<List<Tour>>> UpdateTour(int id, Tour tour)
        {
            Tour? dbtour = await _context.Tours.FindAsync(id);

            if (dbtour == null)
            {
                return NotFound("This tour does not exist");
            }

            dbtour.Id = tour.Id;
            dbtour.Name = tour.Name;
            dbtour.Cost = tour.Cost;
            dbtour.MaxNumberOfGuests = tour.MaxNumberOfGuests;
            dbtour.DurationInDays = tour.DurationInDays;
            dbtour.Description = tour.Description;

            _context.Tours.Update(dbtour);
            await _context.SaveChangesAsync();

            return await GetAllTours();
        }


        [HttpDelete("tour/{id}")]
        public async Task<ActionResult<List<Tour>>> DeleteTour(int id)
        {
            Tour? dbtour = await _context.Tours.FindAsync(id);

            if (dbtour == null)
            {
                return NotFound("This tour does not exist");
            }

            _context.Tours.Remove(dbtour);
            await _context.SaveChangesAsync();

            return await GetAllTours();
        }

        // create 3 methods to get all bookings for each type of booking

        [HttpGet("hotelbooking")]
        public async Task<ActionResult<List<HotelBooking>>> GetAllHotelBookings()
        {
            List<HotelBooking> list = await _context.HotelBookings.ToListAsync();

            return Ok(list);
        }
        [HttpGet("tourbooking")]
        public async Task<ActionResult<List<TourBooking>>> GetAllTourBookings()
        {
            List<TourBooking> list = await _context.TourBookings.ToListAsync();

            return Ok(list);
        }
        [HttpGet("packagebooking")]
        public async Task<ActionResult<List<PackageBooking>>> GetAllPackageBookings()
        {
            List<PackageBooking> list = await _context.PackageBookings.ToListAsync();

            return Ok(list);
        }
    }
}

