namespace Ron.Ido.BM.Models.Duplicates
{
    public class DuplicatesPageItemDto
    {
        public long Id { get; set; }

        public long DossierId { get; set; }

        public string CreateDate { get; set; }

        public string BarCode { get; set; }

        public string CertificateNum { get; set; }

        public string CreatorFullName { get; set; }

        public string OwnerFullName { get; set; }

        public string Storage { get; set; }

        public string HandoutDate { get; set; }

        public string Status { get; set; }

    }
}
