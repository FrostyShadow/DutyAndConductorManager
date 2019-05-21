using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ISSK_2_0.Models
{
    public class CustomMembershipUser : MembershipUser
    {

        #region Conductor Properites
        public int ConductorId { get; set; }
        public int Code { get; set; }
        public new string Email { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public bool IsActive { get; set; }
        public string ActivationCode { get; set; }
        public ConductorData ConductorData { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<BrigadeConductor> BrigadeConductors { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<NotificationRecipient> NotificationRecipients { get; set; }
        public ICollection<MessageRecipient> MessageRecipients { get; set; }
        #endregion


        public CustomMembershipUser(Conductor conductor) : base("CustomMembership", conductor.Email,
            conductor.ConductorId, conductor.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now,
            DateTime.Now, DateTime.Now, DateTime.Now)
        {
            ConductorId = conductor.ConductorId;
            Email = conductor.Email;
            LastActiveDateTime = conductor.LastActiveDateTime;
            Code = conductor.Code;
            IsActive = conductor.IsActive;
            ActivationCode = conductor.ActivationCode;
            ConductorData = conductor.ConductorData;
            Roles = conductor.Roles;
            Messages = conductor.Messages;
            BrigadeConductors = conductor.BrigadeConductors;
            Notifications = conductor.Notifications;
            NotificationRecipients = conductor.NotificationRecipients;
            MessageRecipients = conductor.MessageRecipients;
        }
    }
}