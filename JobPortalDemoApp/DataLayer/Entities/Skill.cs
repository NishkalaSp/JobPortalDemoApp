using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ExperienceDetails> ExperienceDetails { get; set; }

        public Skill()
        {
            ExperienceDetails = new HashSet<ExperienceDetails>();
        }
    }
}