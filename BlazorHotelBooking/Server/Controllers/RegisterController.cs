using BlazorHotelBooking.Server.Models;
using BlazorHotelBooking.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PassportNumber = model.PassportNumber,
                PhoneNumber = model.PhoneNumber
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(e => e.Description);
                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, "User");
            //if (user.Email == "admin@localhost")
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //    return Ok(new RegisterResult { Successful = true });
            //}

            return Ok(new RegisterResult { Successful = true });
        }
    }
}
