using GymNexus.Core.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using GymNexus.Core.Services;

namespace GymNexus.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all products that are currently active in the system
        /// </summary>
        /// <returns>All currently active products</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var products = await _productService.GetAllAsync(userId);
            return Ok(products);
        }

        /// <summary>
        /// Toggles like for specific user on a product
        /// </summary>
        /// <returns>If the current User has liked the product</returns>
        [HttpPut("{id:int}/like")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ToggleLikeProductById([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _productService.ToggleProductLikeByIdAsync(id, userId);
            var isCurrentUserLiked = await _productService.IsCurrentUserLikedProductAsync(id, userId);

            return Ok(isCurrentUserLiked);
        }

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
