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

        public string Skills { get; set; }

        public int[] SkillIds { get; set; }

        public string JobBrief { get; set; }

        public string Responsibilities { get; set; }

        public string Requirements { get; set; }

        public string ActionName { get; set; }
    }
}