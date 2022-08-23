using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using MotorReparation_Api.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorReparation_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly APISettings _apiSettings;
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<APISettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(authenticationDTO.UserName);

                if (user == null)
                {
                    return Unauthorized(new AuthenticationResponseDTO()
                    {
                        ErrorMessage = "Invalid Authentication",
                        IsAuthSuccessful = false
                    });
                }

                var signInCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signInCredentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticationResponseDTO()
                {
                    Token = token,
                    IsAuthSuccessful = true,
                    userDTO = new UserDTO()
                    {
                        Id = user.Id,
                        UserName = user.UserName
                    }
                });

            }
            else
            {
                return Unauthorized(new AuthenticationResponseDTO()
                {
                    ErrorMessage = "Invalid Authentication",
                    IsAuthSuccessful = false
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]RegisterRequestDTO registerRequestDTO)
        {
            if(registerRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = errors,
                    IsReisterationSuccessfull = false
                });
            }

            return StatusCode(201);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id",user.Id)
            };

            return claims;
        }
    }
}
