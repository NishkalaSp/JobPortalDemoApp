using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JobPortalDemoApp.Service
{
    public class UserService
    {
        private JPDbContext _context;

        public UserService()
        {
            _context = new JPDbContext();
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.Include(u => u.Role)
                            .Where(u => u.Email.Equals(email))
                            .FirstOrDefault();
        }

        public bool IsHRUser(string name)
        {
            var user = _context.User.Include("Role").Where(u => u.Email.Equals(name)).Single();
            return user.Role.Type.Equals("HR");
        }
    }
}