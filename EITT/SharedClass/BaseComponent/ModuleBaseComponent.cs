using Blazored.Toast.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Json;

namespace SharedClass.BaseComponent
{
    public class ModuleBaseComponent : ComponentBase
    {
        [Inject]
        BaseApi baseApi { get; set; }
        public string searchQuery = "";
        public List<Module> modules = new List<Module>();
        public List<Module> filteredmodules = new List<Module>();
        public List<Project> projects = new List<Project>();
        public Module module = new Module();

        public int currentPage = 1;
        public int itemsPerPage = 5; // Set the number of items per page
        public int totalPages => (int)Math.Ceiling((double)filteredmodules.Count / itemsPerPage);
        public List<IssueLog> issueLogs { get; set; }

        public bool showSuccessAlert = false;
        public bool showValidationErrors = false;

        public Module ModuleToDelete { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }
        //New implement here
        public string newModuleDescription = "";
        public int selectedProjectId = 0;
        public bool isEditing = false;

        public Module selectedModule;
        public string EditModeTitle => isEditing ? "Edit Module" : "Add New";
        public string SubmitButtonLabel => isEditing ? "Save Changes" : "Create Module";

        protected override async Task OnInitializedAsync()
        {
            await LoadModuleAsync();

            await LoadProjectAsync();

            await LoadIssueLogAsync();
        }

        public async Task LoadModuleAsync()
        {
            try
            {
                modules = await baseApi.GetDataAsync<Module>("eitt-api/modules");
                FilterModules();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modules: {ex.Message}");
            }
        }

        public async Task CreateOrUpdateModule()
        {
            showValidationErrors = false;

            if (selectedProjectId == 0 || string.IsNullOrWhiteSpace(newModuleDescription))
            {
                showValidationErrors = true;
                StateHasChanged();
                return;
            }

            try
            {
                if (isEditing && selectedModule != null)
                {
                    var existingModule = modules.FirstOrDefault(m => m.Oid != selectedModule.Oid && m.ProjectId == selectedProjectId && m.Description == newModuleDescription);
                    if (existingModule != null)
                    {
                        toastService.ShowError("A module with the same name already exists in the selected project.");
                        return;
                    }

                    // Update existing module
                    selectedModule.Description = newModuleDescription;
                    selectedModule.ProjectId = selectedProjectId;

                    var response = await baseApi.PutDataAsync($"eitt-api/module/{selectedModule.Oid}", selectedModule);
                    if (response != null)
                    {
                        showSuccessAlert = true;
                        toastService.ShowInfo("Module updated successfully!");
                        ClearFormAndEditingState();
                        await LoadModuleAsync();
                        FilterModules();
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        var errorMessage = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                        toastService.ShowError(errorMessage["message"]);
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    }
                }
                else
                {
                    // Create new module
                    var newModule = new Module()
                    {
                        Description = newModuleDescription,
                        ProjectId = selectedProjectId,
                    };

                    var existingModule = modules.FirstOrDefault(m => m.ProjectId == selectedProjectId && m.Description == newModuleDescription);
                    if (existingModule != null)
                    {
                        toastService.ShowError("A module with the same name already exists in the selected project.");
                        return;
                    }

                    var response = await baseApi.PostDataAsync<Module>("eitt-api/module", newModule);

                    if (response.IsSuccessStatusCode)
                    {
                        showSuccessAlert = true;
                        toastService.ShowSuccess("Module created successfully!");
                        ClearFormAndEditingState();
                        await LoadModuleAsync();
                        FilterModules();
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        var errorMessage = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                        toastService.ShowError(errorMessage["message"]);
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating/updating module: {ex.Message}");
            }
        }

        public void EditModule(Module module)
        {
            selectedModule = module;
            newModuleDescription = module.Description;
            selectedProjectId = module.ProjectId;
            isEditing = true;
        }

        private void ClearFormAndEditingState()
        {
            selectedModule = null;
            newModuleDescription = "";
            selectedProjectId = 0;
            isEditing = false;
        }

        public string GetProjectName(int projectId)
        {
            var project = projects.FirstOrDefault(p => p.Oid == projectId);
            return project != null ? project.Description : "N/A";
        }

        public async Task CreateModuleAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(module.Description))
                {
                    await baseApi.PostDataAsync<Module>("eitt-api/module", module);

                    showSuccessAlert = true;
                    toastService.ShowSuccess("Module created successfully!");
                    //await ShowAlert("Module created successfully!");
                    await LoadModuleAsync();
                    FilterModules();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modules: {ex.Message}");
            }
        }

        public async Task UpdateModuleAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(module.Description))
                {
                    var response = await baseApi.PutDataAsync($"eitt-api/module/{module.Oid}", module);
                    if (response != null)
                    {
                        showSuccessAlert = true;
                        toastService.ShowInfo("Module updated successfully!");
                        //await ShowAlert("Module updated successfully!");
                        await LoadModuleAsync();
                        FilterModules();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Modules: {ex.Message}");
            }
        }

        public async Task DeleteModuleAsync(Module module)
        {
            try
            {
                await baseApi.DeleteDataAsync($"eitt-api/module/{module.Oid}");

                toastService.ShowWarning("Module deleted successfully!");

                //await ShowAlert("Module deleted successfully!");

                await LoadModuleAsync();
                FilterModules();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Modules: {ex.Message}");
            }
        }

        public async Task OpenEditModal(Module item)
        {
            module = new Module
            {
                Oid = item.Oid,
                Description = item.Description,
                ProjectId = item.ProjectId
            };

            await JSRuntime.InvokeVoidAsync("openEditModal");
        }

        public async Task LoadModuleByKeyAsync(int key)
        {
            try
            {
                modules = await baseApi.GetDataAsync<Module>("eitt-api/module/{key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modules: {ex.Message}");
            }
        }

        public async Task LoadProjectAsync()

        {
            try
            {
                projects = await baseApi.GetDataAsync<Project>("eitt-api/projects");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading projects: {ex.Message}");
            }
        }

        public async Task LoadIssueLogAsync()
        {
            try
            {
                issueLogs = await baseApi.GetDataAsync<IssueLog>("eitt-api/issueLogs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading IssueLogs: {ex.Message}");
            }
        }

        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }

        public void SearchModules()
        {
            //currentPage = 1;
            FilterModules();
        }

        public void ClearSearch()
        {
            searchQuery = "";
            searchQuery = "";
            //currentPage = 1;
            FilterModules();
        }

        public void FilterModules()
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredmodules = modules
                    .Where(i => i.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                filteredmodules = new List<Module>(modules);
            }
            StateHasChanged();
        }

        public List<Module> pagedModules => filteredmodules
           .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();

        public bool HasPreviousPage => currentPage > 1;
        public bool HasNextPage => currentPage < totalPages;

        public void NextPage()
        {
            if (HasNextPage)
            {
                currentPage++;
            }
        }

        public void PreviousPage()
        {
            if (HasPreviousPage)
            {
                currentPage--;
            }
        }
    }
}