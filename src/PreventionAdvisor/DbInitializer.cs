using Microsoft.AspNetCore.Identity;
using PreventionAdvisor.Models;
using System;
using System.Linq;

namespace PreventionAdvisor
{
    public static class DbInitializer
    {
        public async static void Initialize(PreventionAdvisorDbContext context, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();

            //
            // Seeding for organizations
            //
            Organization benvitec = new Organization { Name = "Benvitec NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.2093.30902", Phone = "011/81.12.34", Website = "http://www.benvitec.be" };
            Organization PCT_NV = new Organization { Name = "PCT NV", Address = new Address { City = "Beringen", Country = "Belgium", Number = "201", Street = "Koolmijnlaan", Zipcode = "3580" }, Vat = "BE 123.9999.99999", Phone = "011/81.12.34", Website = "http://www.PVT-NV.be" };

            var organizations = new Organization[] {
                benvitec,
                PCT_NV
           };

            foreach (Organization organization in organizations)
            {
                context.Organizations.Add(organization);
            }

            //
            // Seeding for users
            //

            if (await userManager.FindByEmailAsync("niek.vandael@gmail.com") == null) {
                var user = new User()
                {
                    UserName = "niekvandael",
                    Email = "niek.vandael@gmail.com"
                };

                await userManager.CreateAsync(user, "P@ssw0rd!");
            }

            context.SaveChanges();
        }
    }
}