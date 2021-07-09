using Microsoft.EntityFrameworkCore;

namespace ApiEscola.Models
{
    public class TestAppDbContext : DbContext
    {
        public TestAppDbContext(DbContextOptions<TestAppDbContext> options) :
            base(options)
        { }
        public DbSet<Escola> Escola { get; set; }
    }
}
