namespace Ron.Ido.EM.Entities
{
    public class DuplicateCertificateDeliveryForm
    {
        public long DuplicateId { get; set; }
        public virtual Duplicate Duplicate { get; set; }

        public long DeliveryFormId { get; set; }
        public virtual CertificateDeliveryForm DeliveryForm { get; set; }
    }
}
