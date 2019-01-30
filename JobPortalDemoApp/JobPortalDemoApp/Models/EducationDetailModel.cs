using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalDemoApp.Models
{
    public class EducationDetailModel
    {
       
        [Display(Name = "Highest qualification")]
        public string HighestQualification { get; set; }

        [Required]
        [Display(Name = "Institute / University name")]
        public string InstituteOrUniversityName { get; set; }

        [Display(Name = "Major Branch")]
        public string MajorBranch { get; set; }

        [Display(Name = "Aggregate / Percentage")]
        public decimal Percentage { get; set; }

       
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}