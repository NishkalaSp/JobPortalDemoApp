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
            var jobPostModel = new JobPostModel();
            jobPostModel.ActionName = "AddJobPost";
            ViewBag.SkillList = _context.Skills.ToList();
            return View("JobPost", jobPostModel);
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
                    Requirements = jp.Requirements,
                };

                _context.JobPost.Add(jobPost);

                foreach (var skillId in jp.SkillIds)
                {
                    var jobSkill = new JobSkill
                    {
                        JobPostId = jobPost.Id,
                        SkillId = Convert.ToInt32(skillId)
                    };
                    _context.JobSkill.Add(jobSkill);
                }

                //create notification
                var notification = new Notification
                {
                    Type = NotificationType.Add,
                    JobPostId = jobPost.Id
                };
                _context.Notification.Add(notification);

                //notify all users
                var seekerRoleId = _context.Role.Where(r => r.Type == "JobSeeker").FirstOrDefault().Id;
                var jobSeekersId = _context.User.Include(u => u.Role).Where(u => u.Role.Id == seekerRoleId).Select(u => u.Id).ToList();

                foreach (var seekerId in jobSeekersId)
                {
                    var un = new UserNotification
                    {
                        NotificationId = notification.Id,
                        UserId = seekerId,
                        IsRead = false
                    };
                    _context.UserNotification.Add(un);
                }

                _context.SaveChanges();
                return RedirectToAction("JobsListing", "Job");
            }
            return View("JobPost", jp);
        }

        public ActionResult EditJobPost(int jobPostId)
        {
            var jobPost = _context.JobPost.Where(jp => jp.Id == jobPostId).FirstOrDefault();

            var jobPostModel = new JobPostModel() {
                JobPostId = jobPost.Id,
                Title = jobPost.Title,
                SkillIds = _context.JobSkill.Where(js => js.JobPostId == jobPost.Id).Select(js => js.SkillId).ToArray(),
                JobBrief = jobPost.JobBrief,
                Requirements = jobPost.Requirements,
                Responsibilities = jobPost.Responsibilities,
                ActionName = "EditJobPost"
            };

            ViewBag.SkillList = _context.Skills.ToList();
            return View("JobPost", jobPostModel);
        }

        [HttpPost]
        public ActionResult EditJobPost(JobPostModel jpm)
        {
            if (ModelState.IsValid)
            {
                var jobPost = _context.JobPost.Where(jp => jp.Id == jpm.JobPostId).FirstOrDefault();

                jobPost.ModifiedById = UserService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
                jobPost.ModifiedOn = DateTime.Now;
                jobPost.Title = jpm.Title;
                jobPost.JobBrief = jpm.JobBrief;
                jobPost.Responsibilities = jpm.Responsibilities;
                jobPost.Requirements = jpm.Requirements;

                //foreach (var skillId in jpm.SkillIds)
                //{
                //    var jobSkill = new JobSkill
                //    {
                //        JobPostId = jobPost.Id,
                //        SkillId = Convert.ToInt32(skillId)
                //    };
                //    _context.JobSkill.Add(jobSkill);
                //}

                _context.SaveChanges();
                return RedirectToAction("JobsListing", "Job");
            }
            return View("JobPost", jpm);
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
            var jobPost = _context.JobPost.Where(jp => jp.Id == jobId).FirstOrDefault();
            if (jobPost == null)
            {
                //return 
            }

            var skills = _context.JobSkill.Include(js => js.Skill).Where(js => js.JobPostId == jobPost.Id).Select(js => js.Skill.Name).ToList();
            var jobPostModel = new JobPostModel {
                JobPostId = jobPost.Id,
                Title = jobPost.Title,
                Skills = string.Join(", ", skills),
                JobBrief = jobPost.JobBrief,
                Responsibilities = jobPost.Responsibilities,
                Requirements = jobPost.Requirements
            };

            return View(jobPostModel);
        }

        public ActionResult GetCandidatesGridData(int jobId)
        {
            var candidates = _context.JobPostActivity.Include(jpa => jpa.Seeker).Where(jpa => jpa.JobPostId == jobId)
                                        .Select(jpa => new { AppliedOn = jpa.AppliedOn, Seeker = jpa.Seeker })
                                        .ToList();
            var data = candidates.Select(c => new {
                Id = c.Seeker.Id,
                Email = c.Seeker.Email,
                ContactNumber = c.Seeker.ContactNumber,
                AppliedOn = c.AppliedOn.ToString("MMM d yyyy"),
                Skills = string.Join(",", _context.UserExperienceSkill.Include(ues => ues.Skill)
                                                      .Where(ues => ues.SeekerId == c.Seeker.Id)
                                                      .Select(ues => ues.Skill.Name)
                                                      .Distinct()
                                                      .ToList())
            });

            return Json(new { data = data, recordsTotal = data.Count() }, JsonRequestBehavior.AllowGet);
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
            var eduDetail = _context.EducationDetails.Include(ed => ed.EducationType).Where(ed => ed.Seeker.Id == seekerId).FirstOrDefault();
            var expDetails = _context.ExperienceDetails.Where(exd => exd.Seeker.Id == seekerId).ToList();

            var userDetail = new UserDetail();
            userDetail.PersonalDetail.FirstName = user.FirstName;
            userDetail.PersonalDetail.LastName = user.LastName;
            userDetail.PersonalDetail.Email = user.Email;
            userDetail.PersonalDetail.ContactNumber = user.ContactNumber;

            if (eduDetail != null)
            {
                userDetail.EducationDetail.HighestQualification = eduDetail.HighestQualification;
                userDetail.EducationDetail.InstituteOrUniversityName = eduDetail.InstituteOrUniversityName;
                userDetail.EducationDetail.MajorBranch = eduDetail.MajorBranch;
                userDetail.EducationDetail.Percentage = eduDetail.Percentage;
                userDetail.EducationDetail.Type = eduDetail.EducationType.Name;
            }
            

            foreach (var exd in expDetails)
            {
                var experienceDetailModel = new WorkExperienceDetailModel()
                {
                    CompanyName = exd.CompanyName,
                    JobTitle = exd.JobTitle,
                    StartDate = exd.StartDate,
                    EndDate = exd.EndDate,
                    Type = exd.Type,
                    Skills = string.Join(",", _context.UserExperienceSkill.Include(ues => ues.Skill)
                                                      .Where(ues => ues.SeekerId == user.Id 
                                                      && ues.ExperienceDetailId == exd.Id ).Select(ues => ues.Skill.Name).ToList()) //?
                };
                userDetail.ExperienceDetails.Add(experienceDetailModel);
            }

            return View(userDetail);
        }

        

        private List<User> GetJobSeekers(string searchString)
        {
            var roleId = _context.Role.First(ut => ut.Type.Equals("JobSeeker")).Id;
            return _context.User.Where(u => u.Role.Id == roleId
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