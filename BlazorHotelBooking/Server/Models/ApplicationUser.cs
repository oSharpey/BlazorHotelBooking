using Microsoft.AspNetCore.Identity;

namespace BlazorHotelBooking.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PassportNumber { get; set; }
    }
}
