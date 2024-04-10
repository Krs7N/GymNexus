using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using GymNexus.Core.Services;

namespace GymNexus.API.Controllers
{
    /// <summary>
    /// Represents the admin controller. Handles admin related actions and operations
    /// </summary>
    [Route("api/admin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminRoles")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IAdminService adminService, UserManager<ApplicationUser> userManager)
        {
            _adminService = adminService;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets most liked post that is currently active in the system
        /// </summary>
        /// <returns>Most liked post</returns>
        [HttpGet("posts/mostLiked")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostPreviewDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostLikedPost()
        {
            var post = await _adminService.GetMostLikedPostAsync();

            if (post == null)
            {
                return NoContent();
            }

            return Ok(post);
        }

        /// <summary>
        /// Gets most commented post that is currently active in the system
        /// </summary>
        /// <returns>Most commented post</returns>
        [HttpGet("posts/mostCommented")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostPreviewDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostCommentedPost()
        {
            var post = await _adminService.GetMostCommentedPostAsync();

            if (post == null)
            {
                return NoContent();
            }

            return Ok(post);
        }

        /// <summary>
        /// Gets the total count of all orders that have been made since the system is live
        /// </summary>
        /// <returns>The count of the orders that have been made</returns>
        [HttpGet("orders")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllOrdersCount()
        {
            int count = await _adminService.GetAllOrdersCountAsync();

            return Ok(count);
        }

        /// <summary>
        /// Gets the total count of pending orders that have been made since the system is live
        /// </summary>
        /// <returns>The count of the pending orders that have been made</returns>
        [HttpGet("orders/pending")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllPendingOrdersCount()
        {
            int count = await _adminService.GetPendingOrdersCountAsync();

            return Ok(count);
        }

        /// <summary>
        /// Gets the total count of all orders that have been marked as confirmed. They are still active in the system
        /// </summary>
        /// <returns>The count of the confirmed orders that have been made</returns>
        [HttpGet("orders/confirmed")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllConfirmedOrdersCount()
        {
            int count = await _adminService.GetConfirmedOrdersCountAsync();

            return Ok(count);
        }

        /// <summary>
        /// Gets the total count of all orders that have been marked as completed. They are no longer active in the system
        /// </summary>
        /// <returns>The count of the completed orders that have been made</returns>
        [HttpGet("orders/completed")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllCompletedOrdersCount()
        {
            int count = await _adminService.GetCompletedOrdersCountAsync();

            return Ok(count);
        }

        /// <summary>
        /// Gets all orders that have been made with their product details inside
        /// </summary>
        /// <returns>All orders and their details along with the products inside</returns>
        [HttpGet("orders/details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _adminService.GetAllOrdersAsync();

            return Ok(orders);
        }

        /// <summary>
        /// Changes the status of an order that is still active within the admin dashboard
        /// </summary>
        /// <returns>The new changed status</returns>
        [HttpPut("orders/{id}/changeStatus")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int id, [FromBody] string status)
        {
            var newStatus = await _adminService.ChangeOrderStatusAsync(id, status);

            return Ok(newStatus);
        }

        /// <summary>
        /// Adds a new marketplace partner to the system
        /// </summary>
        /// <returns>The new changed status</returns>
        [HttpPost("marketplaces")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMarketplace([FromBody] MarketplaceFormDto marketplaceModel)
        {
            await _adminService.AddMarketplaceAsync(marketplaceModel);

            return Ok();
        }

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
