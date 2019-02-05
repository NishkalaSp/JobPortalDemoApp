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

            var userNotifications = _context.UserNotification.Include(un => un.Notification.JobPost).Where(un => un.UserId == user.Id && un.IsRead == false).ToList();

            var data = userNotifications.Select(n => new {
                Id = n.Id,
                NotificationType = n.Notification.Type,
                PostedOn = n.Notification.JobPost.PostedOn.ToString("MMM d yyyy"),
                JobTitle = n.Notification.JobPost.Title
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MakeNotificationsAsRead(int[] notificationIds)
        {
            var username = HttpContext.User.Identity.Name;
            var user = _context.User.Where(u => u.Email == username).FirstOrDefault();

            var userNotifications = _context.UserNotification.Where(un => un.UserId == user.Id && un.IsRead == false).ToList();

            foreach (var notification in userNotifications)
            {
                foreach (var nId in notificationIds)
                {
                    if (nId == notification.Id)
                    {
                        notification.IsRead = true;
                    }
                }
            }
            _context.SaveChanges();

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}