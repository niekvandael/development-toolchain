using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using System;
using System.Collections.Generic;
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
            if (await userManager.FindByEmailAsync("niek.vandael@gmail.com") == null)
            {
                identityUser = new IdentityUser()
                {
                    UserName = "niekvandael",
                    Email = "niek.vandael@gmail.com"
                };

                await userManager.CreateAsync(identityUser, "P@ssw0rd!");
            }

            var identityUser1 = new IdentityUser();
            if (await userManager.FindByEmailAsync("user1@test.be") == null)
            {
                identityUser1 = new IdentityUser()
                {
                    UserName = "user1",
                    Email = "user1@test.be"
                };

                await userManager.CreateAsync(identityUser1, "user1");
            }


            var identityUser2 = new IdentityUser();
            if (await userManager.FindByEmailAsync("user2@test.be") == null)
            {
                identityUser2 = new IdentityUser()
                {
                    UserName = "user2",
                    Email = "user2@test.be"
                };

                await userManager.CreateAsync(identityUser2, "user2");
            }

            var identityUser3 = new IdentityUser();
            if (await userManager.FindByEmailAsync("user3@test.be") == null)
            {
                identityUser3 = new IdentityUser()
                {
                    UserName = "user3",
                    Email = "user3@test.be"
                };

                await userManager.CreateAsync(identityUser3, "user3");
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
            Workplace wp1 = new Workplace()
            {
                Organization = org1,
                ProjectNumber = "193022",
                Address = new Address { City = "Hasselt", Country = "Belgium", Number = "17.01", Street = "Kempische Steenweg", Zipcode = "3520" },
                Title = "Corda Campus",
                ProjectLead = "Ludo Pellens",
                ProjectController = "Guy Loenders",
                Description = "Corda campus verbouwingen",
                ChecklistItems = new List<ChecklistItem>()
            };

            Workplace wp2 = new Workplace()
            {
                Organization = org1,
                ProjectNumber = "193022",
                Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" },
                Title = "Werf Soval",
                ProjectLead = "Ludo Pellens",
                ProjectController = "Guy Loenders",
                Description = "Soval inspectie project",
                ChecklistItems = new List<ChecklistItem>()
            };

            context.Workplaces.Add(wp1);
            context.Workplaces.Add(wp2);

            context.SaveChanges();

            // Seeding for Categories
            Category category1 = new Category { Title = "Werfinrichting", User = appUser };
            Category category2 = new Category { Title = "Orde & netheid", User = appUser };

            context.Categories.Add(category1);
            context.Categories.Add(category2);

            context.SaveChanges();

            // Seeding for Checklist items
            ChecklistItem item1 = new ChecklistItem
            {
                Category = category1,
                CategoryId = category1.Id,
                Title = "Toegang en wegen",
                Status = 1,
                Description = "Wegen zijn in orde",
                User = appUser
            };

            ChecklistItem item2 = new ChecklistItem
            {
                Category = category1,
                CategoryId = category1.Id,
                Title = "Verlichting",
                Status = 2,
                Description = "",
                User = appUser
            };

            ChecklistItem item3 = new ChecklistItem
            {
                Category = category1,
                CategoryId = category1.Id,
                Title = "Opslag en materiaal",
                Status = 1,
                Description = "",
                User = appUser

            };

            ChecklistItem item4 = new ChecklistItem
            {
                Category = category1,
                CategoryId = category1.Id,
                Title = "Eet- en kleedruimte",
                Status = 1,
                Description = "",
                User = appUser
            };

            ChecklistItem item5 = new ChecklistItem
            {
                Category = category2,
                CategoryId = category2.Id,
                Title = "Bouwterreind / Bouwwegen",
                Status = 1,
                Description = "",
                User = appUser,
                UserId = appUser.Id
            };

            ChecklistItem item6 = new ChecklistItem
            {
                Category = category2,
                CategoryId = category2.Id,
                Title = "Werkplek",
                Status = 1,
                Description = "",
                User = appUser
            };

            ChecklistItem item7 = new ChecklistItem
            {
                Category = category2,
                CategoryId = category2.Id,
                Title = "Opslag (inclusief stabiliteit)",
                Status = 2,
                Description = "",
                User = appUser
            };

            wp1.ChecklistItems.Add(item1);
            wp1.ChecklistItems.Add(item2);
            wp1.ChecklistItems.Add(item3);
            wp1.ChecklistItems.Add(item4);
            wp1.ChecklistItems.Add(item5);
            wp1.ChecklistItems.Add(item6);

            context.SaveChanges();
        }
    }
}