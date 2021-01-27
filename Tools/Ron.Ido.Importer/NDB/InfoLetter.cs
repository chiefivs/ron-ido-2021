using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class InfoLetter
    {
        public int Id { get; set; }
        public string OutNum { get; set; }
        public DateTime? OutDate { get; set; }
        public string SignerName { get; set; }
        public string SignerPosition { get; set; }
        public int TemplateId { get; set; }
        public string SchoolName { get; set; }
        public string Agreement { get; set; }
        public string ApplyBarCode { get; set; }
        public string DocxFileName { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual DocxTemplate Template { get; set; }
    }
}
