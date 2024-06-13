using Domain.Dto;
using Domain.Entities;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace SharedClass
{
    public class AuthenticationService
    {
        private bool isAuthenticated = false;
        private string loggedInUser = null;
        private readonly HttpClient httpClient;
        private readonly IJSRuntime jsRuntime;

        BaseApi baseApi { get; set; }
        UserAccount userAccount { get; set; }

        public AuthenticationService(HttpClient httpClient, IJSRuntime jsRuntime, BaseApi baseApi)
        {
            this.httpClient = httpClient;
            this.jsRuntime = jsRuntime;
            this.baseApi = baseApi;
        }

        public async Task<bool> UserLogin(LoginDto loginDto)
        {
            try
            {
                var response = await baseApi.PostDataAsync<LoginDto>("eitt-api/user-account/login", loginDto);

                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    isAuthenticated = true;
                    loggedInUser = loginDto.Username;
                    userAccount = await response.Content.ReadFromJsonAsync<UserAccount>();

                    var userProfileJson = JsonSerializer.Serialize(userAccount);

                    await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", isAuthenticated);
                    await jsRuntime.InvokeVoidAsync("localStorage.setItem", "userName", loggedInUser);
                    await jsRuntime.InvokeVoidAsync("localStorage.setItem", "userAccountProfile", userProfileJson);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                throw;
            }

            return false;
        }


        public async Task Logout()
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userName");
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userAccountProfile");
        }

        public async Task<bool> IsAuthenticated()
        {
            var authToken = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            var userName = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "userName");

            return !string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(userName);
        }

        public async Task<string> GetLoggedInUserName()
        {
            var userName = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "userName");
            return userName;
        }

        public async Task<UserAccount> GetUserProfile()
        {
            // Retrieve the user JSON string from the persistent storage
            var userJson = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "userName");

            // Deserialize the user JSON string back to a User object
            var user = JsonSerializer.Deserialize<UserAccount>(userJson);

            return user;
        }
    }
}