﻿using GymNexus.API.Utils;
using GymNexus.Core.Contracts;
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
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
                    var response = new RegisterResponseDto()
                    {
                        Email = user.Email!,
                        ImageUrl = user.ProfilePictureUrl
                    };

                    return Ok(response);
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
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateJwtToken(user, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Email = user.Email,
                        ImageUrl = user.ProfilePictureUrl,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Invalid email or password");

            return ValidationProblem(ModelState);
        }
    }
}