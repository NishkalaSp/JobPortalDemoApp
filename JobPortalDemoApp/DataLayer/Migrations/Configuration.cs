namespace DataLayer.Migrations
{
    using DataLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.JPDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.JPDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.JobTypes.AddOrUpdate(j => j.Id,
                new JobType() { Name = "Full-Time"},
                new JobType() { Name = "Correspondence" }
                );
        }
    }
}
