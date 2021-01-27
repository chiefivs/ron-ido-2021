using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ShedulerTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int Group { get; set; }
        public int Period { get; set; }
        public int StartHour { get; set; }
        public bool? ToMessages { get; set; }
        public bool ToEmail { get; set; }
        public bool ForCreator { get; set; }
        public bool ForManager { get; set; }
        public bool ForExpert { get; set; }
        public string ForRoles { get; set; }
        public string ForAddresses { get; set; }
        public DateTime? LastExec { get; set; }
        public DateTime? NextExec { get; set; }
    }
}
