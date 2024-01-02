using BlazorHotelBooking.Server.Data;
using BlazorHotelBooking.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class TourController : ControllerBase
    {
        private readonly DataContext _context;

        public TourController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tour>>> GetAllTours()
        {
            List<Tour> list = await _context.Tours.ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
            Tour? dbtour = await _context.Tours.FindAsync(id);

            if (dbtour == null)
            {
                return NotFound("This tour does not exist");
            }

            return Ok(dbtour);
        }
    }
}
