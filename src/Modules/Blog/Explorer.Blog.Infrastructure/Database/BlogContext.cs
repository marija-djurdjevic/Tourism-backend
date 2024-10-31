using Explorer.Blog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Blogs> Blogs { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
        //modelBuilder.Entity<Blogs>().Property(item => item.Comments).HasColumnType("jsonb");
        modelBuilder.Entity<Blogs>().Property(item => item.Votes).HasColumnType("jsonb");
    }
}