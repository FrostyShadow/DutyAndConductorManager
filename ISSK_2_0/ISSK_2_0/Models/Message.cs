using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }        
        public int SenderId { get; set; }
        public virtual Conductor SenderConductor { get; set; }
        public string MessageBody { get; set; }
        public DateTime CreationDateTime { get; set; }
        public int? ParentMessageId { get; set; }
        public virtual Message ParentMessage { get; set; }
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
    }

    public class MessageRecipient
    {
        [Key, Column(Order = 0)]
        public int RecipientId { get; set; }
        public virtual Conductor RecipientConductor { get; set; }
        [Key, Column(Order = 1)]
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public bool IsRead { get; set; }
    }
}