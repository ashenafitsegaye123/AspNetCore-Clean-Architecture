namespace BlogBackend.Application.DTOs;


public class CreatePostDto
{
public string Title { get; set; } = null!;
public string? Content { get; set; }
}