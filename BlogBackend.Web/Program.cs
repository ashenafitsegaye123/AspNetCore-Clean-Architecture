using BlogBackend.Application;      // For scanning CQRS/Handlers
using BlogBackend.Infrastructure;
using BlogBackend.Application.CQRS.Handlers;    // For scanning Repos
using BlogBackend.Infrastructure.Data;
using BlogBackend.Application.Mappings;  // For PostProfile
using MediatR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// EF for writes (in Infrastructure)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dapper factory (in Infrastructure)
builder.Services.AddScoped<BlogBackend.Application.Interfaces.IDbConnectionFactory, BlogBackend.Infrastructure.Data.SqlConnectionFactory>();

// Repositories (DIP: Interface from App, Impl from Infra)
builder.Services.AddScoped<BlogBackend.Application.Interfaces.IPostRepository, BlogBackend.Infrastructure.Repositories.PostRepository>();

// AutoMapper (scan Application for profiles)
builder.Services.AddAutoMapper(typeof(PostProfile));  // Or: AddAutoMapper(Assembly.Load(nameof(BlogBackend.Application)));

// MediatR (scan Application for handlers/commands)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePostHandler).Assembly));  // Or Application assembly

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();