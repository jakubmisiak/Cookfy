using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cookfy.Entities;

public class CookfyDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(r => r.UserName).IsRequired().HasMaxLength(32);
        modelBuilder.Entity<User>().Property(r => r.Password).IsRequired();
        modelBuilder.Entity<User>().Property(r => r.Description).HasMaxLength(128);

        modelBuilder.Entity<Post>().Property(r => r.Title).IsRequired().HasMaxLength(128);
        modelBuilder.Entity<Post>().Property(r => r.Value).IsRequired().HasMaxLength(1024);
        modelBuilder.Entity<Post>().Property(r => r.Date).IsRequired();
        
        modelBuilder.Entity<Comment>().Property(r => r.Date).IsRequired();
        modelBuilder.Entity<Comment>().Property(r => r.Value).IsRequired().HasMaxLength(256);
    }

    public CookfyDbContext(DbContextOptions<CookfyDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Comment> Comments { get; set; }
}