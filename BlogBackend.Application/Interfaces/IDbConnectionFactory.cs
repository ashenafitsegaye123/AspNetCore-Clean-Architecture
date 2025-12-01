using System.Data;

namespace BlogBackend.Application.Interfaces
{
    /// <summary>
    /// DIP abstraction for creating DB connections (used in Dapper reads).
    /// Allows swapping providers (e.g., SQL Server → PostgreSQL).
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}