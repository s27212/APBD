using Microsoft.EntityFrameworkCore;

namespace Movies.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movies.Models.Movie> Movie { get; set; } = default!;
    }
}
