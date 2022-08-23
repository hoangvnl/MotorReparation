using Microsoft.AspNetCore.Components;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authenticationService.Logout();
            NavManager.NavigateTo("/");
        }
    }
}
