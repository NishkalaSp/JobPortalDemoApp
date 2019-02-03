using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public NotificationType Type { get; set; }

        public int JobPostId { get; set; }

        public JobPost JobPost { get; set; }
    }

    public enum NotificationType
    {
        Add = 1,
        Delete = 2
    }
}
