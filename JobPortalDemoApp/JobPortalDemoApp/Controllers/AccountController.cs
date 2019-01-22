using DataLayer;
using DataLayer.Entities;
using JobPortalDemoApp.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Register(RegisterFormModel rfm, string[] companyName, string[] jobTitle)
        {
            if (ModelState.IsValid)
            {
                //save to db
                return View("");
            }

            return View();
        }

        //public ActionResult GetWorkExperienceView()
        //{
        //    return PartialView("_WorkExpDetailView");
        //}

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