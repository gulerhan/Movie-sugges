using MovieSugges.Model;
using Microsoft.EntityFrameworkCore;
using static MovieSugges.Model.Comment;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Comments)
            .HasForeignKey(c => c.MovieId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Category)
            .WithMany(c => c.Movies)
            .HasForeignKey(m => m.CategoryId);
    }

}
