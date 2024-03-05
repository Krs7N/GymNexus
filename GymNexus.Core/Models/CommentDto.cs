namespace GymNexus.Core.Models;

public class CommentDto
{
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;
}