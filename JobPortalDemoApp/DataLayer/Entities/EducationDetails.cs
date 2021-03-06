﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class EducationDetails
    {
        public int Id { get; set; }

        public User Seeker { get; set; }

        public string HighestQualification { get; set; }

        public string InstituteOrUniversityName { get; set; }

        public string MajorBranch { get; set; }

        public decimal Percentage { get; set; }

        public int EducationTypeId { get; set; }

        public EducationType EducationType { get; set; }

        

        

        //public string State { get; set; }

        //public string City { get; set; }
    }

    public class EducationType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    
}
