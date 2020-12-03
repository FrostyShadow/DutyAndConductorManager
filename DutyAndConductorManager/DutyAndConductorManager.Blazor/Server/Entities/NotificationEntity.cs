using System.Collections.Generic;

namespace DutyAndConductorManager.Blazor.Server.Entities
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }

        public UserEntity User { get; set; }
        public ICollection<NotificationRecipientEntity> NotificationRecipients { get; set; }
    }

    public class NotificationRecipientEntity
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }

        public NotificationEntity Notification { get; set; }
        public UserEntity User { get; set; }
    }
}