using MediatR;
using BlogBackend.Application.DTOs;

namespace BlogBackend.Application.CQRS.Queries
{
    // Query: a request to retrieve data
    public class GetAllPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        // No parameters needed for now, but you could add filters or pagination later
    }
}
