using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Contracts;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto?> GetPostByIdAsync(int id);
    Task<PostDto> AddPostAsync(PostFormDto postModel, ApplicationUser userId);
}