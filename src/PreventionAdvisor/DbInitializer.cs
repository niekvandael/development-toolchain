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

            var users = new User[] {
                new User{ Firstname = "Raf", Lastname = "Pellens", Organizations = new Organization[] {benvitec, PCT_NV} },
            };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }


            context.SaveChanges();
        }
    }
}