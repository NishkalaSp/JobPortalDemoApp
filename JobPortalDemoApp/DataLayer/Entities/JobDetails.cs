using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    class JobDetails
    {
        public int Id { get; set; }

        public JobType Type { get; set; }

        public int FromYearsOfExperience { get; set; }

        public int ToYearsOfExperience { get; set; }

        public string Qualification { get; set; }

        public string Designation { get; set; }


    }
}
