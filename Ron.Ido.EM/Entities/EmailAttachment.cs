namespace Ron.Ido.EM.Entities
{
    public class EmailAttachment
    {
        public long EmailId { get; set; }

        public long FileInfoId { get; set; }

        public virtual FileInfo FileInfo { get; set; }
    }
}
