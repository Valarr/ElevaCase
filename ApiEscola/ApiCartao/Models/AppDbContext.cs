using Microsoft.EntityFrameworkCore;

namespace ApiEscola.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }
        public DbSet<Escola> Escola { get; set; }
    }
}
