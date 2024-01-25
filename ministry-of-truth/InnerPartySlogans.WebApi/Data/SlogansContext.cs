using Microsoft.EntityFrameworkCore;

namespace InnerPartySlogans.WebApi.Data
{
    public class SlogansContext : DbContext
    {
        public SlogansContext(DbContextOptions<SlogansContext> options) : base(options)
        {
        }

        public DbSet<Slogan> Slogan { get; set; }
    }
}
