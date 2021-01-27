using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonLetter
    {
        public int Id { get; set; }
        public int? DestinationId { get; set; }
        public int DocxFileId { get; set; }
        public string ApplyRonBarCode { get; set; }
        public int TemplateId { get; set; }
        public string Description { get; set; }

        public virtual ApplyRon ApplyRonBarCodeNavigation { get; set; }
        public virtual ApplyRonLetterDest Destination { get; set; }
        public virtual UploadedFile DocxFile { get; set; }
        public virtual DocxTemplate Template { get; set; }
    }
}
