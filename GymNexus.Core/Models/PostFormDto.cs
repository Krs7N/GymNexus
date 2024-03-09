namespace GymNexus.Core.Models;

public class PostFormDto
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }
}