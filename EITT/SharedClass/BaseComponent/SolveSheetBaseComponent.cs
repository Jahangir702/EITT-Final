using Blazored.Toast.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SharedClass.BaseComponent
{
    public class SolveSheetBaseComponent : ComponentBase
    {
        [Inject]
        BaseApi baseApi { get; set; }
        public string searchQuery = "";
        public List<SolveSheet> solveSheets = new List<SolveSheet>();
        public List<SolveSheet> filteredsolvesheet = new List<SolveSheet>();
        public SolveSheet solveSheet = new SolveSheet()
        {
            SolveDate = DateTime.Now
        };
        public int currentPage = 1;
        public int itemsPerPage = 5; // Set the number of items per page
        public int totalPages => (int)Math.Ceiling((double)filteredsolvesheet.Count / itemsPerPage);
        public bool AddRow = false;
        public List<IssueLog> issueLogs { get; set; }

        public bool showSuccessAlert = false;

        public SolveSheet SolveSheetToDelete {  get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadSolveSheetAsync();
            await LoadIssueLogAsync();
        }
        public async Task LoadSolveSheetAsync()
        {
            try
            {
                solveSheets = await baseApi.GetDataAsync<SolveSheet>("eitt-api/solvesheets");
                FilterSolveSheets();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading solveSheets: {ex.Message}");
            }
        }
        public async Task CreateSolveSheetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(solveSheet.Comments))
                {
                    await baseApi.PostDataAsync<SolveSheet>("eitt-api/solvesheet", solveSheet);

                    showSuccessAlert = true;
                    toastService.ShowSuccess("SolveSheet created successfully!");

                    await OnInitializedAsync();
                    solveSheet = new SolveSheet();
                    AddRow = false;
                    FilterSolveSheets();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading solveSheets: {ex.Message}");
            }
        }
        protected async Task ToggleEditMode(SolveSheet solveSheet)
        {
            solveSheet.EditMode = true;
        }

        protected async Task CancelEdit(SolveSheet solveSheet)
        {
            solveSheet.EditMode = false;
        }
        public async Task UpdateSolveSheetAsync(SolveSheet solveSheet)
        {
            try
            {
                await baseApi.PutDataAsync($"eitt-api/solvesheet/{solveSheet.Oid}", solveSheet);

                showSuccessAlert = true;

                toastService.ShowInfo("Solvesheet updated successfully!");
                //await ShowAlert("Solvesheet updated successfully!");

                await LoadSolveSheetAsync();
                FilterSolveSheets();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating solvesheets: {ex.Message}");
            }
        }

        public async Task DeleteSolveSheetAsync(SolveSheet solveSheet)
        {
            try
            {
                await baseApi.DeleteDataAsync($"eitt-api/solvesheet/{solveSheet.Oid}");

                toastService.ShowWarning("Solvesheet deleted successfully!");
                //await ShowAlert("Solvesheet deleted successfully!");

                await LoadSolveSheetAsync();
                FilterSolveSheets();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting SolveSheets: {ex.Message}");
            }
        }

        public async Task OpenEditModal(SolveSheet item)
        {
            solveSheet = new SolveSheet
            {
                Oid = item.Oid,
                SolveDate = item.SolveDate,
                Status = item.Status,
                Comments = item.Comments,
                ResolvedId = item.ResolvedId
            };

            await JSRuntime.InvokeVoidAsync("openEditModal");
        }
        public async Task LoadSolveSheetByKeyAsync(int key)
        {
            try
            {
                solveSheets = await baseApi.GetDataAsync<SolveSheet>("eitt-api/solvesheet/{key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading SolveSheet: {ex.Message}");
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
        
        public void SearchSolveSheet()
        {
            currentPage = 1;
            FilterSolveSheets();
        }

        public void ClearSearch()
        {
            searchQuery = "";
            currentPage = 1;
            FilterSolveSheets();
        }

        public void FilterSolveSheets()
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredsolvesheet = solveSheets
                    .Where(i => i.Comments.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                filteredsolvesheet = new List<SolveSheet>(solveSheets);
            }
            StateHasChanged();
        }
        public List<SolveSheet> pagedSolveSheets => filteredsolvesheet
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
