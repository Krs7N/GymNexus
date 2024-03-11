using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync(string userId)
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new PostDto()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                CreatedBy = p.Creator.UserName,
                Likes = p.PostsLikes.Count(pl => pl.PostId == p.Id),
                IsLikedByCurrentUser = p.PostsLikes.Any(pl => pl.UserId == userId),
                Comments = p.Comments
                    .Select(c => new CommentDto()
                    {
                        Content = c.Content,
                        CreatedOn = c.CreatedOn,
                        CreatedBy = c.Creator.UserName
                    })
                    .ToArray()
            })
            .ToListAsync();
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        return await _context.Posts
            .AsNoTracking()
            .Where(p => p.IsActive && p.Id == id)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                CreatedBy = p.Creator.UserName,
                Likes = p.PostsLikes.Count(pl => pl.PostId == p.Id),
                Comments = p.Comments
                    .Select(c => new CommentDto()
                    {
                        Content = c.Content,
                        CreatedOn = c.CreatedOn,
                        CreatedBy = c.Creator.UserName
                    })
                    .ToArray()
            })
            .FirstOrDefaultAsync();
    }

    public async Task TogglePostLikeByIdAsync(int id, string userId)
    {
        var post = await _context.Posts
            .Where(p => p.IsActive)
            .Include(p => p.PostsLikes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            throw new InvalidOperationException();
        }

        var like = post.PostsLikes.FirstOrDefault(pl => pl.UserId == userId);

        if (like == null)
        {
            await _context.PostsLikes.AddAsync(new PostLike()
            {
                PostId = post.Id,
                UserId = userId
            });
        }
        else
        {
            _context.PostsLikes.Remove(like);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCurrentUserLikedPostAsync(int id, string userId)
    {
        return await _context.PostsLikes
            .AsNoTracking()
            .AnyAsync(pl => pl.PostId == id && pl.UserId == userId);
    }

    public async Task<PostDto> AddPostAsync(PostFormDto postModel, ApplicationUser user)
    {
        var post = new Post()
        {
            Title = postModel.Title,
            Content = postModel.Content,
            ImageUrl = postModel.ImageUrl,
            CreatedBy = user.Id,
            CreatedOn = DateTime.Now
        };

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return new PostDto()
        {
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
            CreatedOn = post.CreatedOn.ToString(DateTimeFormat),
            CreatedBy = user.UserName,
            Likes = post.PostsLikes.Count(pl => pl.PostId == post.Id),
            Comments = post.Comments
                .Select(c => new CommentDto()
                {
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    CreatedBy = c.Creator.UserName
                })
                .ToArray()
        };
    }
}