﻿using GymNexus.Core.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
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
    }
}