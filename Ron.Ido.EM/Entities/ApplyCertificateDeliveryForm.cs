namespace Ron.Ido.EM.Entities
{
    public class ApplyCertificateDeliveryForm
    {
        public long ApplyId { get; set; }
        public virtual Apply Apply { get; set; }
        
        public long DeliveryFormId { get; set; }
        public virtual CertificateDeliveryForm DeliveryForm { get; set; }
    }
}
