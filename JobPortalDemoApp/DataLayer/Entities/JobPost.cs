using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class JobPost
    {
        public int Id { get; set; }

        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public DateTime PostedOn { get; set; }

        public int? ModifiedById { get; set; }

        public User ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Title { get; set; }

        public string JobBrief { get; set; }

        public string Responsibilities { get; set; }

        public string Requirements { get; set; }

        public bool IsActive { get; set; }
    }
}
