using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ReceptionQueue
    {
        public ReceptionQueue()
        {
            ReceptionQueueBarCodes = new HashSet<ReceptionQueueBarCode>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string TimeInterval { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<ReceptionQueueBarCode> ReceptionQueueBarCodes { get; set; }
    }
}
