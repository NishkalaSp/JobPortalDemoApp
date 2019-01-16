using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    class EducationDetails
    {
        public int Id { get; set; }

        public string Education { get; set; }

        public string Type { get; set; }

        public string CollegeOrUniversityName { get; set; }

        public decimal Aggregate { get; set; }

        public string Location { get; set; }
    }
}
