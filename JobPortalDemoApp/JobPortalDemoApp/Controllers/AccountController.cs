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
using System.Web.Security;

namespace JobPortalDemoApp.Controllers
{
    public class AccountController : Controller
    {
        private JPDbContext _context;

        private UserService _userService;

        public UserService UserService { get { return _userService ?? (_userService = new UserService());  } }

        public AccountController()
        {
            _context = new JPDbContext();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = UserService.GetUserByEmail(login.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Email provided is incorrect");
                    return View(login);
                }

                if (user.Password != login.Password)
                {
                    ModelState.AddModelError("", "Password provided is incorrect");
                    return View(login);
                }

                FormsAuthentication.SetAuthCookie(user.Email, false);
                var hrUserType = _context.Role.First(r => r.Type == "HR"); 
                if (user.Role == hrUserType)
                {
                    return RedirectToAction("Search", "Job");
                }

                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        public ActionResult Register1()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View("Register1");
        }

        [HttpPost]
        public ActionResult Register(RegisterFormModel rfm)
        {
            if (ModelState.IsValid)
            {
                if (!IsResumeFileExtensionValid(rfm.PersonalDetail.ResumeFile))
                {
                    ModelState.AddModelError("ResumeFile", "File with extension .txt, .doc, .docx or .pdf is allowed.");
                    return View(rfm);
                }

                
                //save to db
                SaveUser(rfm);
                return RedirectToAction("Login", "Account");
            }
           
            return View("Register1", rfm);
        }

        private void SaveUser(RegisterFormModel rfm)
        {
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + 
                                    rfm.PersonalDetail.FirstName + 
                                    rfm.PersonalDetail.LastName + 
                                    System.IO.Path.GetExtension(rfm.PersonalDetail.ResumeFile.FileName);//datetime needed?

            var user = new User() {
                FirstName = rfm.PersonalDetail.FirstName,
                LastName = rfm.PersonalDetail.LastName,
                Password = rfm.PersonalDetail.Password, //must encrypt
                Email = rfm.PersonalDetail.Email,
                ContactNumber = rfm.PersonalDetail.ContactNumber,
                Role = _context.Role.Single( ut => ut.Type == "JobSeeker"),
                ResumeFileName = fileName,
                DOB = rfm.PersonalDetail.DOB,
                CreatedDate = DateTime.Now
            };

            _context.User.Add(user);

            SaveFile(rfm.PersonalDetail.ResumeFile, fileName);

            foreach (var ed in rfm.ExperienceDetails)
            {
                var experienceDetail = new ExperienceDetails() {
                    Seeker = user,
                    CompanyName = ed.CompanyName,
                    JobTitle = ed.JobTitle,
                    StartDate = ed.StartDate,
                    EndDate = ed.EndDate,
                    Type = ed.Type
                };
                foreach (var skill in ed.SkillId)
                {
                    experienceDetail.Skills.Add(new Skill() { Id = Convert.ToInt32(skill) });
                }
                _context.ExperienceDetails.Add(experienceDetail);
            }

            var eduDetail = new EducationDetails()
            {
                Seeker = user,
                HighestQualification = rfm.EducationDetail.HighestQualification,
                InstituteOrUniversityName = rfm.EducationDetail.InstituteOrUniversityName,
                MajorBranch = rfm.EducationDetail.MajorBranch,
                Percentage = rfm.EducationDetail.Percentage,
                Type = rfm.EducationDetail.Type
            };
            _context.EducationDetails.Add(eduDetail);

            _context.SaveChanges();
        }

        private bool IsResumeFileExtensionValid(HttpPostedFileBase resumeFile)
        {
            var allowedExtensions = new string[]{ ".txt", ".doc", ".docx", ".pdf"};
            var fileExtension = System.IO.Path.GetExtension(resumeFile.FileName);
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }
            return true;
        }

        private void SaveFile(HttpPostedFileBase resumeFile, string fileName) //void or bool?
        {
            var uploadUrl = Server.MapPath("~/Content/Resumes");
            resumeFile.SaveAs(Path.Combine(uploadUrl, fileName));
            return;
        }

        public ActionResult GetWorkExperienceView(int count)
        {
            ViewBag.Count = count;
            ViewBag.SkillList = _context.Skills.ToList();
            return PartialView("_WorkExpDetailView");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        
    }
}