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
    class JPDbContext : DbContext
    {
        public JPDbContext()
            : base(ConfigurationManager.ConnectionStrings["JPDbConnectionString"].ConnectionString)
        {
                
        }

        public DbSet<JobType> JobTypes { get; set; }
    }
}
