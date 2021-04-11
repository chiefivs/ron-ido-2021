using Ron.Ido.Common.Attributes;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataForm<TDto>
    {
        public TDto Item { get; set; }

        public Dictionary<string, IEnumerable<ODataOption>> Options { get; set; }
    }
}
