using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PreventionAdvisor.Models
{
    public class PreventionAdvisorDbContext : IdentityDbContext<IdentityUser>
    {
         public PreventionAdvisorDbContext (DbContextOptions<PreventionAdvisorDbContext> options)
            : base(options)
        {
        }

//        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fixes for InnoDB limitations
            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.Property(u => u.UserName).HasMaxLength(255);
                b.Property(u => u.NormalizedUserName).HasMaxLength(255);
                b.Property(u => u.Email).HasMaxLength(255);
                b.Property(u => u.NormalizedEmail).HasMaxLength(255);
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.Property(r => r.Name).HasMaxLength(255);
                b.Property(r => r.NormalizedName).HasMaxLength(255);
            });
        }
    }
}
