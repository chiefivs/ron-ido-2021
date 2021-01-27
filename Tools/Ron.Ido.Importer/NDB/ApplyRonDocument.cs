using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonDocument
    {
        public int Id { get; set; }
        public bool Required { get; set; }
        public bool Given { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public int? DocumentTypeId { get; set; }
        public string ApplyRonBarCode { get; set; }
        public int? DocumentFileId { get; set; }

        public virtual ApplyRon ApplyRonBarCodeNavigation { get; set; }
        public virtual UploadedFile DocumentFile { get; set; }
        public virtual ApplyDocumentType DocumentType { get; set; }
    }
}
