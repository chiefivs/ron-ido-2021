using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int ForUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ReadsOn { get; set; }

        public virtual User ForUser { get; set; }
    }
}
