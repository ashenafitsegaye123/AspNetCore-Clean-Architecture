using MediatR;
using BlogBackend.Application.DTOs;
namespace BlogBackend.Application.CQRS.Commands
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public CreatePostDto CreatePostDto { get; set; } = null!;
    }
}
