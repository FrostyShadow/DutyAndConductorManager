using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int CreatorId { get; set; }
        public virtual Conductor CreatorConductor { get; set; }
        public string NotificationBody { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; }
    }

    public class NotificationRecipient
    {
        [Key, Column(Order = 0)]
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }
        [Key, Column(Order = 1)]
        public int ConductorId { get; set; }
        public virtual Conductor Conductor { get; set; }
    }
}