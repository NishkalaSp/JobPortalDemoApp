using DataLayer;
using DataLayer.Entities;
using JobPortalDemoApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalDemoApp.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private JPDbContext _context;

        public JobController()
        {
            _context = new JPDbContext();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult GetGridData(string searchString)
        {
            var data = GetJobSeekers(searchString).Select(js => new {
                Id = js.Id,
                FullName = js.FirstName + " " + js.LastName,
                Email = js.Email,
                ContactNumber = js.ContactNumber,
                DownloadUrl = Url.Action("DownloadResume", "Job", new { seekerId = js.Id }),
                DetailsUrl = Url.Action("Details", "Job", new { seekerId = js.Id })
            });

            return Json(new { data = data, recordsTotal = data.Count() } , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int seekerId)
        {
            //var userDetail = from u in _context.User
            //                 join e in _context.EducationDetails on u.Id equals e.Seeker.Id
            //                 select new UserDetail()
            //                 {
            //                     PersonalDetail = new PersonalDetailModel()
            //                     {
            //                         FirstName = u.FirstName,
            //                         LastName = u.LastName,
            //                         Email = u.Email,
            //                         ContactNumber = u.ContactNumber
            //                     },
            //                     EducationDetail = new EducationDetailModel()
            //                     {
            //                         HighestQualification = e.HighestQualification,
            //                         InstituteOrUniversityName = e.InstituteOrUniversityName,
            //                         MajorBranch = e.MajorBranch,
            //                         Percentage = e.Percentage,
            //                         Type = e.Type
            //                     }
                                 
            //                 };
            

            var user = GetUserById(seekerId); //if user == null?
            var eduDetail = _context.EducationDetails.Where(ed => ed.Seeker.Id == seekerId).SingleOrDefault();
            var expDetails = _context.ExperienceDetails.Where(exd => exd.Seeker.Id == seekerId).ToList();

            var userDetail = new UserDetail();
            userDetail.PersonalDetail.FirstName = user.FirstName;



            foreach (var exd in expDetails)
            {
                var experienceDetailModel = new WorkExperienceDetailModel()
                {
                    CompanyName = exd.CompanyName,
                    JobTitle = exd.JobTitle,
                    StartDate = exd.StartDate,
                    EndDate = exd.EndDate,
                    Type = exd.Type,
                    Skills = string.Join(",", exd.Skills.Select(s => s.Name).ToList())
                };
                
            }

            return View(userDetail);
        }

        

        private List<User> GetJobSeekers(string searchString)
        {
            var seekerUserType = _context.UserTypes.First(ut => ut.Type.Equals("JobSeeker"));
            return _context.User.Where(u => u.Type.Id == seekerUserType.Id 
                                            && (u.FirstName.Contains(searchString)
                                            || u.LastName.Contains(searchString))).ToList();
        }


        public FileResult DownloadResume(int seekerId)
        {
            var filename = _context.User.Where(u => u.Id == seekerId).Select(u => u.ResumeFileName).Single(); //filename == null?

            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Content/Resumes"), filename));

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        private User GetUserById(int seekerId)
        {
            return _context.User.Where(u => u.Id == seekerId).FirstOrDefault();
        }
    }
}