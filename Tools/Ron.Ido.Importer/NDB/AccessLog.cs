using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class AccessLog
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Login { get; set; }
        public string Event { get; set; }
        public bool Result { get; set; }
    }
}
