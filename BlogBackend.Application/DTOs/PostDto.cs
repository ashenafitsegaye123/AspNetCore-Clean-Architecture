namespace BlogBackend.Application.DTOs;


public class PostDto
{
public Guid Id { get; set; }
public string Title { get; set; } = null!;
public string? Content { get; set; }
public DateTime CreatedAt { get; set; }
public DateTime? UpdatedAt { get; set; }
}