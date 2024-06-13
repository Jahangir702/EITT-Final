using Domain.Entities;
using System.Net.Http.Json;

namespace BlazorApp.HttpServices
{
    public class SolveSheetHttpService
    {
        private readonly HttpClient httpClient;
        public SolveSheetHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SolveSheet>> GetSolveSheetsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<SolveSheet>>("eitt-api/solvesheets");
        }

        public async Task<List<IssueLog>> GetIssueLogsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<IssueLog>>("eitt-api/issueLogs");
        }

        public async Task<SolveSheet> GetSolveSheetByIdAync(Guid key)
        {
            return await httpClient.GetFromJsonAsync<SolveSheet>($"eitt-api/solvesheet/key/{key}");
        }

        public async Task<SolveSheet> CreateSolveSheetAsync(SolveSheet solveSheet)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/solvesheet", solveSheet);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SolveSheet>();
        }

        public async Task<SolveSheet> UpdateSolveSheetAsync(SolveSheet solveSheet)
        {
            var response = await httpClient.PutAsJsonAsync($"eitt-api/solvesheet/{solveSheet.Oid}", solveSheet);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SolveSheet>();
        }

        public async Task DeleteSolveSheetAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"eitt-api/solvesheet/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}