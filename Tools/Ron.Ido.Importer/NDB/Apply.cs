using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Apply
    {
        public Apply()
        {
            ApplyAdditionalFields = new HashSet<ApplyAdditionalField>();
            ApplyComments = new HashSet<ApplyComment>();
            ApplyDocuments = new HashSet<ApplyDocument>();
            ApplyErrors = new HashSet<ApplyError>();
            ApplyStatusHistories = new HashSet<ApplyStatusHistory>();
            Blanks = new HashSet<Blank>();
            CardPayments = new HashSet<CardPayment>();
            Deliveries = new HashSet<Delivery>();
            Duplicates = new HashSet<Duplicate>();
            Expertizes = new HashSet<Expertize>();
            InfoLetters = new HashSet<InfoLetter>();
            Logs = new HashSet<Log>();
            Requests = new HashSet<Request>();
            Svids = new HashSet<Svid>();
        }

        public string BarCode { get; set; }/**/
        public string EpguCode { get; set; }/**/
        public string CreatorFirstName { get; set; }/**/
        public string CreatorLastName { get; set; }/**/
        public string CreatorSurname { get; set; }/**/
        public DateTime? CreatorBirthDate { get; set; }/**/
        public string CreatorPassportType { get; set; }/**/
        public string CreatorPassportReq { get; set; }/**/
        public string CreatorMailIndex { get; set; }/**/
        public string CreatorCityName { get; set; }/**/
        public string CreatorStreet { get; set; }/**/
        public string CreatorCorpus { get; set; }/**/
        public string CreatorBuilding { get; set; }/**/
        public string CreatorBlock { get; set; }/**/
        public string CreatorFlat { get; set; }/**/
        public string CreatorPhone { get; set; }/**/
        public string CreatorEmail { get; set; }/**/
        public bool ByWarrant { get; set; }/**/
        public string WarrantReq { get; set; }/**/
        public DateTime? WarrantDate { get; set; }/**/
        public DateTime? WarrantTerm { get; set; }/**/
        public string OwnerFirstName { get; set; }/**/
        public string OwnerLastName { get; set; }/**/
        public string OwnerSurname { get; set; }/**/
        public DateTime? OwnerBirthDate { get; set; }/**/
        public string OwnerPassportType { get; set; }/**/
        public string OwnerPassportReq { get; set; }/**/
        public string OwnerMailIndex { get; set; }/**/
        public string OwnerCityName { get; set; }/**/
        public string OwnerStreet { get; set; }/**/
        public string OwnerCorpus { get; set; }/**/
        public string OwnerBuilding { get; set; }/**/
        public string OwnerBlock { get; set; }/**/
        public string OwnerFlat { get; set; }/**/
        public string OwnerPhone { get; set; }/**/
        public string DocBlankNum { get; set; }/**/
        public string DocRegNum { get; set; }/**/
        public string DocDate { get; set; }/**/
        public int? DocDateYear { get; set; }/**/
        public string DocFullName { get; set; }/**/
        public DateTime? BaseLearnDateBegin { get; set; }/**/
        public DateTime? BaseLearnDateEnd { get; set; }/**/
        public DateTime? SpecialLearnDateBegin { get; set; }/**/
        public DateTime? SpecialLearnDateEnd { get; set; }/**/
        public bool DocsWillSendByPost { get; set; }/**/
        public bool TransmitOpenChannels { get; set; }/**/
        public DateTime? CreateDate { get; set; }/**/
        public DateTime? AcceptDate { get; set; }/**/
        public DateTime? PostDate { get; set; }
        public DateTime? PostDateFact { get; set; }
        public DateTime? GetDate { get; set; }
        public DateTime? SignatureDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentNumber { get; set; }
        public string Storage { get; set; }/**/
        public DateTime? DateFirstPrint { get; set; }
        public DateTime? DateLastPrint { get; set; }
        public bool Deleted { get; set; }/**/
        public int StatusId { get; set; }/**/
        public DateTime? StatusChangeTime { get; set; }/**/
        public string SvidNum { get; set; }
        public DateTime? SvidDate { get; set; }
        public int? DocAttachment { get; set; }/**/
        public int? CreatorId { get; set; }
        public int? OwnerCitizenshipId { get; set; }/**/
        public int? CreatorCitizenshipId { get; set; }/**/
        public int? SchoolId { get; set; }
        public int? DocTypeId { get; set; }/**/
        public int? SpecialLearnFormId { get; set; }/**/
        public int? AimId { get; set; }/**/
        public int? EntryFormId { get; set; }/**/
        public int? FixedLearnSpecialityId { get; set; }
        public int? CreatorCountryId { get; set; }/**/
        public int? OwnerCountryId { get; set; }/**/
        public int? StatusChangeUserId { get; set; }
        public int? StatusExpertizeId { get; set; }
        public int? PaymentId { get; set; }
        public int? DocCountryId { get; set; }/**/
        public bool? ConvertedFromOldDb { get; set; }
        public string CurrentBarCode { get; set; }
        public int? ConclusionResultId { get; set; }
        public int? ConclusionResultUserId { get; set; }
        public string Other { get; set; }
        public int? EpguStatus { get; set; }/**/
        public int? StatusFromOldDb { get; set; }
        public int? SchoolCountryId { get; set; }/**/
        public int? SchoolTypeId { get; set; }/**/
        public string SchoolCityName { get; set; }/**/
        public string SchoolName { get; set; }/**/
        public string SchoolPostIndex { get; set; }/**/
        public string SchoolAddress { get; set; }/**/
        public string SchoolPhone { get; set; }/**/
        public string SchoolFax { get; set; }/**/
        public string SchoolEmail { get; set; }/**/
        public int? ActId { get; set; }
        public string LastBlankNumber { get; set; }
        public bool IsArchive { get; set; }
        public string OrgCreator { get; set; }/**/
        public int? DeliveryFormId { get; set; }/**/
        public bool IsMedicalSpeciality { get; set; }
        public bool IsNovorossia { get; set; }/**/
        public bool IsRostovFilial { get; set; }/**/
        public bool ForInfoLetter { get; set; }/**/
        public DateTime? MinTermByEtap { get; set; }
        public DateTime? MaxTermByEtap { get; set; }
        public Guid? ForTest { get; set; }
        public int? LastConclusionId { get; set; }
        public string FixedLearnSpecialityName { get; set; }/**/
        public int? DocFullPackageTypeId { get; set; }
        public string Uin { get; set; }
        public DateTime? UinCreateTime { get; set; }
        public DateTime? UinRemoveTime { get; set; }
        public Guid? UinCreateSmevGuid { get; set; }
        public Guid? UinRemoveSmevGuid { get; set; }
        public string EpguSmev3Code { get; set; }
        public bool ForOferta { get; set; }/**/
        public bool IsEnglish { get; set; }/**/
        public int? ReturnOriginalsFormId { get; set; }/**/
        public string ReturnOriginalsPostAddress { get; set; }
        public bool DigSvidDeliveryByEmail { get; set; }/**/
        public bool DigSvidDeliveryByPortal { get; set; }/**/
        public bool DigSvidDeliveryByEpgu { get; set; }/**/
        public int? CreatorPassportKindTypeId { get; set; }
        public int? OwnerPassportKindTypeId { get; set; }

        public virtual Act Act { get; set; }
        public virtual ApplyAim Aim { get; set; }/**/
        public virtual ConclusionResult ConclusionResult { get; set; }
        public virtual User ConclusionResultUser { get; set; }
        public virtual ApplyCreator Creator { get; set; }
        public virtual Country CreatorCitizenship { get; set; }/**/
        public virtual Country CreatorCountry { get; set; }/**/
        public virtual ApplyDeliveryForm DeliveryForm { get; set; }/**/
        public virtual Country DocCountry { get; set; }/**/
        public virtual ApplyDocFullPackageType DocFullPackageType { get; set; }
        public virtual ApplyDocType DocType { get; set; }/**/
        public virtual ApplyEntryForm EntryForm { get; set; }/**/
        public virtual Speciality FixedLearnSpeciality { get; set; }
        public virtual Conclusion LastConclusion { get; set; }
        public virtual Country OwnerCitizenship { get; set; }/**/
        public virtual Country OwnerCountry { get; set; }/**/
        public virtual Payment Payment { get; set; }
        public virtual ApplyDeliveryForm ReturnOriginalsForm { get; set; }/**/
        public virtual ApplyPassportType CreatorPassportKindType { get; set; }/**/
        public virtual ApplyPassportType OwnerPassportKindType { get; set; }/**/
        public virtual School School { get; set; }
        public virtual Country SchoolCountry { get; set; }/**/
        public virtual SchoolType SchoolType { get; set; }/**/
        public virtual ApplyLearnForm SpecialLearnForm { get; set; }/**/
        public virtual ApplyStatus Status { get; set; }
        public virtual User StatusChangeUser { get; set; }
        public virtual ExpertizeStatus StatusExpertize { get; set; }
        public virtual ICollection<ApplyAdditionalField> ApplyAdditionalFields { get; set; }
        public virtual ICollection<ApplyComment> ApplyComments { get; set; }
        public virtual ICollection<ApplyDocument> ApplyDocuments { get; set; }
        public virtual ICollection<ApplyError> ApplyErrors { get; set; }
        public virtual ICollection<ApplyStatusHistory> ApplyStatusHistories { get; set; }
        public virtual ICollection<Blank> Blanks { get; set; }
        public virtual ICollection<CardPayment> CardPayments { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
        public virtual ICollection<Expertize> Expertizes { get; set; }
        public virtual ICollection<InfoLetter> InfoLetters { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Svid> Svids { get; set; }
    }
}
