using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync(string userId);
    Task<PostDto?> GetPostByIdAsync(int id);
    Task<PostDto> AddPostAsync(PostFormDto postModel, ApplicationUser user);
    Task TogglePostLikeByIdAsync(int postId, string userId);
    Task<bool> IsCurrentUserLikedPostAsync(int id, string userId);
}