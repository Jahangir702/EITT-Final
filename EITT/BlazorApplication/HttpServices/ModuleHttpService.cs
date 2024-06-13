using Domain.Entities;
using System.Net.Http.Json;

namespace BlazorApp.HttpServices
{
    public class ModuleHttpService
    {
        private readonly HttpClient httpClient;

        public ModuleHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Module>> GetModulesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Module>>("eitt-api/modules");
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Project>>("eitt-api/projects");
        }

        public async Task<Module> GetModuleByIdAsync(int key)
        {
            return await httpClient.GetFromJsonAsync<Module>($"eitt-api/module/key/{key}");
        }

        public async Task<Module> CreateModuleAsync(Module module)
        {
            var response = await httpClient.PostAsJsonAsync("eitt-api/module", module);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Module>();
        }

        public async Task<Module> UpdateModuleAsync(Module module)
        {
            var response = await httpClient.PutAsJsonAsync($"eitt-api/module/{module.Oid}", module);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Module>();
        }

        public async Task DeleteModuleAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"eitt-api/module/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}