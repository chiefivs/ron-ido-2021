using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Conclusion
    {
        public Conclusion()
        {
            Applies = new HashSet<Apply>();
            ConclusionAdditionalFields = new HashSet<ConclusionAdditionalField>();
            ConclusionConclusionRights = new HashSet<ConclusionConclusionRight>();
            ConclusionLicenseConfirmTypeConclusions = new HashSet<ConclusionLicenseConfirmTypeConclusion>();
        }

        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string DocumentDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string CreatorPassportReq { get; set; }
        public int? SpecialLearnYearBegin { get; set; }
        public int? SpecialLearnYearEnd { get; set; }
        public string License { get; set; }
        public string Certification { get; set; }
        public string ConclusionText { get; set; }
        public bool? Approve { get; set; }
        public string ReasonComment { get; set; }
        public string Period { get; set; }
        public string Header { get; set; }
        public bool? LearnFormDocument { get; set; }
        public bool? LearnFormDocAttachment { get; set; }
        public bool? LearnFormAnalysis { get; set; }
        public bool? LearnFormHelp { get; set; }
        public string LearnFormOther { get; set; }
        public bool? LicenseHelp { get; set; }
        public bool? LicenseLicenseCopy { get; set; }
        public bool? LicenseDocumentIdo { get; set; }
        public bool? LicenseAttachmentIdo { get; set; }
        public bool? LicensePrecedent { get; set; }
        public bool? LicenseInternet { get; set; }
        public string LicenseUrl { get; set; }
        public bool? CertificationHelp { get; set; }
        public bool? CertificationCopy { get; set; }
        public bool? CertificationDocumentIdo { get; set; }
        public bool? CertificationAttachmentIdo { get; set; }
        public bool? CertificationPrecedent { get; set; }
        public bool? CertificationInternet { get; set; }
        public string CertificationUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Confirm { get; set; }
        public string ProgramVolume { get; set; }
        public string Mark { get; set; }
        public string TotalQualification { get; set; }
        public string DocFullName { get; set; }
        public string LevelName { get; set; }
        public string SourceDocumentType { get; set; }
        public string Legalization { get; set; }
        public string StandardSpeciality { get; set; }
        public string StandardQualification { get; set; }
        public string SpecialityIdoname { get; set; }
        public string QualificationIdoname { get; set; }
        public string DocumentTypeName { get; set; }
        public bool? ConfirmationIsNotRequired { get; set; }
        public bool? CertificateFromEducationalInstitution { get; set; }
        public bool? ConfirmationInternet { get; set; }
        public string ConfirmationUrl { get; set; }
        public int? PeriodMonths { get; set; }
        public int? PeriodYears { get; set; }
        public bool Draft { get; set; }
        public int? BaseLearnDiscMinQty { get; set; }
        public int? BaseLearnDiscFactQty { get; set; }
        public int? SpecLearnVolume { get; set; }
        public int? SpecLearnMeasureWeight { get; set; }
        public int? SpecLearnHours { get; set; }
        public int? SpecLearnDiscQty { get; set; }
        public int? SpecLearnDiscHours { get; set; }
        public int? SpecLearnQtyPercentage { get; set; }
        public string SpecLearnQtySource { get; set; }
        public int? SpecLearnVolumePercentage { get; set; }
        public string SpecLearnVolumeSource { get; set; }
        public string Comment { get; set; }
        public int ExpertizeId { get; set; }
        public int? SchoolId { get; set; }
        public int? LearnFormId { get; set; }
        public int? ConclusionReasonsId { get; set; }
        public int? SpecialityIdoId { get; set; }
        public int? QualificationIdoId { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? QualificationId { get; set; }
        public int? CreatorCitizenshipId { get; set; }
        public int? AimId { get; set; }
        public int? LegalizationTypeId { get; set; }
        public int? LearnLevelId { get; set; }
        public int? LearnLevelMskoId { get; set; }
        public int? StandardDocTypeId { get; set; }
        public int? StandardLearnLevelId { get; set; }
        public int? SourceDocumentTypeForExpertId { get; set; }
        public int? ConfirmTypeId { get; set; }
        public int? LicenseTypeId { get; set; }
        public int? CertificationTypeId { get; set; }
        public int? ConclusionDocTypeId { get; set; }
        public int CreatorId { get; set; }
        public int? BaseLearnIdorightId { get; set; }
        public int? SpecLearnMeasureId { get; set; }
        public bool? ConfirmNotNeed { get; set; }
        public int? SpecLearnDiscQtyStd { get; set; }
        public int? SpecLearnDiscHoursStd { get; set; }
        public int? SchoolCountryId { get; set; }
        public string SchoolName { get; set; }
        public string PassportFullName { get; set; }
        public string ProgramStructure { get; set; }
        public int? StandardSpecialityExId { get; set; }
        public int? StandardQualificationExId { get; set; }
        public string LearnLevelMskotype { get; set; }
        public string StandardUgs { get; set; }
        public int? LicensingVariantId { get; set; }
        public bool? LicensingVariantHelp { get; set; }
        public bool? LicensingVariantCopy { get; set; }
        public bool? LicensingVariantDocumentIdo { get; set; }
        public bool? LicensingVariantAttachmentIdo { get; set; }
        public bool? LicensingVariantPrecedent { get; set; }
        public bool? LicensingVariantInternet { get; set; }
        public string LicensingVariantUrl { get; set; }

        public virtual ApplyAim Aim { get; set; }
        public virtual ConclusionIdoright BaseLearnIdoright { get; set; }
        public virtual ConclusionCertificationType CertificationType { get; set; }
        public virtual ConclusionDocType ConclusionDocType { get; set; }
        public virtual ConclusionReason ConclusionReasons { get; set; }
        public virtual ConclusionConfirmType ConfirmType { get; set; }
        public virtual User Creator { get; set; }
        public virtual Country CreatorCitizenship { get; set; }
        public virtual ApplyDocType DocumentType { get; set; }
        public virtual Expertize Expertize { get; set; }
        public virtual ApplyLearnForm LearnForm { get; set; }
        public virtual LearnLevel LearnLevel { get; set; }
        public virtual LearnLevelMsko LearnLevelMsko { get; set; }
        public virtual Legalization LegalizationType { get; set; }
        public virtual ConclusionLicenseType LicenseType { get; set; }
        public virtual ConclusionLicensingVariant LicensingVariant { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Qualification QualificationIdo { get; set; }
        public virtual School School { get; set; }
        public virtual Country SchoolCountry { get; set; }
        public virtual ApplyDocType SourceDocumentTypeForExpert { get; set; }
        public virtual ConclusionMeasure SpecLearnMeasure { get; set; }
        public virtual Speciality SpecialityIdo { get; set; }
        public virtual StandardDocType StandardDocType { get; set; }
        public virtual LearnLevel StandardLearnLevel { get; set; }
        public virtual StandardQualification StandardQualificationEx { get; set; }
        public virtual StandardSpeciality StandardSpecialityEx { get; set; }
        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<ConclusionAdditionalField> ConclusionAdditionalFields { get; set; }
        public virtual ICollection<ConclusionConclusionRight> ConclusionConclusionRights { get; set; }
        public virtual ICollection<ConclusionLicenseConfirmTypeConclusion> ConclusionLicenseConfirmTypeConclusions { get; set; }
    }
}
