using Blazored.Toast.Services;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SharedClass.BaseComponent
{
    public class UserBaseComponent : ComponentBase
    {
        [Inject]
        BaseApi baseApi { get; set; }

        [Inject]
        AuthenticationService? authenticationService { get; set; }

        public string searchQuery = "";

        public List<UserAccount> userAccounts = new List<UserAccount>();
        public List<UserAccount> filteredUserAccounts = new List<UserAccount>();

        public UserAccount userAccount = new UserAccount();
        public LoginDto loginDto = new LoginDto();

        public int currentPage = 1;
        public int itemsPerPage = 5; // Set the number of items per page
        public int totalPages => (int)Math.Ceiling((double)filteredUserAccounts.Count / itemsPerPage);

        public bool showSuccessAlert = false;
        public string ErrorMessage;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadUserAccountAsync();
        }

        public async Task LoadUserAccountAsync()
        {
            try
            {
                userAccounts = await baseApi.GetDataAsync<UserAccount>("eitt-api/user-accounts");
                FilterUserAccounts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Loading user: {ex.Message}");
            }
        }

        public void SearchUserAccounts()
        {
            currentPage = 1;
            FilterUserAccounts();
        }

        public void ClearSearch()
        {
            searchQuery = "";
            currentPage = 1;
            FilterUserAccounts();
        }

        public void FilterUserAccounts()
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredUserAccounts = userAccounts
                    .Where(p => p.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                filteredUserAccounts = new List<UserAccount>(userAccounts);
            }

            StateHasChanged();
        }

        public List<UserAccount> PagedUserAccounts => filteredUserAccounts
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

        public async Task RegisterUserAsync()
        {
            try
            {
                var response = await baseApi.PostDataAsync("eitt-api/user-account", userAccount);

                if (response != null)
                {
                    showSuccessAlert = true;

                    toastService.ShowSuccess("User created successfully!");
                    //await ShowAlert("User created successfully!");

                    NavigationManager.NavigateTo("/");

                    FilterUserAccounts();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateUserAsync()
        {
            try
            {
                var response = await baseApi.PutDataAsync($"eitt-api/user-account/{userAccount.Oid}", userAccount);

                if (response != null)
                {
                    showSuccessAlert = true;

                    toastService.ShowInfo("User updated successfully!");
                    //await ShowAlert("User updated successfully!");

                    NavigationManager.NavigateTo("/user-accounts");
                    FilterUserAccounts();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating User: {ex.Message}");
            }
        }

        public async Task LoginAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(loginDto.Username) && !string.IsNullOrEmpty(loginDto.Password))
                {
                    bool isSuccess = await authenticationService.UserLogin(loginDto);

                    if (isSuccess)
                    {
                        toastService.ShowSuccess("Login Successful!");
                        //await ShowAlert("Login Successful!");

                        NavigationManager.NavigateTo("/dashboard");
                    }
                    else
                    {
                        //await ShowAlert("User not found. Please Check your Credentials.");
                        toastService.ShowWarning("User not found. Please Check your Credentials.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                Console.WriteLine($"Error during login: {ex.Message}");
            }
        }

        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }
    }
}