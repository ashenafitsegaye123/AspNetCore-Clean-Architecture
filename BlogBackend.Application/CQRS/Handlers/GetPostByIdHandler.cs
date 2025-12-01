using MediatR;
using AutoMapper;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.CQRS.Queries;
using BlogBackend.Application.DTOs;

namespace BlogBackend.Application.CQRS.Handlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostByIdHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch post from repository
            var post = await _postRepository.GetByIdAsync(request.Id);

            // Return null if not found
            if (post == null) return null;

            // Map Post entity to PostDto
            return _mapper.Map<PostDto>(post);
        }
    }
}
