using Domain.Entities;
using System.Net.Http.Json;

namespace BlazorApp.HttpServices
{
    public class IssueLogHttpService
    {
        private readonly HttpClient httpClient;
        public IssueLogHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<IssueLog>> GetIssueLogsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<IssueLog>>("eitt-api/issueLogs");
        }

        public async Task<List<UserAccount>> GetUserAccountsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<UserAccount>>("eitt-api/user-accounts");
        }

        public async Task<List<Module>> GetModulesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Module>>("eitt-api/modules");
        }

        public async Task<IssueLog> GetIssueLogByIdAync(Guid key)
        {
            return await httpClient.GetFromJsonAsync<IssueLog>($"eitt-api/issueLog/key/{key}");
        }

        public async Task<IssueLog> CreateIssueLogAsync(IssueLog issueLog)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/issueLog", issueLog);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IssueLog>();
        }

        public async Task<IssueLog> UpdateIssueLogAsync(IssueLog issueLog)
        {
            var response = await httpClient.PutAsJsonAsync($"eitt-api/issueLog/{issueLog.Oid}", issueLog);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IssueLog>();
        }

        public async Task DeleteIssueLogAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"eitt-api/issueLog/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}