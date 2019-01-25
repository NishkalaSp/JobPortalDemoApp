using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class ExperienceDetails
    {
        public int Id { get; set; }

        public User Seeker { get; set; }

        public string CompanyName { get; set; }

        public string JobTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public JobType Type { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string[] SkillId { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ExperienceDetails()
        {
            Skills = new HashSet<Skill>();
        }
    }
}
