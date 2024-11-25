using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext:DbContext
{
    public AppContext(DbContextOptions<AppContext> options) 
        : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/daniel/RiderProjects/DNPAsg1/EfcRepositories/app.db");
    }
}