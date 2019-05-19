using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public virtual Conductor CreatorConductor { get; set; }
        public string NotificationBody { get; set; }
        public virtual ICollection<Conductor> RecipientsCollection { get; set; }
    }
}