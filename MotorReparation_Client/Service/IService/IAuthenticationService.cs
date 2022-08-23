using Models;

namespace MotorReparation_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO authenticationDTO);
        Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequestDTO);
        Task Logout();
    }
}
