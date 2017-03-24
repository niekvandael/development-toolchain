using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Models;

namespace PreventionAdvisor.Models
{
    public class PreventionAdvisorDbContext : DbContext
    {
         public PreventionAdvisorDbContext (DbContextOptions<PreventionAdvisorDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
