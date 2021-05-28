using System;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class ApplyAttachment
    {
        [Key]
        public long Id { get; set; }

        public long ApplyId { get; set; }
        public virtual Apply Apply { get; set; }

        public bool Required { get; set; }

        public bool Given { get; set; }

        public string Description { get; set; }

        public string Error { get; set; }

        public long? AttachmentTypeId { get; set; }
        public virtual ApplyAttachmentType Type { get; set; }

        public Guid? FileInfoUid { get; set; }
        public virtual FileInfo FileInfo { get; set; }

    }
}
