using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class JPDbContext : DbContext
    {
        public JPDbContext()
            : base(ConfigurationManager.ConnectionStrings["JPDbConnectionString"].ConnectionString)
        {
                
        }

        public DbSet<JobType> JobTypes { get; set; }

        public DbSet<EducationType> EducationTypes { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<ExperienceDetails> ExperienceDetails { get; set; }

        public DbSet<EducationDetails> EducationDetails { get; set; }

        public DbSet<Skill> Skills { get; set; }
    }
}
