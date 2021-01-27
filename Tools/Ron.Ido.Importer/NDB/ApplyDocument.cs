using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyDocument
    {
        public int Id { get; set; }
        public bool Required { get; set; }
        public bool Given { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public int? DocumentTypeId { get; set; }
        public string ApplyBarCode { get; set; }
        public int? DocumentFileId { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual UploadedFile DocumentFile { get; set; }
        public virtual ApplyDocumentType DocumentType { get; set; }
    }
}
