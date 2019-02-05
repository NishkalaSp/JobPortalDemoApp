using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class UserExperienceSkill
    {
        [Key]
        [Column(Order = 1)]
        public int SeekerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ExperienceDetailId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int SkillId { get; set; }

        public User Seeker { get; set; }

        public ExperienceDetails ExperienceDetail { get; set; }

        public Skill Skill { get; set; }
    }
}
