using Ron.Ido.EM.Enums;

namespace Ron.Ido.BM.Models.Applies.Acceptance
{
    public class AppliesAcceptancePageItemDto
    {
        public string BarCode { get; set; }

        public ApplyEntryFormEnum EntryFormId { get; set; }

        public string CreatorFullName { get; set; }

        public string OwnerFullName { get; set; }

        public string Status { get; set; }
    }
}
