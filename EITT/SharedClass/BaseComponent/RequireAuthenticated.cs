using Microsoft.AspNetCore.Components;

namespace SharedClass.BaseComponent
{
    public class RequireAuthenticated : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public AuthenticationService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticated())
                NavigationManager.NavigateTo("/");
        }
    }
}
