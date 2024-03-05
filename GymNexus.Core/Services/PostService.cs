using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using static GymNexus.Infrastructure.Constants.DataConstants;

using Microsoft.EntityFrameworkCore;

namespace GymNexus.Core.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                CreatedBy = p.Creator.UserName
            })
            .ToListAsync();
    }
}