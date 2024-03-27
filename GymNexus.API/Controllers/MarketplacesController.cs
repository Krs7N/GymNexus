using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymNexus.API.Controllers
{
    [Route("api/marketplaces")]
    [ApiController]
    public class MarketplacesController : ControllerBase
    {
        private readonly IMarketplaceService _marketplaceService;

        public MarketplacesController(IMarketplaceService marketplaceService)
        {
            _marketplaceService = marketplaceService;
        }

        /// <summary>
        /// Gets all marketplaces that are currently active in the system
        /// </summary>
        /// <returns>All currently active marketplaces</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MarketplaceDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMarketplaces()
        {
            var marketplaces = await _marketplaceService.GetAllAsync();
            return Ok(marketplaces);
        }

        /// <summary>
        /// Gets all marketplaces that are currently active in the system and have at least one active store
        /// </summary>
        /// <returns>All currently active marketplaces with at least one active store</returns>
        [HttpGet("withStores")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MarketplaceViewDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMarketplacesWithStores()
        {
            var marketplaces = await _marketplaceService.GetAllWithStoresAsync();
            return Ok(marketplaces);
        }
    }
}
