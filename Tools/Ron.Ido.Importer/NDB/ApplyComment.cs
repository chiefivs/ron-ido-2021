using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyComment
    {
        public ApplyComment()
        {
            ApplyCommentUploadedFiles = new HashSet<ApplyCommentUploadedFile>();
        }

        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ApplyApplyCommentCommentBarCode { get; set; }
        public int? UserId { get; set; }

        public virtual Apply ApplyApplyCommentCommentBarCodeNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ApplyCommentUploadedFile> ApplyCommentUploadedFiles { get; set; }
    }
}
