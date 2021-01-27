using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Delivery
    {
        public int Id { get; set; }
        public string CurrentBarCode { get; set; }
        public string OutNumber { get; set; }
        public DateTime? OutDate { get; set; }
        public bool IsAddress { get; set; }
        public string FullAddress { get; set; }
        public string ConfirmName { get; set; }
        public string OutputName { get; set; }
        public Guid CreatorUnityKey { get; set; }

        public virtual Apply CurrentBarCodeNavigation { get; set; }
    }
}
