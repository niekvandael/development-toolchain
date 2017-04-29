using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using System;
using System.Linq;

namespace PreventionAdvisor
{
    public static class DbInitializer
    {
        public async static void Initialize(PreventionAdvisorDbContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

#if !DEBUG
                return;
#endif

            //
            // Seeding for organizations
            //
            Organization org1 = new Organization { Name = "Benvitec NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.2093.30902", Phone = "011/81.12.34", Website = "http://www.benvitec.be" };
            Organization org2 = new Organization { Name = "PCT NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.9999.99999", Phone = "011/81.12.34", Website = "http://www.PVT-NV.be" };

            var organizations = new Organization[] {
                org1,
                org2
            };

            //
            // Seeding for users
            //

            var identityUser = new IdentityUser();
            if (await userManager.FindByEmailAsync("niek.vandael@gmail.com") == null) {
                identityUser = new IdentityUser()
                {
                    UserName = "niekvandael",
                    Email = "niek.vandael@gmail.com"
                };

                await userManager.CreateAsync(identityUser, "P@ssw0rd!");
            }

            AppUser appUser = new AppUser
            {
                Firstname = "Niek",
                Lastname = "Vandael",
                IdentityUser = identityUser,
                Organizations = organizations
            };
            context.AppUsers.Add(appUser);

            context.SaveChanges();

            //
            // Seeding for workplaces
            //
            Workplace wp1 = new Workplace() { Organization = org1, ProjectNumber = "193022",
                Address = new Address { City = "Hasselt", Country = "Belgium", Number = "17.01", Street = "Kempische Steenweg", Zipcode = "3520" },
                Title = "Corda Campus", ProjectLead = "Ludo Pellens", ProjectController = "Guy Loenders", Description = "Corda campus verbouwingen"};

            Workplace wp2 = new Workplace()
            {
                Organization = org1,
                ProjectNumber = "193022",
                Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" },
                Title = "Werf Soval",
                ProjectLead = "Ludo Pellens",
                ProjectController = "Guy Loenders",
                Description = "Soval inspectie project"
            };

            context.Workplaces.Add(wp1);
            context.Workplaces.Add(wp2);

            context.SaveChanges();

            // Seeding for Categories


        }
    }
}