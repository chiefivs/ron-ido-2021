using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class AutoComplete
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string TableName { get; set; }
    }
}
