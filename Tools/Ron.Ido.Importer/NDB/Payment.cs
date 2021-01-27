using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Payment
    {
        public Payment()
        {
            Applies = new HashSet<Apply>();
            Duplicates = new HashSet<Duplicate>();
        }

        public int Id { get; set; }
        public DateTime BankDate { get; set; }
        public string Payer { get; set; }
        public string PayerInfo { get; set; }
        public double PaySum { get; set; }
        public string PayInfo { get; set; }
        public string DocNumber { get; set; }
        public DateTime? DocDate { get; set; }
        public double? DocSum { get; set; }
        public DateTime? ImpotDate { get; set; }
        public string BankNumber { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
    }
}
