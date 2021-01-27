using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionInputAttach
    {
        public int CorrInputId { get; set; }
        public int FileId { get; set; }

        public virtual CorrespondentionInput CorrInput { get; set; }
        public virtual UploadedFile File { get; set; }
    }
}
