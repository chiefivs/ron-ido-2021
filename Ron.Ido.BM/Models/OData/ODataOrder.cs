using Ron.Ido.Common.Attributes;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataOrder
    {
        public string Field { get; set; }
        public ODataOrderTypeEnum Direct { get; set; }
    }
}
