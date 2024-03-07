using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymNexus.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                result = await _userManager.AddToRoleAsync(user, "Writer");

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
        //[HttpPost("login")]
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        //{
        //    var user = await _authService.LoginAsync(loginDto);
        //    return Ok(user);
        //}
    }
}
