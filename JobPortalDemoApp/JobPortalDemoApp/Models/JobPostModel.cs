using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class JobPostModel
    {
        public string Title { get; set; }

        public string JobBrief { get; set; }

        public string Responsibilities { get; set; }

        public string Requirements { get; set; }
    }
}