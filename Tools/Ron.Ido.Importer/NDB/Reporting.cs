using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Reporting
    {
        public Reporting()
        {
            ChiefRoomFrames = new HashSet<ChiefRoomFrame>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid? Guid { get; set; }
        public int Type { get; set; }
        public int DefaultUpdateTime { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ChiefRoomFrame> ChiefRoomFrames { get; set; }
    }
}
