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
    [Route("api/posts")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all posts that are currently active in the system
        /// </summary>
        /// <returns>All currently active posts</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPosts()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var posts = await _postService.GetAllAsync(userId);
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
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }


            var post = await _postService.AddPostAsync(postModel, user);
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

            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var post = await _postService.GetPostByIdAsync(id, userId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        /// <summary>
        /// Gets single post by it's id that is currently active in the system and updates it's value
        /// </summary>
        /// <returns>Updated post</returns>
        [HttpPut("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePostById([FromRoute] int id, [FromBody] PostFormDto post)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var updatedPost = await _postService.UpdatePostByIdAsync(id, post, user);

            return Ok(updatedPost);
        }

        /// <summary>
        /// Toggles like for specific user on a post
        /// </summary>
        /// <returns>If the current User has liked the post</returns>
        [HttpPut("{id:int}/like")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ToggleLikePostById([FromRoute] int id)
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

            await _postService.TogglePostLikeByIdAsync(id, userId);
            var isCurrentUserLiked = await _postService.IsCurrentUserLikedPostAsync(id, userId);

            return Ok(isCurrentUserLiked);
        }

        /// <summary>
        /// Adds a comment to specific post
        /// </summary>
        [HttpPut("{id:int}/comment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditPostComment([FromRoute] int id, [FromBody] CommentDto comment)
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

            await _postService.AddOrEditPostCommentAsync(id, comment, userId);

            return Ok();
        }

        /// <summary>
        /// Deletes a comment from specific post. Sets IsActive to false
        /// </summary>
        [HttpDelete("{id:int}/comment/{commentId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePostComment([FromRoute] int id, int commentId)
        {
            if (id < 0 || commentId < 0)
            {
                return BadRequest();
            }

            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _postService.DeletePostCommentAsync(id, commentId, userId);

            return Ok();
        }

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
