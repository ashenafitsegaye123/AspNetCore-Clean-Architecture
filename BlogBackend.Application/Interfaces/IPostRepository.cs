using BlogBackend.Domain.Entities;

namespace BlogBackend.Application.Interfaces
{
    public interface IPostRepository
    {
        // Reads (queries) - using Dapper for performance
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default);

        // Writes (commands) - using EF Core for ease
        Task<Post> AddAsync(Post post, CancellationToken cancellationToken = default);
        Task<Post?> UpdateAsync(Post post, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}