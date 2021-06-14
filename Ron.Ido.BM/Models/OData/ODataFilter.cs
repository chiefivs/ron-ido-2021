
using Ron.Ido.Common.Attributes;
using Ron.Ido.Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataFilter
    {
        public string Field { get; set; }
        public string[] Aliases { get; set; }
        public ODataFilterTypeEnum Type { get; set; }
        public string[] Values { get; set; }

        public long[] GetIds()
        {
            return Values.Select(v => v.Parse((long)0)).Where(v => v > 0).ToArray();
        }
    }
}
