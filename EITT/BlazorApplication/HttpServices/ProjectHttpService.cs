using Domain.Entities;
using System.Net.Http.Json;

namespace BlazorApp.HttpServices
{
    public class ProjectHttpService
    {
        private readonly HttpClient httpClient;
        public ProjectHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Project>>("eitt-api/projects");
        }

        public async Task<Project> GetprojectByIdAync(int key)
        {
            return await httpClient.GetFromJsonAsync<Project>($"eitt-api/project/key/{key}");
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/project", project);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Project>();
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            var response = await httpClient.PutAsJsonAsync($"eitt-api/project/{project.Oid}", project);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Project>();
        }

        public async Task DeleteProjectAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"eitt-api/project/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}