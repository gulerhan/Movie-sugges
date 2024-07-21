using LoginBackend.Model;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Category> Categories { get; set; }
}
