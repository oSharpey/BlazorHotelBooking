using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlazorHotelBooking.Shared;
using BlazorHotelBooking.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


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
            var list = await _context.Tours.ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
            var dbtour = await _context.Tours.FindAsync(id);

            if (dbtour == null)
            {
                return NotFound("This tour does not exist");
            }

            return Ok(dbtour);
        }
    }
}
