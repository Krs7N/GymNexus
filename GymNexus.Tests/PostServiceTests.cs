using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Tests;

[TestFixture]
public class PostServiceTests : TestBase
{
    private IPostService _postService;

    [SetUp]
    public void SetUp()
    {
        _postService = new PostService(_context);
    }

    [Test]
    public async Task GetAllAsyncReturnsAllPosts()
    {
        var result = await _postService.GetAllAsync(User.Id);

        Assert.That(result.Count(), Is.EqualTo(4));
    }

    [Test]
    public async Task GetByIdAsyncWithValidIdReturnsPost()
    {
        var result = await _postService.GetPostByIdAsync(1, User.Id);

        Assert.IsNotNull(result);
        Assert.That(result!.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdAsyncWithInvalidIdReturnsNull()
    {
        var result = await _postService.GetPostByIdAsync(100, User.Id);

        Assert.IsNull(result);
    }

    [Test]
    public async Task ToggleLikeAsyncWithValidIdTogglesLike()
    {
        await _postService.TogglePostLikeByIdAsync(1, User.Id);

        var post = await _context.Posts.FindAsync(1);

        Assert.That(post.PostsLikes.Count, Is.EqualTo(1));
    }

    [Test]
    public void ToggleLikeAsyncWithInvalidIdThrowsException()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.TogglePostLikeByIdAsync(100, User.Id));
    }

    [Test]
    public async Task IsCurrentUserLikedAsyncWithValidIdReturnsTrue()
    {
        var postLike = new PostLike()
        {
            PostId = 1,
            UserId = User.Id
        };

        await _context.PostsLikes.AddAsync(postLike);
        await _context.SaveChangesAsync();

        var result = await _postService.IsCurrentUserLikedPostAsync(1, User.Id);

        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public async Task IsCurrentUserLikedAsyncWithInvalidIdReturnsFalse()
    {
        var result = await _postService.IsCurrentUserLikedPostAsync(100, User.Id);

        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public async Task AddCommentAsyncWithValidIdAddsComment()
    {
        var comment = new CommentDto()
        {
            Content = "Test Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
        };

        await _postService.AddOrEditPostCommentAsync(1, comment, User.Id);

        var post = await _context.Posts.FindAsync(1);

        Assert.That(post.Comments.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddCommentAsyncWithInvalidIdThrowsException()
    {
        var comment = new CommentDto()
        {
            Content = "Test Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.AddOrEditPostCommentAsync(100, comment, User.Id));
    }

    [Test]
    public async Task EditCommentAsyncWithValidIdUpdatesComment()
    {
        var comment = new Comment()
        {
            Id = 1,
            Content = "New Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
            PostId = 1
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        var commentDto = new CommentDto()
        {
            Id = 1,
            Content = "Test Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
        };

        await _postService.AddOrEditPostCommentAsync(1, commentDto, User.Id);

        var post = await _context.Posts.FindAsync(1);

        Assert.That(post.Comments.First().Content, Is.EqualTo(comment.Content));
    }

    [Test]
    public void EditCommentAsyncWithInvalidIdThrowsException()
    {
        var comment = new CommentDto()
        {
            Id = 1,
            Content = "Test Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.AddOrEditPostCommentAsync(100, comment, User.Id));
    }

    [Test]
    public async Task DeleteCommentAsyncWithValidIdDeletesComment()
    {
        var comment = new Comment()
        {
            Id = 1,
            Content = "Test Comment",
            CreatedBy = User.Email,
            CreatedOn = DateTime.Now,
            PostId = 1
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        await _postService.DeletePostCommentAsync(1, 1, User.Email);

        var post = await _context.Posts.FindAsync(1);

        Assert.That(comment.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void DeleteCommentAsyncWithInvalidIdThrowsException()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.DeletePostCommentAsync(100, 1, User.Email));
    }

    [Test]
    public void DeleteCommentAsyncWithInvalidCommentIdThrowsException()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.DeletePostCommentAsync(1, 100, User.Email));
    }

    [Test]
    public async Task CreateAsyncCreatesPost()
    {
        var post = new PostFormDto()
        {
            Title = "Test Post",
            Content = "Test Content",
            ImageUrl = "https://test.com/image.jpg"
        };

        var newPost = await _postService.AddPostAsync(post, User);

        Assert.That(post.Title, Is.EqualTo(newPost.Title));
        Assert.That(post.Content, Is.EqualTo(newPost.Content));
        Assert.That(post.ImageUrl, Is.EqualTo(newPost.ImageUrl));
    }

    [Test]
    public async Task UpdateAsyncWithValidIdUpdatesPost()
    { 
        var postModel = new PostFormDto()
        {
            Title = "Test Post",
            Content = "Test Content",
            ImageUrl = "https://test.com/image.jpg"
        };

        var updatedPost = await _postService.UpdatePostByIdAsync(Post.Id, postModel, User);

        Assert.That(postModel.Title, Is.EqualTo(updatedPost.Title));
        Assert.That(postModel.Content, Is.EqualTo(updatedPost.Content));
        Assert.That(postModel.ImageUrl, Is.EqualTo(updatedPost.ImageUrl));
    }

    [Test]
    public void UpdateAsyncWithInvalidIdThrowsException()
    {
        var postModel = new PostFormDto()
        {
            Title = "Test Post",
            Content = "Test Content",
            ImageUrl = "https://test.com/image.jpg"
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.UpdatePostByIdAsync(100, postModel, User));
    }

    [Test]
    public async Task DeleteAsyncWithValidIdDeletesPost()
    {
        await _postService.DeletePostByIdAsync(Post.Id);

        Assert.That(Post.IsActive, Is.EqualTo(false));
    }

    [Test]
    public void DeleteAsyncWithInvalidIdThrowsException()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.DeletePostByIdAsync(100));
    }
}