
using Ron.Ido.Common.Attributes;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataFilter
    {
        public string Field { get; set; }
        public string[] Aliases { get; set; }
        public ODataFilterTypeEnum Type { get; set; }
        public string[] Values { get; set; }
    }
}
