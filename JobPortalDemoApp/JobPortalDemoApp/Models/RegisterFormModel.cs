using DataLayer.Entities;
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
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[Display(Name = "Contact Number")]
      
        public string ContactNumber { get; set; }

        [Required]
        public string Password { get; set; }

        //[Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "It does not match with password.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Date of Birth")]
        //[DataType(dataType: DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MaxDate(ErrorMessage = "DOB cannot be greater than today's date.")]
        public DateTime DOB { get; set; }

        [Required]
        public HttpPostedFileBase ResumeFile { get; set; }

        [Required]
        [Display(Name = "Highest qualification")]
        public string HighestQualification { get; set; }

        [Required]
        [Display(Name = "Institute / University name")]
        public string InstituteOrUniversityName { get; set; }

        [Display(Name = "Major Branch")]
        public string MajorBranch { get; set; }

        [Display(Name = "Aggregate / Percentage")]
        public decimal Percentage { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        public List<ExperienceDetails> ExperienceDetails { get; set; }

        public RegisterFormModel()
        {
            ExperienceDetails = new List<ExperienceDetails>();
        }
    }
}