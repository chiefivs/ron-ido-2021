using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class UploadedFileRequest
    {
        public int AttachmentsId { get; set; }
        public int UploadedFileRequestUploadedFileId { get; set; }

        public virtual UploadedFile Attachments { get; set; }
        public virtual Request UploadedFileRequestUploadedFile { get; set; }
    }
}
