namespace JobHunt.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobHunt.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobHunt.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Companies.AddOrUpdate(
                c => c.Name,
               new Company { Name = "Atlas", City = "Bentonville", State = "AR", Address1 = "1211 SE 28th St", Address2 = "Ste 11", Zipcode = 72712, Url = "www.atlasdsr.com" },
               new Company { Name = "Walmart" }
            );

            context.SaveChanges();

            var company = context.Companies.FirstOrDefault(x => x.Name == "Atlas");
            context.Leads.AddOrUpdate(
               l => l.Title,
               new Lead { Title = "Programmer", Company = company, DateAdded = DateTime.Now, Deadline = DateTime.Now.AddDays(6) },
               new Lead { Title = "Front-End Developer", Company = company, DateAdded = DateTime.Now, Deadline = DateTime.Now.AddDays(4) }
             );
            
            
        }
    }
}
