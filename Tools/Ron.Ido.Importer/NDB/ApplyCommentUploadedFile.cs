using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyCommentUploadedFile
    {
        public int ApplyCommentUploadedFileUploadedFileId { get; set; }
        public int FilesId { get; set; }

        public virtual ApplyComment ApplyCommentUploadedFileUploadedFile { get; set; }
        public virtual UploadedFile Files { get; set; }
    }
}
