using AutoMapper;
using BlogBackend.Application.DTOs;
using BlogBackend.Domain.Entities;
using BlogBackend.Application.Interfaces;
using MediatR;
using BlogBackend.Application.CQRS.Commands;

namespace BlogBackend.Application.CQRS.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;

        public CreatePostHandler(IPostRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postEntity = _mapper.Map<Post>(request.CreatePostDto);
            var createdPost = await _repo.AddAsync(postEntity, cancellationToken);

            // Always return mapped DTO
            return _mapper.Map<PostDto>(createdPost);
        }
    }
}