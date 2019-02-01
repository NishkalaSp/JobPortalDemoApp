using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class JobSkill
    {
        [Key]
        [Column(Order = 1)]
        public int JobPostId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SkillId { get; set; }

        public JobPost JobPost { get; set; }

        public Skill Skill { get; set; }
    }
}
