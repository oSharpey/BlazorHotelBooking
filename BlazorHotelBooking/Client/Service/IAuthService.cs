using BlazorHotelBooking.Shared.Models;

namespace BlazorHotelBooking.Client.Service
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();

    }
}
