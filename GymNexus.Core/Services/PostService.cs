using GymNexus.Core.Contracts;
using GymNexus.Infrastructure.Data;

namespace GymNexus.Core.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }
}