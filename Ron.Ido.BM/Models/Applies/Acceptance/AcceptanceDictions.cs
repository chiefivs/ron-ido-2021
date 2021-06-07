using Ron.Ido.BM.Models.OData;
using System.Collections.Generic;

namespace Ron.Ido.BM.Models.Applies.Acceptance
{
    public class AcceptanceDictions
    {
        public IEnumerable<ODataOption> Statuses { get; set; }
        public IEnumerable<ODataOption> EducationLevels { get; set; }
        public IEnumerable<ODataOption> ApplyEntryForm { get; set; }


    }
}
