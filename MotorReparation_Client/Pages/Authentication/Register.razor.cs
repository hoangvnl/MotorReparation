using Microsoft.AspNetCore.Components;
using Models;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.Authentication
{
    public partial class Register
    {
        private RegisterRequestDTO RegisterRequestDTO { get; set; } = new RegisterRequestDTO();
        public bool ShowRegistrationErrors { get; set; } = false;
        public bool IsProcessing { get; set; } = false;
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        public async Task RegisterUser()
        {
            IsProcessing = true;
            ShowRegistrationErrors = false;
            var result = await authenticationService.Register(RegisterRequestDTO);

            if (result.IsReisterationSuccessfull)
            {
                IsProcessing = false;
                NavManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
        }
    }
}
