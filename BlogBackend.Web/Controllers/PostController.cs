using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogBackend.Application.CQRS.Commands;
using BlogBackend.Application.CQRS.Queries;
using BlogBackend.Application.DTOs;

namespace BlogPostApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ GET: api/post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
        {
            var posts = await _mediator.Send(new GetAllPostsQuery());
            return Ok(posts);
        }

        // ✅ GET: api/post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostById(Guid id)
        {
            var post = await _mediator.Send(new GetPostByIdQuery { Id = id });
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // ✅ POST: api/post
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new CreatePostCommand { CreatePostDto = dto });
            return CreatedAtAction(nameof(GetPostById), new { id = result.Id }, result);
        }


    }
}
