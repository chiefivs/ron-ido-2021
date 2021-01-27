using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionOutputAttach
    {
        public int CorrOutputId { get; set; }
        public int FileId { get; set; }

        public virtual CorrespondentionOutput CorrOutput { get; set; }
        public virtual UploadedFile File { get; set; }
    }
}
