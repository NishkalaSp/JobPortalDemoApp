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

        public DbSet<Role> Role { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<ExperienceDetails> ExperienceDetails { get; set; }

        public DbSet<EducationDetails> EducationDetails { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<UserExperienceSkill> UserExperienceSkill { get; set; }

        public DbSet<JobPost> JobPost { get; set; }

        public DbSet<JobPostActivity> JobPostActivity { get; set; }

        public DbSet<JobSkill> JobSkill { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public DbSet<UserNotification> UserNotification { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobPostActivity>().HasRequired(jpa => jpa.Seeker).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>().HasRequired(un => un.User).WithMany().WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
