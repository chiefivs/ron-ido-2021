using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Svid
    {
        public int Id { get; set; }
        public string ApplyBarCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string SvidNum { get; set; }
        public string Xml { get; set; }
        public string CertNumber { get; set; }
        public string CertOwner { get; set; }
        public DateTime? CertFromDate { get; set; }
        public DateTime? CertToDate { get; set; }
        public string FileData { get; set; }
        public string PdfSignature { get; set; }
        public string PdfDataBase64 { get; set; }
        public string PdfHashBase64 { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
    }
}
