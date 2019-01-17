﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    class User
    {
        public int Id { get; set; }

        public UserType Type { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class UserType
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
