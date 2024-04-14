using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymNexus.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Registered user</returns>
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email?.Trim(),
                UserName = registerDto.Email?.Trim(),
                ProfilePictureUrl = registerDto.ImageUrl
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.Writer);

                if (result.Succeeded)
                {
                    return Ok();
                }
                
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Logs in a user in the system
        /// </summary>
        /// <param name="loginDto">User login data</param>
        /// <returns>Logged in user</returns>
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    await _signInManager.SignInAsync(user, true);

                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateJwtToken(user, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        ImageUrl = user.ProfilePictureUrl,
                        Roles = roles.ToList(),
                        IsExternal = false,
                        Token = jwtToken
                    };


                    return Ok(response);
                }
            }

            ModelState.AddModelError("message", "Invalid email or password");

            return ValidationProblem(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Logs in a user in the system using facebook
        /// </summary>
        /// <returns>Logged in user</returns>
        [HttpGet("login/facebook")]
        public IActionResult LoginFacebook()
        {
            string? redirectUrl = Url.Action(nameof(LoginFacebookCallback), "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        /// <summary>
        /// The callback response from facebook API after a user is authenticated
        /// </summary>
        [HttpGet("login/facebook-callback")]
        public async Task<IActionResult> LoginFacebookCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
            }

            var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var imageUrl = info.Principal.FindFirstValue("picture");
            var user = new ApplicationUser { FirstName = firstName, LastName = lastName, ProfilePictureUrl = imageUrl, UserName = email, Email = email };
            var createResult = await _userManager.CreateAsync(user);

            if (createResult.Succeeded)
            {
                createResult = await _userManager.AddLoginAsync(user, info);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
            }

            await _userManager.AddToRoleAsync(user, Roles.Writer);
            var roles = new List<string> { Roles.Writer };

            var jwtToken = _tokenService.CreateJwtToken(user, roles.ToList());

            var cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1),
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("Authorization", $"Bearer {jwtToken}", cookieOptions);

            return Redirect($"http://localhost:4200/map#{user.Email}");
        }

        /// <summary>
        /// Gets the external information for the user for the different providers.
        /// </summary>
        /// <returns>Log in information</returns>
        [HttpGet("externalLogin/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExternalLoginInfo([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Error loading external login information.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var response = new LoginResponsePartialDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ImageUrl = user.ProfilePictureUrl,
                Roles = roles.ToList(),
                IsExternal = true
            };

            return Ok(response);
        }
    }
}
