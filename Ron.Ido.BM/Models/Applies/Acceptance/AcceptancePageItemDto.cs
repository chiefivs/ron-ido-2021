using Ron.Ido.EM.Enums;

namespace Ron.Ido.BM.Models.Applies.Acceptance
{
    public class AcceptancePageItemDto
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
