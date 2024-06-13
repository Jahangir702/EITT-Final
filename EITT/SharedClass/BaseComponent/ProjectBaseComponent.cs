using Blazored.Toast.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace SharedClass.BaseComponent
{
    public class ProjectBaseComponent : ComponentBase
    {
        [Inject]
        BaseApi baseApi {  get; set; }

        public string searchQuery = "";
        public List<Project> projects = new List<Project>();
        public List<Project> filteredprojects = new List<Project>();
        public Project project = new Project();
        public int currentPage = 1;
        public int itemsPerPage = 5; // Set the number of items per page
        public int totalPages => (int)Math.Ceiling((double)filteredprojects.Count / itemsPerPage);
        public bool showSuccessAlert = false;
        public Project ProjectToDelete { get; set; }

        public bool AddRow = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadProjectAsync();
        }
        public async Task<bool> IsDuplicateProject(string description)
        {
            return projects.Any(p => p.Description == description);
        }
        public async Task LoadProjectAsync()
        
        {
            try
            {
                projects = await baseApi.GetDataAsync<Project>("eitt-api/projects");
                FilterProjects();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading projects: {ex.Message}");
            }
        }

        public async Task CreateProjectAsync()
        {
            try
            {
                if (await IsDuplicateProject(project.Description))
                {
                    toastService.ShowError("Project already exists. Please enter a different project name.");
                }
                else
                {
                    project.EditMode = true;
                    var response = await baseApi.PostDataAsync<Project>("eitt-api/project", project);
                    if (response != null)
                    {
                        await LoadProjectAsync();
                        toastService.ShowSuccess("Project created successfully!");
                        project = new Project();
                        AddRow = false;
                        FilterProjects();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading projects: {ex.Message}");
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            try
            {
                if (!string.IsNullOrEmpty(project.Description))
                {
                    project.EditMode = true;
                    var response = await baseApi.PutDataAsync($"eitt-api/project/{project.Oid}", project);
                    if (response != null)
                    {
                        showSuccessAlert = true;
                        toastService.ShowInfo("Project updated successfully!");
                        await LoadProjectAsync();
                        FilterProjects();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating projects: {ex.Message}");
            }
        }

        protected async Task ToggleEditMode(Project project)
        {
            project.EditMode = true ;
        }
        protected async Task CancelEdit(Project project)
        {
            project.EditMode = false;
        }
        public async Task DeleteProjectAsync(Project project)
        {
            try
            {
                var response = await baseApi.DeleteDataAsync($"eitt-api/project/{project.Oid}");
                if (response != null)
                {
                    showSuccessAlert = true;
                    toastService.ShowWarning("Project deleted successfully!");
                    await LoadProjectAsync();
                    FilterProjects();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting projects: {ex.Message}");
            }
        }

        public async Task LoadProjectByKeyAsync(int key)
        {
            try
            {
                projects = await baseApi.GetDataAsync<Project>("eitt-api/project/{key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting projects: {ex.Message}");
            }
        }

        public async Task OpenEditModal(Project item)
        {
            
            project = new Project { Oid = item.Oid, Description = item.Description };

            await JSRuntime.InvokeVoidAsync("openEditModal");
        }

        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }

        public void SearchProjects()
        {
            currentPage = 1;
            FilterProjects();
        }

        public void ClearSearch()
        {
            searchQuery = "";
            currentPage = 1;
            FilterProjects();
        }

        public void FilterProjects()
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredprojects = projects
                    .Where(p=>p.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                filteredprojects = new List<Project>(projects);
            }
            StateHasChanged();
        }

        public List<Project> pagedProjects => filteredprojects
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
