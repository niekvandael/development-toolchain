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
            Organization org1 = new Organization { Workplaces = new List<Workplace>(), Name = "Benvitec NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.2093.30902", Phone = "011/81.12.34", Website = "http://www.benvitec.be" };
            Organization org2 = new Organization { Workplaces = new List<Workplace>(), Name = "PCT NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.9999.99999", Phone = "011/81.12.34", Website = "http://www.PVT-NV.be" };


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
            if (await userManager.FindByEmailAsync("raf@pellens.be") == null)
            {
                identityUser2 = new IdentityUser()
                {
                    UserName = "rafpellens",
                    Email = "raf@pellens.be"
                };

                await userManager.CreateAsync(identityUser2, "rafpellens");
            }

            var identityUser3 = new IdentityUser();
            if (await userManager.FindByEmailAsync("user3@test.be") == null)
            {
                identityUser3 = new IdentityUser()
                {
                    UserName = "rafpellens",
                    Email = "user3@test.be"
                };

                await userManager.CreateAsync(identityUser3, "user3");
            }

            AppUser appUser = new AppUser
            {
                Firstname = "Niek",
                Lastname = "Vandael",
                IdentityUser = identityUser,
                Organizations = new List<Organization>(){org1, org2}
            };
            context.AppUsers.Add(appUser);

            //
            // Seeding for workplaces
            //
            Workplace wp1 = new Workplace()
            {
                ProjectNumber = "193022",
                Address = new Address { City = "Hasselt", Country = "Belgium", Number = "17.01", Street = "Kempische Steenweg", Zipcode = "3520" },
                Title = "Corda Campus",
                ProjectLead = "Ludo Pellens",
                ProjectController = "Guy Loenders",
                Description = "Corda campus verbouwingen",
                User = appUser,
                Categories = new List<Category>()
            };

            Workplace wp2 = new Workplace()
            {
                ProjectNumber = "193022",
                Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" },
                Title = "Werf Soval",
                ProjectLead = "Ludo Pellens",
                ProjectController = "Guy Loenders",
                Description = "Soval inspectie project",
                User = appUser,
                Categories = new List<Category>()
            };

            Workplace defaultWorkplace = DbInitializer.getDefaultWorkplace();
            
            foreach (Category cat in defaultWorkplace.Categories){
                wp1.Categories.Add(cat.getClone());
                wp2.Categories.Add(cat.getClone());
            }
        
            org1.Workplaces.Add(wp1);
            org1.Workplaces.Add(wp2);
            
            var organizations = new Organization[] {
                org1,
                org2
            };
            context.Organizations.Add(org1);
            context.Organizations.Add(org2);

            context.SaveChanges();
        }

        public static Workplace getDefaultWorkplace()
        {
            Organization org = new Organization(){
                Address = new Address(){},
                Name = "default"
            };

            Workplace workplace = new Workplace()
            {
                ProjectNumber = "",
                Address = new Address {  },
                Title = "default",
                ProjectLead = "",
                ProjectController = "",
                Description = "",
                Categories = new List<Category>()
            };

            Category cat1 = new Category { Title = "Werfinrichting", ChecklistItems = new List<ChecklistItem>() };
            Category cat2 = new Category { Title = "Orde & netheid", ChecklistItems = new List<ChecklistItem>()  };

            ChecklistItem item1 = new ChecklistItem
            {
                Title = "Toegang en wegen",
                Status = 1,
                Description = "Wegen zijn in orde",
            };

            ChecklistItem item2 = new ChecklistItem
            {
                Title = "Verlichting",
                Status = 2,
                Description = ""
            };

            ChecklistItem item3 = new ChecklistItem
            {
                Title = "Opslag en materiaal",
                Status = 1,
                Description = ""
            };

            ChecklistItem item4 = new ChecklistItem
            {
                Title = "Eet- en kleedruimte",
                Status = 1,
                Description = ""
            };

            ChecklistItem item5 = new ChecklistItem
            {
                Title = "Bouwterrein / Bouwwegen",
                Status = 1,
                Description = ""
            };

            ChecklistItem item6 = new ChecklistItem
            {
                Title = "Werkplek",
                Status = 1,
                Description = ""
            };

            ChecklistItem item7 = new ChecklistItem
            {
                Title = "Opslag (inclusief stabiliteit)",
                Status = 2,
                Description = ""
            };
            
            cat1.ChecklistItems.Add(item1);
            cat1.ChecklistItems.Add(item2);
            cat1.ChecklistItems.Add(item3);
            cat2.ChecklistItems.Add(item4);
            cat2.ChecklistItems.Add(item5);
            cat2.ChecklistItems.Add(item6);
            cat2.ChecklistItems.Add(item7);

            workplace.Categories.Add(cat1);
            workplace.Categories.Add(cat2);

            workplace.Organization = org;
            return workplace;
        }

    }
}