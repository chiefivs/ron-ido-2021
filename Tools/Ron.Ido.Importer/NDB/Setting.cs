using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Setting
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int OrderNum { get; set; }
        public string ValueEng { get; set; }
    }
}
