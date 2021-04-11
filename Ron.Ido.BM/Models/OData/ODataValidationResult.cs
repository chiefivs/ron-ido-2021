using Ron.Ido.Common.Attributes;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataValidationResult
    {
        public string Field { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
