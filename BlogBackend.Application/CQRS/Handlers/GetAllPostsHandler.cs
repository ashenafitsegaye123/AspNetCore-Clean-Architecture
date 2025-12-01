using MediatR;
using AutoMapper;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.CQRS.Queries;
using BlogBackend.Application.DTOs;
namespace BlogBackend.Application.CQRS.Handlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            // Fetch all posts (using Dapper or EF Core inside repository)
            var posts = await _postRepository.GetAllAsync();

            // Map entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}
