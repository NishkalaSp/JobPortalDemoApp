using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class JobPostModel
    {
        public int JobPostId { get; set; }

        [Required]
        public string Title { get; set; }

        public List<int> SkillIds { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        [Display(Name = "Type")]
        public int JobTypeId { get; set; }

        public IEnumerable<JobType> JobTypes { get; set; }

        [Required]
        public string JobBrief { get; set; }

        [Required]
        public string Responsibilities { get; set; }

        [Required]
        public string Requirements { get; set; }

        public string ActionName { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Posted By")]
        public string CreatedUserEmail { get; set; }

        [Display(Name = "Posted On")]
        public DateTime? PostedOn { get; set; }

        [Display(Name = "Type")]
        public string JobType { get; set; }

        public JobPostModel()
        {
            SkillIds = new List<int>();
        }
    }
}