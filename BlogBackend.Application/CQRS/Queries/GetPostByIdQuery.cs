using MediatR;
using BlogBackend.Application.DTOs;

namespace BlogBackend.Application.CQRS.Queries
{
    public class GetPostByIdQuery : IRequest<PostDto?>
    {
        public Guid Id { get; set; }
    }
}
