using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto?> GetPostByIdAsync(int id);
    Task<PostDto> AddPostAsync(PostFormDto postModel, string userId);
}