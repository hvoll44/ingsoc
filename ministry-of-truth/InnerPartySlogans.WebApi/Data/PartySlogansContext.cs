using Microsoft.EntityFrameworkCore;

namespace InnerPartySlogans.WebApi.Data
{
    public class PartySlogansContext : DbContext
    {
        public PartySlogansContext(DbContextOptions<PartySlogansContext> options) : base(options)
        {
        }

        public DbSet<Slogan> Slogan { get; set; }
    }
}
