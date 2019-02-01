using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace JobPortalDemoApp.Controllers
{
    public class NController : Controller
    {
        private JPDbContext _context;

        public NController()
        {
            _context = new JPDbContext();
        }

        // GET: N
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAllUserNotifications()
        {
            var username = HttpContext.User.Identity.Name;
            var user = _context.User.Where(u => u.Email == username).FirstOrDefault();

            var notifications = _context.UserNotification.Include(un => un.Notification).Where(un => un.UserId == user.Id).ToList();

            var data = notifications.Select(n => new {
                NotificationType = n.Notification.Type
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}