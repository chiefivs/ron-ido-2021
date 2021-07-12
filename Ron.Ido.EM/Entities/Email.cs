using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(MessageId), IsUnique = true)]
    [Index(nameof(InReplyTo))]
    public class Email
    {
        [Key]
        public long Id { get; set; } 

        [StringLength(300)]
        public string From { get; set; }

        [StringLength(300)]
        public string To { get; set; }

        public int Type { get; set; }   //  EmailTypeEnum

        [StringLength(450)]
        public string MessageId { get; set; }

        [StringLength(450)]
        public string InReplyTo { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public virtual List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}
