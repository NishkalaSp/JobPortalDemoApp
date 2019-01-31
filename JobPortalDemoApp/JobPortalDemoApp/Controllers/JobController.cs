using DataLayer;
using DataLayer.Entities;
using JobPortalDemoApp.Models;
using JobPortalDemoApp.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace JobPortalDemoApp.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private JPDbContext _context;
        private UserService _userService;

        public UserService UserService { get { return _userService ?? (_userService = new UserService()); } }

        public JobController()
        {
            _context = new JPDbContext();
        }

        public ActionResult CandidatesListing()
        {
            return View();
        }

        public ActionResult AddJobPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddJobPost(JobPostModel jp)
        {
            if (ModelState.IsValid)
            {
                var jobPost = new JobPost()
                {
                    CreatedById = UserService.GetUserByEmail(HttpContext.User.Identity.Name).Id,
                    PostedOn = DateTime.Now,
                    IsActive = true,
                    Title = jp.Title,
                    JobBrief = jp.JobBrief,
                    Responsibilities = jp.Responsibilities,
                    Requirements = jp.Requirements
                };

                _context.JobPost.Add(jobPost);
                _context.SaveChanges();
                return RedirectToAction("JobsListing", "Job");
            }
            return View(jp);
        }

        public ActionResult JobsListing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetJobsPostedGridData(string search)
        {
            var allJobs = _context.JobPost.Include(j => j.CreatedBy).Select(jp => new
            {
                Id = jp.Id,
                Title = jp.Title,
                PostedBy = jp.CreatedBy.Email,
                PostedOn = jp.PostedOn,
                IsActive = jp.IsActive
            }).OrderByDescending(j => j.PostedOn).ToList();

            var data = allJobs.Select( jp=> new {
                Id = jp.Id,
                Title = jp.Title,
                PostedBy = jp.PostedBy,
                PostedOn = jp.PostedOn.ToString("MMM d yyyy"),
                IsActive = jp.IsActive,
                DetailsUrl = Url.Action("JobDetails", "Job", new { jobId = jp.Id })
            });

            return Json(new { data = data, recordsTotal = allJobs.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JobDetails(int jobId)
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
            var expDetails = _context.ExperienceDetails.Include("Skills").Where(exd => exd.Seeker.Id == seekerId).ToList();

            var userDetail = new UserDetail();
            userDetail.PersonalDetail.FirstName = user.FirstName;
            userDetail.PersonalDetail.LastName = user.LastName;
            userDetail.PersonalDetail.Email = user.Email;
            userDetail.PersonalDetail.ContactNumber = user.ContactNumber;

            userDetail.EducationDetail.HighestQualification = eduDetail.HighestQualification;
            userDetail.EducationDetail.InstituteOrUniversityName = eduDetail.InstituteOrUniversityName;
            userDetail.EducationDetail.MajorBranch = eduDetail.MajorBranch;
            userDetail.EducationDetail.Percentage = eduDetail.Percentage;
            userDetail.EducationDetail.Type = eduDetail.Type;

            foreach (var exd in expDetails)
            {
                var experienceDetailModel = new WorkExperienceDetailModel()
                {
                    CompanyName = exd.CompanyName,
                    JobTitle = exd.JobTitle,
                    StartDate = exd.StartDate,
                    EndDate = exd.EndDate,
                    Type = exd.Type,
                    Skills = string.Join(",", exd.Skills.Select(s => s.Name).ToList()) //?
                };
                userDetail.ExperienceDetails.Add(experienceDetailModel);
            }

            return View(userDetail);
        }

        

        private List<User> GetJobSeekers(string searchString)
        {
            var seekerUserType = _context.Role.First(ut => ut.Type.Equals("JobSeeker"));
            return _context.User.Where(u => u.Role.Id == seekerUserType.Id 
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