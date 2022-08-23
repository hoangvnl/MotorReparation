using Microsoft.AspNetCore.Components;
using Models;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO UserForAuthentication { get; set; } = new AuthenticationDTO();
        public bool ShowAuthenticationErrors { get; set; } = false;
        public string Errors { get; set; } = string.Empty;

        public bool IsProcessing { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        private async Task LoginUser()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;
            var result = await authenticationService.Login(UserForAuthentication);

            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;
                NavManager.NavigateTo("/");
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
