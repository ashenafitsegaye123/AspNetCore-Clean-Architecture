using BlogBackend.Infrastructure.Data;
using BlogBackend.Domain.Entities;
using Dapper;
using System.Data;
using System.Threading;
using BlogBackend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BlogBackend.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnectionFactory _connectionFactory;

        public PostRepository(ApplicationDbContext context, IDbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM Posts ORDER BY CreatedAt DESC";
            var command = new CommandDefinition(query, cancellationToken: cancellationToken);
            return await connection.QueryAsync<Post>(command);
        }

        public async Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT * FROM Posts WHERE Id = @Id";
            var param = new { Id = id };
            var command = new CommandDefinition(query, param, cancellationToken: cancellationToken);
            return await connection.QueryFirstOrDefaultAsync<Post>(command);
        }

        public async Task<Post> AddAsync(Post post, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
            return post;
        }

        public async Task<Post?> UpdateAsync(Post post, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Posts.Update(post);
            var affected = await _context.SaveChangesAsync(cancellationToken);
            return affected > 0 ? post : null;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var post = await _context.Posts.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}