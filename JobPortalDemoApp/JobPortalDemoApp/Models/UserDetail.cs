using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class UserDetail
    {
        public PersonalDetailModel PersonalDetail { get; set; }

        public EducationDetailModel EducationDetail { get; set; }

        public List<WorkExperienceDetailModel> ExperienceDetails { get; set; }

        public UserDetail()
        {
            PersonalDetail = new PersonalDetailModel();
            EducationDetail = new EducationDetailModel();
            ExperienceDetails = new List<WorkExperienceDetailModel>();
        }
    }
}