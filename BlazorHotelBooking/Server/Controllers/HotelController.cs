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

        [HttpPost]
        public async Task<ActionResult<List<Hotel>>> AddHotel(Hotel hotl)
        {
            _context.Hotels.Add(hotl);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }

        [HttpPut("{id}")]
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
            dbHotel.Spaces = hotl.Spaces;
            dbHotel.Description = hotl.Description;

            _context.Hotels.Add(hotl);
            await _context.SaveChangesAsync();

            return await GetAllHotels();
        }

        [HttpDelete("{id}")]
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
