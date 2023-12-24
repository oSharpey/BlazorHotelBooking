using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlazorHotelBooking.Shared;
using BlazorHotelBooking.Server.Data;
using Microsoft.EntityFrameworkCore;


namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _context;

        public HotelController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetAllHotels()
        {
            var list = await _context.Hotels.ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var dbhotel = await _context.Hotels.FindAsync(id);

            if (dbhotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            return Ok(dbhotel);
        }
    }
}
