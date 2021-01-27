using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CardPayment
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string ApplyBarCode { get; set; }
        public string DuplicateBarCode { get; set; }
        public DateTime Time { get; set; }
        public string Bank { get; set; }
        public string OrderId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int? OrderStatus { get; set; }
        public string OrderStatusDesc { get; set; }
        public int? Amount { get; set; }
        public string Pan { get; set; }
        public string Expiration { get; set; }
        public string CardholderName { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual Duplicate DuplicateBarCodeNavigation { get; set; }
    }
}
