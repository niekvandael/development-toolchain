using PreventionAdvisor.Models;
using System;
using System.Linq;

namespace PreventionAdvisor
{
    public static class DbInitializer
    {
        public static void Initialize(PreventionAdvisorDbContext context)
        {
            context.Database.EnsureCreated();

            //
            // Seeding for users
            //

            var users = new User[] {
                new User{ Id =  Guid.NewGuid(), Firstname = "Raf", Lastname = "Pellens"},
                new User{ Id =  Guid.NewGuid(), Firstname = "Niek", Lastname = "Vandael"}
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            
            context.SaveChanges();
        }
    }
}