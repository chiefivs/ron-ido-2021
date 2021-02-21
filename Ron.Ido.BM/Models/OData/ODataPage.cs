using Ron.Ido.Common.Attributes;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataPage<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Size { get; set; }
    }
}
