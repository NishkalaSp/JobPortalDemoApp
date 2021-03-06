﻿using DataLayer.Entities;
using JobPortalDemoApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class RegisterFormModel
    {

        public PersonalDetailModel PersonalDetail { get; set; }

        public EducationDetailModel EducationDetail { get; set; }

        public List<WorkExperienceDetailModel> ExperienceDetails { get; set; }

        public RegisterFormModel()
        {
            ExperienceDetails = new List<WorkExperienceDetailModel>();
        }
    }
}