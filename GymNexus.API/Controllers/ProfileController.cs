using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymNexus.API.Controllers
{
    /// <summary>
    /// User profile management controller. Handles user profile picture updates and other profile related operations
    /// </summary>
    [Route("api/profile")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;

        public ProfileController(UserManager<ApplicationUser> userManager, IProfileService profileService)
        {
            _userManager = userManager;
            _profileService = profileService;
        }

        /// <summary>
        /// Updates the profile picture of the user
        /// </summary>
        /// <param name="model">Updated profile model data</param>
        /// <returns>Updated profile model with roles</returns>
        [HttpPut("update")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] ProfileUpdateDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest();
            }

            var response = await _profileService.UpdateProfileAsync(model, user);

            return Ok(response);
        }

        /// <summary>
        /// Gets all stores for the currently logged in user that are currently active in the system
        /// </summary>
        /// <returns>Stores that are owned by the logged user</returns>
        [HttpGet("stores")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StoreDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserStores()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var stores = await _profileService.GetUserStoresAsync(userId);
            return Ok(stores);
        }

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
