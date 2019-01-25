using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class WorkExperienceDetailModel
    {
        public string CompanyName { get; set; }

        public string JobTitle { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public JobType Type { get; set; }

        public int[] Skills { get; set; }
    }

    public enum JobType
    {
        FullTime,
        Contract
    }
}