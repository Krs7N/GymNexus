namespace GymNexus.Core.Models;

public class PostDto
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string CreatedOn { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public int Likes { get; set; }

    public CommentDto[] Comments { get; set; } = null!;
}