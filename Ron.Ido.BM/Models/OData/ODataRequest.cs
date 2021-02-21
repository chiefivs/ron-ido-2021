using Ron.Ido.Common.Attributes;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<ODataFilter> Filters { get; set; }
        public IEnumerable<ODataOrder> Orders { get; set; }
    }
}
