using Microsoft.EntityFrameworkCore;

namespace GetStartedDotnet.Models
{
    public class GreenLivingDbContext : DbContext
    {
         public GreenLivingDbContext (DbContextOptions<GreenLivingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
