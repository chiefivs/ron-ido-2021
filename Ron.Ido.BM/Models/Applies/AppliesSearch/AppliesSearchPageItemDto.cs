using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Models.Applies.AppliesSearch
{
    public class AppliesSearchPageItemDto
    {
        public long Id { get; set; }

        public long DossierId { get; set; }

        public string BarCode { get; set; }

        public string CreateDate { get; set; }

        public ApplyEntryFormEnum EntryFormId { get; set; }

        public string CreatorFullName { get; set; }

        public string OwnerFullName { get; set; }

        public string Status { get; set; }

    }
}
