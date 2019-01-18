using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
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
                ContactNumber = js.ContactNumber
            });

            return Json(new { data = data, recordsTotal = data.Count() } , JsonRequestBehavior.AllowGet);
        }

        private List<User> GetJobSeekers(string searchString)
        {
            var seekerUserType = _context.UserTypes.First(ut => ut.Type.Equals("JobSeeker"));
            return _context.User.Where(u => u.Type.Id == seekerUserType.Id 
                                            && (u.FirstName.Contains(searchString)
                                            || u.LastName.Contains(searchString))).ToList();
        }
    }
}