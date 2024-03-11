using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GymNexus.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GymNexus.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize(Roles = Roles.Writer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Gets all posts that are currently active in the system
        /// </summary>
        /// <returns>All currently active posts</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Adds a new post to the system
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPost([FromBody] PostFormDto postModel)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var post = await _postService.AddPostAsync(postModel, userId);
            return Ok(post);
        }

        /// <summary>
        /// Gets single post by it's id that is currently active in the system
        /// </summary>
        /// <returns>Active post by it's id</returns>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
    }
}
