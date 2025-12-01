namespace BlogBackend.Domain.Entities;


public class Post
{
public Guid Id { get; set; }
public string Title { get; set; } = null!;
public string? Content { get; set; }
public DateTime CreatedAt { get; set; }
public DateTime? UpdatedAt { get; set; }
}