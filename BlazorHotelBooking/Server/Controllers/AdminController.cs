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
    [Authorize(Roles = "Admin")]

    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet("hotel")]
        public async Task<ActionResult<List<Hotel>>> GetAllHotels()
        {
            var list = await _context.Hotels.ToListAsync();

            return Ok(list);
        }
        
        
        [HttpGet("hotel/{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var dbhotel = await _context.Hotels.FindAsync(id);

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
            var dbHotel = await _context.Hotels.FindAsync(id);

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
            var dbHotel = await _context.Hotels.FindAsync(id);

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            _context.Hotels.Remove(dbHotel);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }
    }
}

