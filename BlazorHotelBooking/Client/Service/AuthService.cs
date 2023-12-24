using Blazored.LocalStorage;
using BlazorHotelBooking.Client.Auth;
using BlazorHotelBooking.Shared.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorHotelBooking.Client.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/register", registerModel);
            if (!result.IsSuccessStatusCode)
                return new RegisterResult { Successful = false, Errors = new List<string> { "Error occured" } };
            return new RegisterResult { Successful = true, Errors = new List<string> { "Account Created successfully" } };
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var JsonLogin = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/login",
                new StringContent(JsonLogin, Encoding.UTF8, "application/json"));

            var loginResult = JsonSerializer.Deserialize<LoginResult>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            if (!response.IsSuccessStatusCode) return loginResult!;

            await _localStorage.SetItemAsync("authToken", loginResult!.Token);
            ((APIAuthStateProvider) _authStateProvider).MarkUserAsAuthenticated(loginResult.Token!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((APIAuthStateProvider) _authStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
