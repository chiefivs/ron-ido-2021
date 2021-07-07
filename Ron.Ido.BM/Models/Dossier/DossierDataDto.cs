namespace Ron.Ido.BM.Models.Dossier
{
    public class DossierDataDto
    {
        public ApplyData Apply { get; set; }
        public DuplicateData Duplicate { get; set; }
    }

    public class ApplyData
    {
        public long Id { get; set; }

        public string BarCode { get; set; }

        public string CreateTime { get; set; }
    }

    public class DuplicateData
    {
        public long Id { get; set; }

        public string BarCode { get; set; }

        public string CreateTime { get; set; }
    }
}
