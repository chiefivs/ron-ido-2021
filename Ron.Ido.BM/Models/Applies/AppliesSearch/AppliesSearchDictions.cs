using Ron.Ido.BM.Models.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Models.Applies.AppliesSearch
{
    public class AppliesSearchDictions
    {
        public IEnumerable<ODataOption> Statuses { get; set; }
        public IEnumerable<ODataOption> LearnLevels { get; set; }
        public IEnumerable<ODataOption> EntryForms { get; set; }
        public IEnumerable<ODataOption> Stages { get; set; }

    }
}
