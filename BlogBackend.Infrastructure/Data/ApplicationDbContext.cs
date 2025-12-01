
using BlogBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
{
}


public DbSet<Post> Posts => Set<Post>();


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);
modelBuilder.ApplyConfiguration(new Configurations.PostConfiguration());
}
}