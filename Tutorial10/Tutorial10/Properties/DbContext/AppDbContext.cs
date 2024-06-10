using Tutorial10.Properties.Models;

namespace Tutorial10.Properties.DbContext;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly string _connectionString;
    
    public AppDbContext()
    {

    }

    public AppDbContext(IConfiguration config, DbContextOptions options) : base(options)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(config), "Connection string is not set");
    }

    public DbSet<AppUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}