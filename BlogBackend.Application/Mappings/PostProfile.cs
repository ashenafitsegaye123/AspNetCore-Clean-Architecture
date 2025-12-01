using AutoMapper;
using BlogBackend.Domain.Entities;
using BlogBackend.Application.DTOs;


namespace BlogBackend.Application.Mappings;


public class PostProfile : Profile
{
public PostProfile()
{
CreateMap<Post, PostDto>();
CreateMap<CreatePostDto, Post>()
.ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
}
}