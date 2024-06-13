using Domain.Dto;
using Domain.Entities;
using System.Net.Http.Json;

namespace BlazorApp.HttpServices
{
    public class UserAccountHttpService
    {
        private readonly HttpClient httpClient;
        public UserAccountHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<UserAccount>> GetUserAccountsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<UserAccount>>("eitt-api/user-accounts");
        }

        public async Task<UserAccount> GetUserAccountByIdAync(Guid key)
        {
            return await httpClient.GetFromJsonAsync<UserAccount>($"eitt-api/user-account/key/{key}");
        }

        public async Task<UserAccount> CreateUserAccountAsync(UserAccount userAccount)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/user-account", userAccount);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccount>();
        }

        public async Task<UserAccount> UpdateUserAccountAsync(UserAccount userAccount)
        {
            var response = await httpClient.PutAsJsonAsync($"eitt-api/user-account/{userAccount.Oid}", userAccount);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserAccount>();
        }

        public async Task DeleteUserAccountAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"eitt-api/user-account/{key}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/user-account/login", loginDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}