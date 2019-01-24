using DataLayer;
using DataLayer.Entities;
using JobPortalDemoApp.Models;
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
                var user = GetUserByEmail(login.Email);

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
                var hrUserType = _context.UserTypes.First(ut => ut.Type == "HR"); 
                if (user.Type == hrUserType)
                {
                    return RedirectToAction("Search", "Job");
                }

                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterFormModel rfm)
        {
            if (ModelState.IsValid)
            {
                if (!IsResumeFileExtensionValid(rfm.ResumeFile))
                {
                    ModelState.AddModelError("ResumeFile", "File with extension .txt, .doc, .docx or .pdf is allowed.");
                    return View(rfm);
                }

                SaveFile(rfm.ResumeFile, rfm.Name);
                //save to db
                SaveUser(rfm);
                return RedirectToAction("Index", "Home");
            }
            SaveUser(rfm);
            return View(rfm);
        }

        private void SaveUser(RegisterFormModel rfm)
        {
            _context.ExperienceDetails.AddRange(rfm.ExperienceDetails);
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

        private void SaveFile(HttpPostedFileBase resumeFile, string name) //void or bool?
        {
            var fileExtension = System.IO.Path.GetExtension(resumeFile.FileName);
            var uploadUrl = Server.MapPath("~/Content/Resumes");
            var fileName = DateTime.Now.ToString("yyyymmddMMss") + name + fileExtension;
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

        private User GetUserByEmail(string email)
        {
            return _context.User.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}