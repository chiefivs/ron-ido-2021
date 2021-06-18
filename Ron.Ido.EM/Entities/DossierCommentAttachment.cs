using System;

namespace Ron.Ido.EM.Entities
{
    public class DossierCommentAttachment
    {
        public long CommentId { get; set; }
        public virtual DossierComment Comment { get; set; }

        public Guid FileInfoUid { get; set; }
        public virtual FileInfo FileInfo { get; set; }
    }
}
