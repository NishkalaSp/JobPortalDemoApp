using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ContactNumber { get; set; }

        public DateTime DOB { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ResumeFileName { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
