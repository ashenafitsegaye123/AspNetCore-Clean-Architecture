using System.Data;
using Microsoft.Data.SqlClient;
using BlogBackend.Application.Interfaces;
using Microsoft.Extensions.Configuration;  // For IConfiguration (fixes CS0246)
using Microsoft.Extensions.Logging;

namespace BlogBackend.Infrastructure.Data
{
    /// <summary>
    /// Factory for SQL Server connections used in Dapper queries (reads).
    /// Implements IDbConnectionFactory from Repositories for DIP.
    /// </summary>
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private readonly ILogger<SqlConnectionFactory> _logger;

        public SqlConnectionFactory(IConfiguration configuration, ILogger<SqlConnectionFactory> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger.LogInformation("SqlConnectionFactory initialized with connection string (redacted for security).");
        }

        public IDbConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                var ex = new InvalidOperationException("Connection string cannot be null or empty.");
                _logger.LogError(ex, "Failed to create connection due to invalid string.");
                throw ex;
            }

            _logger.LogDebug("Creating new pooled SQL Server connection.");
            return new SqlConnection(_connectionString);
        }
    }
}