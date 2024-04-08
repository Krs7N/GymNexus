namespace GymNexus.Core.Models;

public class PostPreviewDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int? Likes { get; set; }

    public int? Comments { get; set; }
}