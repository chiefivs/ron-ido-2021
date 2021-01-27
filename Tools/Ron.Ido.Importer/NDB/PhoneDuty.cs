using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class PhoneDuty
    {
        public int Id { get; set; }
        public DateTime DutyDate { get; set; }
        public int DutyUserId { get; set; }
        public int DutyManagerId { get; set; }
        public DateTime AcceptDate { get; set; }
        public bool? IsActual { get; set; }
        public int? CancelManagerId { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelComments { get; set; }

        public virtual User CancelManager { get; set; }
        public virtual User DutyManager { get; set; }
        public virtual User DutyUser { get; set; }
    }
}
