using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ChiefRoomFrame
    {
        public int Id { get; set; }
        public int ReportingId { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int UpdateTime { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public int OrderNum { get; set; }

        public virtual Reporting Reporting { get; set; }
        public virtual User User { get; set; }
    }
}
