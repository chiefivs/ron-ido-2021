using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class DossierComment //  NDB:ApplyComment
    {
        [Key]
        public long Id { get; set; }

        public long DossierId { get; set; }
        public virtual Dossier Dossier { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }

        public long? UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<DossierCommentAttachment> Attachments { get; set; } = new List<DossierCommentAttachment>();
    }
}
