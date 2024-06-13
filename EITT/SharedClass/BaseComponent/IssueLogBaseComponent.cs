using Blazored.Toast.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static Utilities.Constants.Enums;

namespace SharedClass.BaseComponent
{
    public class IssueLogBaseComponent : ComponentBase
    {
        [Inject]
        BaseApi baseApi { get; set; }
        public string searchQuery = "";
        public List<IssueLog> issueLogs = new List<IssueLog>();
        public List<IssueLog> filteredissueLogs = new List<IssueLog>();
        public IssueLog issueLog = new IssueLog()
        {
            IssueDate = DateTime.Now,
        };
        public int currentPage = 1;
        public int itemsPerPage = 5; // Set the number of items per page
        public int totalPages => (int)Math.Ceiling((double)filteredissueLogs.Count / itemsPerPage);
        public bool AddRow = false;
        public List<Module> modules { get; set; }

        public List<UserAccount> userAccounts { get; set; }

        private UserAccount CurrentUser;

        public bool showSuccessAlert = false;

        public IssueLog IssueLogToDelete { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadIssueLogAsync();
            await LoadUserAccountAsync();
            await LoadModuleAsync();
        }

        public async Task LoadIssueLogAsync()
        {
            try
            {
                issueLogs = await baseApi.GetDataAsync<IssueLog>("eitt-api/issueLogs");
                FilterIssueLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading IssueLogs: {ex.Message}");
            }
        }

        public async Task CreateIssueLogAsync()
        {
            try
            {
                var currentUserJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");

                if (!string.IsNullOrEmpty(currentUserJson))
                {
                    CurrentUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(currentUserJson);

                    issueLog.UserAccountId = CurrentUser.Oid;

                    if (!string.IsNullOrEmpty(issueLog.IncidentCategory))
                    {
                        await baseApi.PostDataAsync<IssueLog>("eitt-api/issueLog", issueLog);

                        showSuccessAlert = true;
                        toastService.ShowSuccess("IssueLog created successfully!");
                        await OnInitializedAsync();
                        issueLog = new IssueLog();
                        AddRow = false;
                        FilterIssueLogs();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading issuelogs: {ex.Message}");
            }
        }


        protected async Task ToggleEditMode(IssueLog issueLog)
        {
            issueLog.EditMode = true;
        }
        protected async Task CancelEdit(IssueLog issueLog)
        {
            issueLog.EditMode = false;
        }

        public async Task UpdateIssueLogAsync(IssueLog issueLog)
        {
            try
            {
                var currentUserJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");

                if (!string.IsNullOrEmpty(currentUserJson))
                {
                    CurrentUser = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(currentUserJson);

                    issueLog.UserAccountId = CurrentUser.Oid;

                    if (!string.IsNullOrEmpty(issueLog.IncidentCategory))
                    {
                        issueLog.EditMode = true;
                        await baseApi.PutDataAsync($"eitt-api/issueLog/{issueLog.Oid}", issueLog);

                        showSuccessAlert = true;

                        //await ShowAlert("Issue log updated successfully!");
                        toastService.ShowInfo("Issuelog updated successfully!");

                        await LoadIssueLogAsync();
                        FilterIssueLogs();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating IssueLog: {ex.Message}");
            }
        }

        public async Task DeleteIssueLogAsync(IssueLog issueLog)
        {
            try
            {
                await baseApi.DeleteDataAsync($"eitt-api/issueLog/{issueLog.Oid}");

                //await ShowAlert("IssueLog deleted successfully!");
                toastService.ShowWarning("IssueLog deleted successfully!");

                await LoadIssueLogAsync();
                FilterIssueLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting IssueLogs: {ex.Message}");
            }
        }
        public async Task OpenEditModal(IssueLog item)
        {
            issueLog = new IssueLog { Oid = item.Oid, IncidentCategory = item.IncidentCategory, IssueDescription =item.IssueDescription,
            TestProdcedure = item.TestProdcedure, Imagelinks = item.Imagelinks, Prototypelink=item.Prototypelink, Comments=item.Comments, 
                Priority = item.Priority, IssueDate = item.IssueDate
            };

            await JSRuntime.InvokeVoidAsync("openEditModal");
        }

        public async Task LoadIssueLogByKeyAsync(int key)
        {
            try
            {
                issueLogs = await baseApi.GetDataAsync<IssueLog>("eitt-api/issueLog/{key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading issueLog: {ex.Message}");
            }
        }

        public async Task LoadModuleAsync()
        {
            try
            {
                modules = await baseApi.GetDataAsync<Module>("eitt-api/modules");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modules: {ex.Message}");
            }
        }

        public async Task LoadUserAccountAsync()
        {
            try
            {
                userAccounts = await baseApi.GetDataAsync<UserAccount>("eitt-api/user-accounts");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Loading user: {ex.Message}");
            }
        }

        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }

        public void SearchIssueLogs()
        {
            currentPage = 1;
            FilterIssueLogs();
        }

        public void ClearSearch()
        {
            searchQuery = "";
            currentPage = 1;
            FilterIssueLogs();
        }

        public void FilterIssueLogs()
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredissueLogs = issueLogs
                    .Where(i=>i.IssueDescription.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                filteredissueLogs = new List<IssueLog>(issueLogs);
            }
            StateHasChanged();
        }
        public List<IssueLog> pagedIssueLogs => filteredissueLogs
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