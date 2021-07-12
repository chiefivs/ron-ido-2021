using System;

namespace Ron.Ido.EM.Entities
{
    public class EmailAttachment
    {
        public long EmailId { get; set; }

        public Guid FileInfoUid { get; set; }

        public virtual FileInfo FileInfo { get; set; }
    }
}
