using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class UserNotification
    {
        [Column(Order = 1)]
        public int Id { get; set; }

  
        [Column(Order = 2)]
        public int UserId { get; set; }

        public User User { get; set; }

    
        [Column(Order = 3)]
        public int NotificationId { get; set; }

        public Notification Notification { get; set; }

        public bool IsRead { get; set; }
    }
}
