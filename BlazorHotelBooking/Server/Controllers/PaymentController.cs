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
    public class PaymentController : ControllerBase
    {

        private readonly DataContext _context;

        public PaymentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Payments>>> GetAllPayments()
        {
            var list = await _context.Payments.OrderByDescending(x => x.PaymentDate).ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Payments>>> GetAllPaymentsByUserId(string id)
        {
            var list = await _context.Payments.Where(x => x.UserId == id).OrderByDescending(x => x.PaymentDate).ToListAsync();

            return Ok(list);
        }
    }
}
