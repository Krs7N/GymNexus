using GymNexus.Core.Models;

namespace GymNexus.Core.Contracts;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
}