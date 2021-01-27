using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Duplicate
    {
        public Duplicate()
        {
            Blanks = new HashSet<Blank>();
            CardPayments = new HashSet<CardPayment>();
            DuplicateStatusHistories = new HashSet<DuplicateStatusHistory>();
        }

        public string BarCode { get; set; }
        public string ApplyBarCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? HandoutDate { get; set; }
        public string Storage { get; set; }
        public string FullName { get; set; }
        public int? PaymentId { get; set; }
        public int? CreatedById { get; set; }
        public int? HandoutById { get; set; }
        public string Address { get; set; }
        public string Phones { get; set; }
        public string Email { get; set; }
        public int? NotificationMethod { get; set; }
        public int? DeliveryMethod { get; set; }
        public int? ActId { get; set; }
        public bool DeletedFromAct { get; set; }
        public string DocFullName { get; set; }
        public string SchoolName { get; set; }
        public string DocCountry { get; set; }
        public string Note { get; set; }
        public string MailIndex { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string Flat { get; set; }
        public string Corpus { get; set; }
        public string Building { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDate { get; set; }
        public int StatusId { get; set; }
        public string LastBlankNumber { get; set; }
        public int? DocCountryId { get; set; }
        public int? CreatorCountryId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Uin { get; set; }
        public DateTime? UinCreateTime { get; set; }
        public DateTime? UinRemoveTime { get; set; }
        public Guid? UinCreateSmevGuid { get; set; }
        public Guid? UinRemoveSmevGuid { get; set; }
        public bool IsEnglish { get; set; }
        public int? ReturnOriginalsFormId { get; set; }
        public string ReturnOriginalsPostAddress { get; set; }
        public bool DigSvidDeliveryByEmail { get; set; }
        public bool DigSvidDeliveryByPortal { get; set; }
        public bool DigSvidDeliveryByEpgu { get; set; }

        public virtual Act Act { get; set; }
        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Country CreatorCountry { get; set; }
        public virtual Country DocCountryNavigation { get; set; }
        public virtual ApplyDocType DocumentTypeNavigation { get; set; }
        public virtual User HandoutBy { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ApplyDeliveryForm ReturnOriginalsForm { get; set; }
        public virtual DuplicateStatus Status { get; set; }
        public virtual ICollection<Blank> Blanks { get; set; }
        public virtual ICollection<CardPayment> CardPayments { get; set; }
        public virtual ICollection<DuplicateStatusHistory> DuplicateStatusHistories { get; set; }
    }
}
