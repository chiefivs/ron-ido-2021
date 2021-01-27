using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class NostrificationRONContext : DbContext
    {
        public NostrificationRONContext()
        {
        }

        public NostrificationRONContext(DbContextOptions<NostrificationRONContext> options)
            : base(options)
        {
        }

        public NostrificationRONContext(string conn) : base(_createOptions(conn))
        {

        }

        private static DbContextOptions<NostrificationRONContext> _createOptions(string conn)
        {
            var builder = new DbContextOptionsBuilder<NostrificationRONContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(conn, opt => { });

            return builder.Options;
        }

        public virtual DbSet<AccessLog> AccessLogs { get; set; }
        public virtual DbSet<Act> Acts { get; set; }
        public virtual DbSet<ActConfirmResult> ActConfirmResults { get; set; }
        public virtual DbSet<ActStatus> ActStatuses { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<AgreementApplyDocType> AgreementApplyDocTypes { get; set; }
        public virtual DbSet<AgreementEduDoc> AgreementEduDocs { get; set; }
        public virtual DbSet<AgreementMapCountryValue> AgreementMapCountryValues { get; set; }
        public virtual DbSet<AgreementVersion> AgreementVersions { get; set; }
        public virtual DbSet<Apostille> Apostilles { get; set; }
        public virtual DbSet<ApostilleReason> ApostilleReasons { get; set; }
        public virtual DbSet<AppliesApplyAimsApplyDocType> AppliesApplyAimsApplyDocTypes { get; set; }
        public virtual DbSet<Apply> Applies { get; set; }
        public virtual DbSet<ApplyAdditionalField> ApplyAdditionalFields { get; set; }
        public virtual DbSet<ApplyAim> ApplyAims { get; set; }
        public virtual DbSet<ApplyComment> ApplyComments { get; set; }
        public virtual DbSet<ApplyCommentUploadedFile> ApplyCommentUploadedFiles { get; set; }
        public virtual DbSet<ApplyCreator> ApplyCreators { get; set; }
        public virtual DbSet<ApplyCreatorTicket> ApplyCreatorTickets { get; set; }
        public virtual DbSet<ApplyDeliveryForm> ApplyDeliveryForms { get; set; }
        public virtual DbSet<ApplyDocFullPackageType> ApplyDocFullPackageTypes { get; set; }
        public virtual DbSet<ApplyDocType> ApplyDocTypes { get; set; }
        public virtual DbSet<ApplyDocument> ApplyDocuments { get; set; }
        public virtual DbSet<ApplyDocumentType> ApplyDocumentTypes { get; set; }
        public virtual DbSet<ApplyEntryForm> ApplyEntryForms { get; set; }
        public virtual DbSet<ApplyError> ApplyErrors { get; set; }
        public virtual DbSet<ApplyField> ApplyFields { get; set; }
        public virtual DbSet<ApplyHandoutView> ApplyHandoutViews { get; set; }
        public virtual DbSet<ApplyLearnForm> ApplyLearnForms { get; set; }
        public virtual DbSet<ApplyRecipientView> ApplyRecipientViews { get; set; }
        public virtual DbSet<ApplyRon> ApplyRons { get; set; }
        public virtual DbSet<ApplyRonAppeal> ApplyRonAppeals { get; set; }
        public virtual DbSet<ApplyRonDocument> ApplyRonDocuments { get; set; }
        public virtual DbSet<ApplyRonLetter> ApplyRonLetters { get; set; }
        public virtual DbSet<ApplyRonLetterDest> ApplyRonLetterDests { get; set; }
        public virtual DbSet<ApplyRonStatus> ApplyRonStatuses { get; set; }
        public virtual DbSet<ApplyRonStatusHistory> ApplyRonStatusHistories { get; set; }
        public virtual DbSet<ApplyStatus> ApplyStatuses { get; set; }
        public virtual DbSet<ApplyStatusHistory> ApplyStatusHistories { get; set; }
        public virtual DbSet<ApplyTemplate> ApplyTemplates { get; set; }
        public virtual DbSet<ApplyTemplateField> ApplyTemplateFields { get; set; }
        public virtual DbSet<AutoComplete> AutoCompletes { get; set; }
        public virtual DbSet<B20121121ApplyStatus> B20121121ApplyStatuses { get; set; }
        public virtual DbSet<B20121121ApplyStatusHistory> B20121121ApplyStatusHistories { get; set; }
        public virtual DbSet<B20121121ExpertizeStatus> B20121121ExpertizeStatuses { get; set; }
        public virtual DbSet<BackUp130311SchoolsAddedExpert> BackUp130311SchoolsAddedExperts { get; set; }
        public virtual DbSet<Blank> Blanks { get; set; }
        public virtual DbSet<CardPayment> CardPayments { get; set; }
        public virtual DbSet<ChiefRoomFrame> ChiefRoomFrames { get; set; }
        public virtual DbSet<Conclusion> Conclusions { get; set; }
        public virtual DbSet<ConclusionAdditionalField> ConclusionAdditionalFields { get; set; }
        public virtual DbSet<ConclusionCertificationType> ConclusionCertificationTypes { get; set; }
        public virtual DbSet<ConclusionConclusionRight> ConclusionConclusionRights { get; set; }
        public virtual DbSet<ConclusionConfirmType> ConclusionConfirmTypes { get; set; }
        public virtual DbSet<ConclusionDocType> ConclusionDocTypes { get; set; }
        public virtual DbSet<ConclusionField> ConclusionFields { get; set; }
        public virtual DbSet<ConclusionIdoright> ConclusionIdorights { get; set; }
        public virtual DbSet<ConclusionLicenseConfirmType> ConclusionLicenseConfirmTypes { get; set; }
        public virtual DbSet<ConclusionLicenseConfirmTypeConclusion> ConclusionLicenseConfirmTypeConclusions { get; set; }
        public virtual DbSet<ConclusionLicenseType> ConclusionLicenseTypes { get; set; }
        public virtual DbSet<ConclusionLicensingVariant> ConclusionLicensingVariants { get; set; }
        public virtual DbSet<ConclusionMeasure> ConclusionMeasures { get; set; }
        public virtual DbSet<ConclusionReason> ConclusionReasons { get; set; }
        public virtual DbSet<ConclusionResult> ConclusionResults { get; set; }
        public virtual DbSet<ConclusionRight> ConclusionRights { get; set; }
        public virtual DbSet<ConclusionTemplate> ConclusionTemplates { get; set; }
        public virtual DbSet<ConclusionTemplateField> ConclusionTemplateFields { get; set; }
        public virtual DbSet<ConfirmedSchoolDoc> ConfirmedSchoolDocs { get; set; }
        public virtual DbSet<ConfirmedSchoolName> ConfirmedSchoolNames { get; set; }
        public virtual DbSet<ConfirmedSchoolOrder> ConfirmedSchoolOrders { get; set; }
        public virtual DbSet<CorrespondentionInput> CorrespondentionInputs { get; set; }
        public virtual DbSet<CorrespondentionInputAttach> CorrespondentionInputAttachs { get; set; }
        public virtual DbSet<CorrespondentionInputBarCode> CorrespondentionInputBarCodes { get; set; }
        public virtual DbSet<CorrespondentionInputDeliveryType> CorrespondentionInputDeliveryTypes { get; set; }
        public virtual DbSet<CorrespondentionInputType> CorrespondentionInputTypes { get; set; }
        public virtual DbSet<CorrespondentionOutput> CorrespondentionOutputs { get; set; }
        public virtual DbSet<CorrespondentionOutputAttach> CorrespondentionOutputAttachs { get; set; }
        public virtual DbSet<CorrespondentionOutputBarCode> CorrespondentionOutputBarCodes { get; set; }
        public virtual DbSet<CorrespondentionOutputStatus> CorrespondentionOutputStatuses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustomObjectsId> CustomObjectsIds { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DocxTemplate> DocxTemplates { get; set; }
        public virtual DbSet<Duplicate> Duplicates { get; set; }
        public virtual DbSet<DuplicateStatus> DuplicateStatuses { get; set; }
        public virtual DbSet<DuplicateStatusHistory> DuplicateStatusHistories { get; set; }
        public virtual DbSet<EiisCountry> EiisCountries { get; set; }
        public virtual DbSet<EiisPackage> EiisPackages { get; set; }
        public virtual DbSet<EiisPackagePart> EiisPackageParts { get; set; }
        public virtual DbSet<EiisSession> EiisSessions { get; set; }
        public virtual DbSet<EiisUinEvent> EiisUinEvents { get; set; }
        public virtual DbSet<EnglishResource> EnglishResources { get; set; }
        public virtual DbSet<EpguEvent> EpguEvents { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<ExpertDepartment> ExpertDepartments { get; set; }
        public virtual DbSet<ExpertOrganization> ExpertOrganizations { get; set; }
        public virtual DbSet<Expertize> Expertizes { get; set; }
        public virtual DbSet<ExpertizeStatus> ExpertizeStatuses { get; set; }
        public virtual DbSet<ExpertizeStatusHistory> ExpertizeStatusHistories { get; set; }
        public virtual DbSet<FisReception> FisReceptions { get; set; }
        public virtual DbSet<Foundation> Foundations { get; set; }
        public virtual DbSet<InfoLetter> InfoLetters { get; set; }
        public virtual DbSet<LearnCourse> LearnCourses { get; set; }
        public virtual DbSet<LearnLevel> LearnLevels { get; set; }
        public virtual DbSet<LearnLevelMsko> LearnLevelMskos { get; set; }
        public virtual DbSet<LearnProgram> LearnPrograms { get; set; }
        public virtual DbSet<LearnProgramDetail> LearnProgramDetails { get; set; }
        public virtual DbSet<LegalAct> LegalActs { get; set; }
        public virtual DbSet<LegalActType> LegalActTypes { get; set; }
        public virtual DbSet<LegalActVersion> LegalActVersions { get; set; }
        public virtual DbSet<Legalization> Legalizations { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MigrConfirm> MigrConfirms { get; set; }
        public virtual DbSet<NostrificationV2Expertize> NostrificationV2Expertizes { get; set; }
        public virtual DbSet<NostrificationV2Request> NostrificationV2Requests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PhoneDuty> PhoneDuties { get; set; }
        public virtual DbSet<PreferredPage> PreferredPages { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Ratification> Ratifications { get; set; }
        public virtual DbSet<ReLinkSchoolApply> ReLinkSchoolApplies { get; set; }
        public virtual DbSet<ReLinkSchoolConclusion> ReLinkSchoolConclusions { get; set; }
        public virtual DbSet<ReLinkSchoolSpeciality> ReLinkSchoolSpecialities { get; set; }
        public virtual DbSet<ReLinkSchoolToDelete> ReLinkSchoolToDeletes { get; set; }
        public virtual DbSet<ReceptionQueue> ReceptionQueues { get; set; }
        public virtual DbSet<ReceptionQueueBarCode> ReceptionQueueBarCodes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ReglamentEtap> ReglamentEtaps { get; set; }
        public virtual DbSet<Reporting> Reportings { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<RequestTemplate> RequestTemplates { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SchoolsSpeciality> SchoolsSpecialities { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<ShedulerTask> ShedulerTasks { get; set; }
        public virtual DbSet<Smev3Job> Smev3Jobs { get; set; }
        public virtual DbSet<SpecialitiesLearnProgram> SpecialitiesLearnPrograms { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<StandardDocType> StandardDocTypes { get; set; }
        public virtual DbSet<StandardLearnProgram> StandardLearnPrograms { get; set; }
        public virtual DbSet<StandardLearnProgramDetail> StandardLearnProgramDetails { get; set; }
        public virtual DbSet<StandardQualification> StandardQualifications { get; set; }
        public virtual DbSet<StandardSpecialitiesCategory> StandardSpecialitiesCategories { get; set; }
        public virtual DbSet<StandardSpecialitiesGroup> StandardSpecialitiesGroups { get; set; }
        public virtual DbSet<StandardSpeciality> StandardSpecialities { get; set; }
        public virtual DbSet<StandardSpecialityStandardLearnProgram> StandardSpecialityStandardLearnPrograms { get; set; }
        public virtual DbSet<Svid> Svids { get; set; }
        public virtual DbSet<TempApplyCreator> TempApplyCreators { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TmpSchool> TmpSchools { get; set; }
        public virtual DbSet<UploadedFile> UploadedFiles { get; set; }
        public virtual DbSet<UploadedFileRequest> UploadedFileRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AccessLog>(entity =>
            {
                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Act>(entity =>
            {
                entity.Property(e => e.ConfirmResultId).HasColumnName("ConfirmResult_Id");

                entity.Property(e => e.Coordination).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DigSvidCreatorId).HasColumnName("DigSvidCreator_Id");

                entity.Property(e => e.GiveToRonDate).HasColumnType("datetime");

                entity.Property(e => e.InputDate).HasColumnType("datetime");

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.Property(e => e.OutputDate).HasColumnType("datetime");

                entity.Property(e => e.PreCoordinationDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectNumber).HasMaxLength(50);

                entity.Property(e => e.ProjectSignDate).HasColumnType("datetime");

                entity.Property(e => e.Sight).HasMaxLength(100);

                entity.Property(e => e.SignDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.HasOne(d => d.ConfirmResult)
                    .WithMany(p => p.Acts)
                    .HasForeignKey(d => d.ConfirmResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acts_ActConfirmResults");

                entity.HasOne(d => d.DigSvidCreator)
                    .WithMany(p => p.Acts)
                    .HasForeignKey(d => d.DigSvidCreatorId)
                    .HasConstraintName("FK_Acts_DigSvidCreator");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Acts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acts_ActStatuses");
            });

            modelBuilder.Entity<ActConfirmResult>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ActStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Agreement>(entity =>
            {
                entity.HasIndex(e => e.IdokbId, "IX_Agreements_IdokbId");

                entity.HasIndex(e => e.IdokbRev, "IX_Agreements_IdokbRev");

                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.RejectDate).HasColumnType("datetime");

                entity.Property(e => e.SignatureDate).HasColumnType("datetime");

                entity.Property(e => e.Site).HasMaxLength(500);

                entity.Property(e => e.ValidDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AgreementApplyDocType>(entity =>
            {
                entity.HasKey(e => new { e.AgreementsId, e.DocTypesId })
                    .IsClustered(false);

                entity.ToTable("AgreementApplyDocType");

                entity.HasIndex(e => e.DocTypesId, "IX_FK_AgreementApplyDocType_ApplyDocType");

                entity.Property(e => e.AgreementsId).HasColumnName("Agreements_Id");

                entity.Property(e => e.DocTypesId).HasColumnName("DocTypes_Id");

                entity.HasOne(d => d.Agreements)
                    .WithMany(p => p.AgreementApplyDocTypes)
                    .HasForeignKey(d => d.AgreementsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgreementApplyDocType_Agreement");

                entity.HasOne(d => d.DocTypes)
                    .WithMany(p => p.AgreementApplyDocTypes)
                    .HasForeignKey(d => d.DocTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgreementApplyDocType_ApplyDocType");
            });

            modelBuilder.Entity<AgreementEduDoc>(entity =>
            {
                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.EduLevel).HasMaxLength(200);

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.AgreementEduDocs)
                    .HasForeignKey(d => d.AgreementId)
                    .HasConstraintName("FK_AgreementEduDocs_Agreements");
            });

            modelBuilder.Entity<AgreementMapCountryValue>(entity =>
            {
                entity.HasKey(e => e.CountryCode);

                entity.Property(e => e.CountryCode).HasMaxLength(2);
            });

            modelBuilder.Entity<AgreementVersion>(entity =>
            {
                entity.HasIndex(e => e.AgreementId, "IX_FK_AgreementAgreementVersion");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.Remark).IsRequired();

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.ValidDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.AgreementVersions)
                    .HasForeignKey(d => d.AgreementId)
                    .HasConstraintName("FK_AgreementAgreementVersion");
            });

            modelBuilder.Entity<Apostille>(entity =>
            {
                entity.HasKey(e => e.IsgaCode);

                entity.Property(e => e.IsgaCode).HasMaxLength(50);

                entity.Property(e => e.CountryIsgaCode).HasMaxLength(50);

                entity.Property(e => e.CountryOutIsgaCode).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EduLevelIsgaCode).HasMaxLength(50);

                entity.Property(e => e.ReasonIsgaCode).HasMaxLength(50);

                entity.Property(e => e.ReasonText).HasMaxLength(500);
            });

            modelBuilder.Entity<ApostilleReason>(entity =>
            {
                entity.HasKey(e => e.IsgaCode);

                entity.Property(e => e.IsgaCode).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<AppliesApplyAimsApplyDocType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Applies_ApplyAims_ApplyDocTypes");

                entity.Property(e => e.BarCode).HasMaxLength(12);

                entity.Property(e => e.Cday)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CDay")
                    .IsFixedLength(true);

                entity.Property(e => e.Cmonth)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CMonth")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(767)
                    .HasColumnName("creatorName");

                entity.Property(e => e.CresultName).HasColumnName("CResult_name");

                entity.Property(e => e.Cyear).HasColumnName("CYear");

                entity.Property(e => e.DocName).HasMaxLength(200);

                entity.Property(e => e.EformsName)
                    .HasMaxLength(50)
                    .HasColumnName("EForms_name");

                entity.Property(e => e.Handout).HasMaxLength(100);

                entity.Property(e => e.IdoCountry).HasMaxLength(100);

                entity.Property(e => e.LformName)
                    .HasMaxLength(50)
                    .HasColumnName("LForm_name");

                entity.Property(e => e.Llevel)
                    .HasMaxLength(100)
                    .HasColumnName("LLevel");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OwnerCountry).HasMaxLength(100);

                entity.Property(e => e.Recipient).HasMaxLength(100);

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .HasColumnName("Status_name");

                entity.Property(e => e.Storage).HasMaxLength(100);
            });

            modelBuilder.Entity<Apply>(entity =>
            {
                entity.HasKey(e => e.BarCode);

                entity.HasIndex(e => e.EpguCode, "IX_Applies_EpguCode");

                entity.HasIndex(e => e.EpguSmev3Code, "IX_Applies_EpguSmev3Code");

                entity.HasIndex(e => e.StatusChangeTime, "IX_Applies_StatusChangeTime");

                entity.HasIndex(e => e.SvidDate, "IX_Applies_SvidDate");

                entity.HasIndex(e => e.SvidNum, "IX_Applies_SvidNum");

                entity.HasIndex(e => e.CreatorPassportType, "IX_CreatorPassportType");

                entity.HasIndex(e => e.AimId, "IX_FK_ApplyAimApply");

                entity.HasIndex(e => e.CreatorId, "IX_FK_ApplyApplyCreator");

                entity.HasIndex(e => e.DocTypeId, "IX_FK_ApplyDocTypeApply");

                entity.HasIndex(e => e.EntryFormId, "IX_FK_ApplyEntryFormApply");

                entity.HasIndex(e => e.StatusExpertizeId, "IX_FK_ApplyExpertizeStatus");

                entity.HasIndex(e => e.SpecialLearnFormId, "IX_FK_ApplyLearnFormApply");

                entity.HasIndex(e => e.FixedLearnSpecialityId, "IX_FK_ApplySpeciality");

                entity.HasIndex(e => e.StatusId, "IX_FK_ApplyStatusApply");

                entity.HasIndex(e => e.CreatorCitizenshipId, "IX_FK_CountryApply");

                entity.HasIndex(e => e.OwnerCitizenshipId, "IX_FK_CountryApply1");

                entity.HasIndex(e => e.CreatorCountryId, "IX_FK_CountryApply2");

                entity.HasIndex(e => e.OwnerCountryId, "IX_FK_CountryApply3");

                entity.HasIndex(e => e.DocCountryId, "IX_FK_CountryApply4");

                entity.HasIndex(e => e.PaymentId, "IX_FK_PaymentApply");

                entity.HasIndex(e => e.SchoolId, "IX_FK_SchoolApply");

                entity.HasIndex(e => e.StatusChangeUserId, "IX_FK_UserApply");

                entity.HasIndex(e => e.CurrentBarCode, "PK_Applies_CurrentBarCode")
                    .IsUnique();

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.ActId).HasColumnName("Act_Id");

                entity.Property(e => e.AimId).HasColumnName("Aim_Id");

                entity.Property(e => e.BaseLearnDateBegin).HasColumnType("datetime");

                entity.Property(e => e.BaseLearnDateEnd).HasColumnType("datetime");

                entity.Property(e => e.ConclusionResultId).HasColumnName("ConclusionResult_Id");

                entity.Property(e => e.ConvertedFromOldDb).HasColumnName("ConvertedFromOldDB");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatorBirthDate).HasColumnType("datetime");

                entity.Property(e => e.CreatorBlock).HasMaxLength(10);

                entity.Property(e => e.CreatorBuilding).HasMaxLength(10);

                entity.Property(e => e.CreatorCitizenshipId).HasColumnName("CreatorCitizenship_Id");

                entity.Property(e => e.CreatorCityName).HasMaxLength(100);

                entity.Property(e => e.CreatorCorpus).HasMaxLength(10);

                entity.Property(e => e.CreatorCountryId).HasColumnName("CreatorCountry_Id");

                entity.Property(e => e.CreatorEmail).HasMaxLength(100);

                entity.Property(e => e.CreatorFirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatorFlat).HasMaxLength(10);

                entity.Property(e => e.CreatorId).HasColumnName("Creator_Id");

                entity.Property(e => e.CreatorLastName).HasMaxLength(255);

                entity.Property(e => e.CreatorMailIndex).HasMaxLength(10);

                entity.Property(e => e.CreatorPassportReq).HasMaxLength(250);

                entity.Property(e => e.CreatorPassportType).HasMaxLength(50);

                entity.Property(e => e.CreatorPhone).HasMaxLength(100);

                entity.Property(e => e.CreatorStreet).HasMaxLength(100);

                entity.Property(e => e.CreatorSurname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CurrentBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.DateFirstPrint).HasColumnType("datetime");

                entity.Property(e => e.DateLastPrint).HasColumnType("datetime");

                entity.Property(e => e.DeliveryFormId).HasColumnName("DeliveryForm_Id");

                entity.Property(e => e.DocBlankNum).HasMaxLength(50);

                entity.Property(e => e.DocCountryId).HasColumnName("DocCountry_Id");

                entity.Property(e => e.DocDate).HasMaxLength(150);

                entity.Property(e => e.DocFullName).HasMaxLength(150);

                entity.Property(e => e.DocFullPackageTypeId).HasColumnName("DocFullPackageType_Id");

                entity.Property(e => e.DocRegNum).HasMaxLength(50);

                entity.Property(e => e.DocTypeId).HasColumnName("DocType_Id");

                entity.Property(e => e.EntryFormId).HasColumnName("EntryForm_Id");

                entity.Property(e => e.EpguCode).HasMaxLength(100);

                entity.Property(e => e.EpguSmev3Code).HasMaxLength(200);

                entity.Property(e => e.FixedLearnSpecialityId).HasColumnName("FixedLearnSpeciality_Id");

                entity.Property(e => e.FixedLearnSpecialityName).HasMaxLength(200);

                entity.Property(e => e.GetDate).HasColumnType("datetime");

                entity.Property(e => e.LastBlankNumber)
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.LastConclusionId).HasColumnName("LastConclusion_Id");

                entity.Property(e => e.MaxTermByEtap).HasColumnType("datetime");

                entity.Property(e => e.MinTermByEtap).HasColumnType("datetime");

                entity.Property(e => e.OrgCreator).HasMaxLength(200);

                entity.Property(e => e.OwnerBirthDate).HasColumnType("datetime");

                entity.Property(e => e.OwnerBlock).HasMaxLength(10);

                entity.Property(e => e.OwnerBuilding).HasMaxLength(10);

                entity.Property(e => e.OwnerCitizenshipId).HasColumnName("OwnerCitizenship_Id");

                entity.Property(e => e.OwnerCityName).HasMaxLength(100);

                entity.Property(e => e.OwnerCorpus).HasMaxLength(10);

                entity.Property(e => e.OwnerCountryId).HasColumnName("OwnerCountry_Id");

                entity.Property(e => e.OwnerFirstName).HasMaxLength(255);

                entity.Property(e => e.OwnerFlat).HasMaxLength(10);

                entity.Property(e => e.OwnerLastName).HasMaxLength(255);

                entity.Property(e => e.OwnerMailIndex).HasMaxLength(10);

                entity.Property(e => e.OwnerPassportReq).HasMaxLength(250);

                entity.Property(e => e.OwnerPassportType).HasMaxLength(50);

                entity.Property(e => e.OwnerPhone).HasMaxLength(100);

                entity.Property(e => e.OwnerStreet).HasMaxLength(100);

                entity.Property(e => e.OwnerSurname).HasMaxLength(255);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");

                entity.Property(e => e.PaymentNumber).HasMaxLength(100);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.PostDateFact).HasColumnType("datetime");

                entity.Property(e => e.ReturnOriginalsFormId).HasColumnName("ReturnOriginalsForm_Id");

                entity.Property(e => e.ReturnOriginalsPostAddress).HasMaxLength(200);

                entity.Property(e => e.SchoolAddress).HasMaxLength(50);

                entity.Property(e => e.SchoolCityName).HasMaxLength(100);

                entity.Property(e => e.SchoolCountryId).HasColumnName("SchoolCountry_Id");

                entity.Property(e => e.SchoolEmail).HasMaxLength(100);

                entity.Property(e => e.SchoolFax).HasMaxLength(50);

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.SchoolPhone).HasMaxLength(50);

                entity.Property(e => e.SchoolPostIndex).HasMaxLength(10);

                entity.Property(e => e.SchoolTypeId).HasColumnName("SchoolType_Id");

                entity.Property(e => e.SignatureDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialLearnDateBegin).HasColumnType("datetime");

                entity.Property(e => e.SpecialLearnDateEnd).HasColumnType("datetime");

                entity.Property(e => e.SpecialLearnFormId).HasColumnName("SpecialLearnForm_Id");

                entity.Property(e => e.StatusChangeTime).HasColumnType("datetime");

                entity.Property(e => e.StatusChangeUserId).HasColumnName("StatusChangeUser_Id");

                entity.Property(e => e.StatusExpertizeId).HasColumnName("StatusExpertize_Id");

                entity.Property(e => e.StatusFromOldDb).HasColumnName("StatusFromOldDB");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.Storage).HasMaxLength(100);

                entity.Property(e => e.SvidDate).HasColumnType("datetime");

                entity.Property(e => e.SvidNum).HasMaxLength(20);

                entity.Property(e => e.Uin).HasMaxLength(50);

                entity.Property(e => e.UinCreateTime).HasColumnType("datetime");

                entity.Property(e => e.UinRemoveTime).HasColumnType("datetime");

                entity.Property(e => e.WarrantDate).HasColumnType("datetime");

                entity.Property(e => e.WarrantReq).HasMaxLength(250);

                entity.Property(e => e.WarrantTerm).HasColumnType("datetime");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.ActId)
                    .HasConstraintName("FK_Applies_Acts");

                entity.HasOne(d => d.Aim)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.AimId)
                    .HasConstraintName("FK_ApplyAimApply");

                entity.HasOne(d => d.ConclusionResult)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.ConclusionResultId)
                    .HasConstraintName("FK_ApplyConclusionResult");

                entity.HasOne(d => d.ConclusionResultUser)
                    .WithMany(p => p.ApplyConclusionResultUsers)
                    .HasForeignKey(d => d.ConclusionResultUserId)
                    .HasConstraintName("FK_ConclusionResultUser");

                entity.HasOne(d => d.CreatorCitizenship)
                    .WithMany(p => p.ApplyCreatorCitizenships)
                    .HasForeignKey(d => d.CreatorCitizenshipId)
                    .HasConstraintName("FK_CountryApply");

                entity.HasOne(d => d.CreatorCountry)
                    .WithMany(p => p.ApplyCreatorCountries)
                    .HasForeignKey(d => d.CreatorCountryId)
                    .HasConstraintName("FK_CountryApply2");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ApplyApplyCreator");

                entity.HasOne(d => d.DeliveryForm)
                    .WithMany(p => p.ApplyDeliveryForms)
                    .HasForeignKey(d => d.DeliveryFormId)
                    .HasConstraintName("FK_Applies_ApplyDeliveryForms");

                entity.HasOne(d => d.DocCountry)
                    .WithMany(p => p.ApplyDocCountries)
                    .HasForeignKey(d => d.DocCountryId)
                    .HasConstraintName("FK_CountryApply4");

                entity.HasOne(d => d.DocFullPackageType)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.DocFullPackageTypeId)
                    .HasConstraintName("FK_ApplyDocFullPackageApply");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.DocTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ApplyDocTypeApply");

                entity.HasOne(d => d.EntryForm)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.EntryFormId)
                    .HasConstraintName("FK_ApplyEntryFormApply");

                entity.HasOne(d => d.FixedLearnSpeciality)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.FixedLearnSpecialityId)
                    .HasConstraintName("FK_ApplySpeciality");

                entity.HasOne(d => d.LastConclusion)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.LastConclusionId)
                    .HasConstraintName("FK_Applies_Conclusions");

                entity.HasOne(d => d.OwnerCitizenship)
                    .WithMany(p => p.ApplyOwnerCitizenships)
                    .HasForeignKey(d => d.OwnerCitizenshipId)
                    .HasConstraintName("FK_CountryApply1");

                entity.HasOne(d => d.OwnerCountry)
                    .WithMany(p => p.ApplyOwnerCountries)
                    .HasForeignKey(d => d.OwnerCountryId)
                    .HasConstraintName("FK_CountryApply3");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_PaymentApply");

                entity.HasOne(d => d.ReturnOriginalsForm)
                    .WithMany(p => p.ApplyReturnOriginalsForms)
                    .HasForeignKey(d => d.ReturnOriginalsFormId)
                    .HasConstraintName("FK_Applies_ReturnOriginalsForm");

                entity.HasOne(d => d.SchoolCountry)
                    .WithMany(p => p.ApplySchoolCountries)
                    .HasForeignKey(d => d.SchoolCountryId)
                    .HasConstraintName("FK_Applies_SchoolCountry");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SchoolApply");

                entity.HasOne(d => d.SchoolType)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.SchoolTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Applies_SchoolTypes");

                entity.HasOne(d => d.SpecialLearnForm)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.SpecialLearnFormId)
                    .HasConstraintName("FK_ApplyLearnFormApply");

                entity.HasOne(d => d.StatusChangeUser)
                    .WithMany(p => p.ApplyStatusChangeUsers)
                    .HasForeignKey(d => d.StatusChangeUserId)
                    .HasConstraintName("FK_UserApply");

                entity.HasOne(d => d.StatusExpertize)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.StatusExpertizeId)
                    .HasConstraintName("FK_ApplyExpertizeStatus");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyStatusApply");
            });

            modelBuilder.Entity<ApplyAdditionalField>(entity =>
            {
                entity.HasIndex(e => e.ApplyAdditionalFieldApplyApplyAdditionalFieldBarCode, "IX_FK_ApplyAdditionalField_Apply");

                entity.HasIndex(e => e.FieldId, "IX_FK_ApplyField_ApplyAdditionalField");

                entity.Property(e => e.ApplyAdditionalFieldApplyApplyAdditionalFieldBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("ApplyAdditionalField_Apply_ApplyAdditionalField_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.FieldId).HasColumnName("Field_Id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ApplyAdditionalFieldApplyApplyAdditionalFieldBarCodeNavigation)
                    .WithMany(p => p.ApplyAdditionalFields)
                    .HasForeignKey(d => d.ApplyAdditionalFieldApplyApplyAdditionalFieldBarCode)
                    .HasConstraintName("FK_ApplyAdditionalField_Apply");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.ApplyAdditionalFields)
                    .HasForeignKey(d => d.FieldId)
                    .HasConstraintName("FK_ApplyField_ApplyAdditionalField");
            });

            modelBuilder.Entity<ApplyAim>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NameEng).HasMaxLength(200);
            });

            modelBuilder.Entity<ApplyComment>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_FK_ApplyComment_User");

                entity.HasIndex(e => e.ApplyApplyCommentCommentBarCode, "IX_FK_Apply_ApplyComment");

                entity.Property(e => e.ApplyApplyCommentCommentBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Apply_ApplyComment_Comment_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.ApplyApplyCommentCommentBarCodeNavigation)
                    .WithMany(p => p.ApplyComments)
                    .HasForeignKey(d => d.ApplyApplyCommentCommentBarCode)
                    .HasConstraintName("FK_Apply_ApplyComment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplyComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ApplyComment_User");
            });

            modelBuilder.Entity<ApplyCommentUploadedFile>(entity =>
            {
                entity.HasKey(e => new { e.ApplyCommentUploadedFileUploadedFileId, e.FilesId })
                    .IsClustered(false);

                entity.ToTable("ApplyCommentUploadedFile");

                entity.HasIndex(e => e.FilesId, "IX_FK_ApplyCommentUploadedFile_UploadedFile");

                entity.Property(e => e.ApplyCommentUploadedFileUploadedFileId).HasColumnName("ApplyCommentUploadedFile_UploadedFile_Id");

                entity.Property(e => e.FilesId).HasColumnName("Files_Id");

                entity.HasOne(d => d.ApplyCommentUploadedFileUploadedFile)
                    .WithMany(p => p.ApplyCommentUploadedFiles)
                    .HasForeignKey(d => d.ApplyCommentUploadedFileUploadedFileId)
                    .HasConstraintName("FK_ApplyCommentUploadedFile_ApplyComment");

                entity.HasOne(d => d.Files)
                    .WithMany(p => p.ApplyCommentUploadedFiles)
                    .HasForeignKey(d => d.FilesId)
                    .HasConstraintName("FK_ApplyCommentUploadedFile_UploadedFile");
            });

            modelBuilder.Entity<ApplyCreator>(entity =>
            {
                entity.HasIndex(e => e.Email, "ApplyCreators_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Organisation, "IX_ApplyCreators");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DeleteTerm).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(765);

                entity.Property(e => e.Organisation).HasMaxLength(300);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Quota).HasDefaultValueSql("((3))");
            });

            modelBuilder.Entity<ApplyCreatorTicket>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<ApplyDeliveryForm>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NameEng).HasMaxLength(150);
            });

            modelBuilder.Entity<ApplyDocFullPackageType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ApplyDocType>(entity =>
            {
                entity.HasIndex(e => e.LearnLevelId, "IX_FK_LearnLevelApplyDocType");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LearnLevelId).HasColumnName("LearnLevel_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NameEng).HasMaxLength(200);

                entity.HasOne(d => d.LearnLevel)
                    .WithMany(p => p.ApplyDocTypes)
                    .HasForeignKey(d => d.LearnLevelId)
                    .HasConstraintName("FK_LearnLevelApplyDocType");
            });

            modelBuilder.Entity<ApplyDocument>(entity =>
            {
                entity.HasIndex(e => e.DocumentTypeId, "IX_FK_ApplyDocumentType_ApplyDocument");

                entity.HasIndex(e => e.DocumentFileId, "IX_FK_ApplyDocumentUploadedFile");

                entity.HasIndex(e => e.ApplyBarCode, "IX_FK_ApplyDocument_Apply");

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.DocumentFileId).HasColumnName("DocumentFile_Id");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_Id");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.ApplyDocuments)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplyDocument_Apply");

                entity.HasOne(d => d.DocumentFile)
                    .WithMany(p => p.ApplyDocuments)
                    .HasForeignKey(d => d.DocumentFileId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplyDocumentUploadedFile");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.ApplyDocuments)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_ApplyDocumentType_ApplyDocument");
            });

            modelBuilder.Entity<ApplyDocumentType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NameEng).HasMaxLength(200);
            });

            modelBuilder.Entity<ApplyEntryForm>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ApplyError>(entity =>
            {
                entity.HasIndex(e => e.ApplyApplyErrorApplyErrorBarCode, "IX_FK_ApplyApplyError");

                entity.HasIndex(e => e.FieldId, "IX_FK_ApplyErrorApplyField");

                entity.Property(e => e.ApplyApplyErrorApplyErrorBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("ApplyApplyError_ApplyError_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.FieldId).HasColumnName("Field_Id");

                entity.HasOne(d => d.ApplyApplyErrorApplyErrorBarCodeNavigation)
                    .WithMany(p => p.ApplyErrors)
                    .HasForeignKey(d => d.ApplyApplyErrorApplyErrorBarCode)
                    .HasConstraintName("FK_ApplyApplyError");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.ApplyErrors)
                    .HasForeignKey(d => d.FieldId)
                    .HasConstraintName("FK_ApplyErrorApplyField");
            });

            modelBuilder.Entity<ApplyField>(entity =>
            {
                entity.Property(e => e.Block)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BlockEng).HasMaxLength(200);

                entity.Property(e => e.IsPortal)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTerminal)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LabelEng).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderNum)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.RemarkEng).HasMaxLength(200);

                entity.Property(e => e.Subblock)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SubblockEng).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ApplyHandoutView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ApplyHandout_View");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ApplyLearnForm>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEng).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplyRecipientView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ApplyRecipient_View");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ApplyRon>(entity =>
            {
                entity.HasKey(e => e.BarCode)
                    .HasName("PK_AppliesRon");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatorBlock).HasMaxLength(10);

                entity.Property(e => e.CreatorBuilding).HasMaxLength(10);

                entity.Property(e => e.CreatorCityName).HasMaxLength(100);

                entity.Property(e => e.CreatorCorpus).HasMaxLength(10);

                entity.Property(e => e.CreatorCountryId).HasColumnName("CreatorCountry_Id");

                entity.Property(e => e.CreatorEmail).HasMaxLength(100);

                entity.Property(e => e.CreatorFirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatorFlat).HasMaxLength(10);

                entity.Property(e => e.CreatorLastName).HasMaxLength(255);

                entity.Property(e => e.CreatorMailIndex).HasMaxLength(10);

                entity.Property(e => e.CreatorPhone).HasMaxLength(100);

                entity.Property(e => e.CreatorStreet).HasMaxLength(100);

                entity.Property(e => e.CreatorSurname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DocBlankNum).HasMaxLength(50);

                entity.Property(e => e.DocCountryId).HasColumnName("DocCountry_Id");

                entity.Property(e => e.DocDate).HasMaxLength(150);

                entity.Property(e => e.DocFullName).HasMaxLength(150);

                entity.Property(e => e.DocRegNum).HasMaxLength(50);

                entity.Property(e => e.DocTypeId).HasColumnName("DocType_Id");

                entity.Property(e => e.Qualification).HasMaxLength(250);

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.Speciality).HasMaxLength(250);

                entity.Property(e => e.StatusChangeTime).HasColumnType("datetime");

                entity.Property(e => e.StatusChangeUserId).HasColumnName("StatusChangeUser_Id");

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_Id")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatorCountry)
                    .WithMany(p => p.ApplyRonCreatorCountries)
                    .HasForeignKey(d => d.CreatorCountryId)
                    .HasConstraintName("FK_ApplyRonsCreatorCountry_Countries");

                entity.HasOne(d => d.DocCountry)
                    .WithMany(p => p.ApplyRonDocCountries)
                    .HasForeignKey(d => d.DocCountryId)
                    .HasConstraintName("FK_ApplyRonsDocCountry_Countries");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.ApplyRons)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_ApplyRons_ApplyDocTypes");

                entity.HasOne(d => d.StatusChangeUser)
                    .WithMany(p => p.ApplyRons)
                    .HasForeignKey(d => d.StatusChangeUserId)
                    .HasConstraintName("FK_ApplyRons_Users");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ApplyRons)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyRons_ApplyRonStatuses");
            });

            modelBuilder.Entity<ApplyRonAppeal>(entity =>
            {
                entity.Property(e => e.ApplyRonBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.RegNum).HasMaxLength(50);

                entity.HasOne(d => d.ApplyRonBarCodeNavigation)
                    .WithMany(p => p.ApplyRonAppeals)
                    .HasForeignKey(d => d.ApplyRonBarCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplyRonAppeals_ApplyRons");
            });

            modelBuilder.Entity<ApplyRonDocument>(entity =>
            {
                entity.Property(e => e.ApplyRonBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.DocumentFileId).HasColumnName("DocumentFile_Id");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_Id");

                entity.HasOne(d => d.ApplyRonBarCodeNavigation)
                    .WithMany(p => p.ApplyRonDocuments)
                    .HasForeignKey(d => d.ApplyRonBarCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplyRonDocuments_ApplyRons");

                entity.HasOne(d => d.DocumentFile)
                    .WithMany(p => p.ApplyRonDocuments)
                    .HasForeignKey(d => d.DocumentFileId)
                    .HasConstraintName("FK_ApplyRonDocuments_UploadedFiles");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.ApplyRonDocuments)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_ApplyRonDocuments_ApplyDocumentTypes");
            });

            modelBuilder.Entity<ApplyRonLetter>(entity =>
            {
                entity.Property(e => e.ApplyRonBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.DestinationId).HasColumnName("Destination_Id");

                entity.Property(e => e.DocxFileId).HasColumnName("DocxFile_Id");

                entity.Property(e => e.TemplateId).HasColumnName("Template_Id");

                entity.HasOne(d => d.ApplyRonBarCodeNavigation)
                    .WithMany(p => p.ApplyRonLetters)
                    .HasForeignKey(d => d.ApplyRonBarCode)
                    .HasConstraintName("FK_ApplyRonLetters_ApplyRons");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.ApplyRonLetters)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK_ApplyRonLetters_ApplyRonLetterDests");

                entity.HasOne(d => d.DocxFile)
                    .WithMany(p => p.ApplyRonLetters)
                    .HasForeignKey(d => d.DocxFileId)
                    .HasConstraintName("FK_ApplyRonLetters_UploadedFiles");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.ApplyRonLetters)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyRonLetters_DocxTemplates");
            });

            modelBuilder.Entity<ApplyRonLetterDest>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<ApplyRonStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ApplyRonStatusHistory>(entity =>
            {
                entity.Property(e => e.ApplyRonBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("ApplyRon_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.PrevStatusId).HasColumnName("PrevStatus_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.ApplyRonBarCodeNavigation)
                    .WithMany(p => p.ApplyRonStatusHistories)
                    .HasForeignKey(d => d.ApplyRonBarCode)
                    .HasConstraintName("FK_ApplyRonStatusHistories_ApplyRons");

                entity.HasOne(d => d.PrevStatus)
                    .WithMany(p => p.ApplyRonStatusHistoryPrevStatuses)
                    .HasForeignKey(d => d.PrevStatusId)
                    .HasConstraintName("FK_ApplyRonStatusHistories_ApplyRonStatuses1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ApplyRonStatusHistoryStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyRonStatusHistories_ApplyRonStatuses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplyRonStatusHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyRonStatusHistories_Users");
            });

            modelBuilder.Entity<ApplyStatus>(entity =>
            {
                entity.Property(e => e.AllowStepToStatuses).HasMaxLength(60);

                entity.Property(e => e.DateFieldName).HasMaxLength(50);

                entity.Property(e => e.EtapId).HasColumnName("Etap_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameForApplier).HasMaxLength(50);

                entity.Property(e => e.NameForApplierEng).HasMaxLength(50);

                entity.Property(e => e.NameForButton).HasMaxLength(50);

                entity.Property(e => e.RollbackAction).HasMaxLength(256);

                entity.Property(e => e.StepAction).HasMaxLength(256);

                entity.Property(e => e.StepGrants).HasMaxLength(50);

                entity.Property(e => e.ViewGrants)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Etap)
                    .WithMany(p => p.ApplyStatuses)
                    .HasForeignKey(d => d.EtapId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ApplyStatuses_ReglamentEtaps");
            });

            modelBuilder.Entity<ApplyStatusHistory>(entity =>
            {
                entity.HasIndex(e => e.ChangeTime, "IX_ApplyStatusHistories_ChangeTime");

                entity.HasIndex(e => e.ApplyBarCode, "IX_FK_ApplyApplyStatusHistory");

                entity.HasIndex(e => e.StatusId, "IX_FK_ApplyStatusApplyStatusHistory");

                entity.HasIndex(e => e.UserId, "IX_FK_UserApplyStatusHistory");

                entity.Property(e => e.ApplyBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ChangeTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.PrevStatusId).HasColumnName("PrevStatus_Id");

                entity.Property(e => e.RollbackTime).HasColumnType("datetime");

                entity.Property(e => e.StatusFromOldDb).HasColumnName("StatusFromOldDB");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.ApplyStatusHistories)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .HasConstraintName("FK_ApplyApplyStatusHistory");

                entity.HasOne(d => d.PrevStatus)
                    .WithMany(p => p.ApplyStatusHistoryPrevStatuses)
                    .HasForeignKey(d => d.PrevStatusId)
                    .HasConstraintName("FK_ApplyStatusHistories_ApplyPrevStatuses");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ApplyStatusHistoryStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyStatusApplyStatusHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplyStatusHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplyStatusHistory");
            });

            modelBuilder.Entity<ApplyTemplate>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ApplyTemplateField>(entity =>
            {
                entity.HasIndex(e => e.FieldId, "IX_FK_ApplyField_ApplyTemplateField");

                entity.HasIndex(e => e.TemplateId, "IX_FK_ApplyTemplate_ApplyTemplateField");

                entity.Property(e => e.FieldId).HasColumnName("Field_Id");

                entity.Property(e => e.TemplateId).HasColumnName("Template_Id");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.ApplyTemplateFields)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyField_ApplyTemplateField");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.ApplyTemplateFields)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyTemplate_ApplyTemplateField");
            });

            modelBuilder.Entity<AutoComplete>(entity =>
            {
                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<B20121121ApplyStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("b20121121ApplyStatuses");

                entity.Property(e => e.AllowStepToStatuses).HasMaxLength(60);

                entity.Property(e => e.DateFieldName).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameForApplier).HasMaxLength(50);

                entity.Property(e => e.NameForButton).HasMaxLength(50);

                entity.Property(e => e.RollbackAction).HasMaxLength(256);

                entity.Property(e => e.StepAction).HasMaxLength(256);

                entity.Property(e => e.StepGrants).HasMaxLength(50);

                entity.Property(e => e.ViewGrants)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<B20121121ApplyStatusHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("b20121121ApplyStatusHistories");

                entity.Property(e => e.ApplyBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ChangeTime).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RollbackTime).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<B20121121ExpertizeStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("b20121121ExpertizeStatuses");

                entity.Property(e => e.AllowStepToStatuses).HasMaxLength(60);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StepAction).HasMaxLength(256);

                entity.Property(e => e.ViewGrants)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BackUp130311SchoolsAddedExpert>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");
            });

            modelBuilder.Entity<Blank>(entity =>
            {
                entity.HasKey(e => new { e.Ser, e.Number });

                entity.HasIndex(e => e.UsedTime, "IX_Blanks");

                entity.Property(e => e.Ser)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Number)
                    .HasMaxLength(8)
                    .IsFixedLength(true);

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.DuplicateBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.UsedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Blanks)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Blanks_Applies");

                entity.HasOne(d => d.DuplicateBarCodeNavigation)
                    .WithMany(p => p.Blanks)
                    .HasForeignKey(d => d.DuplicateBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Blanks_Duplicates");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blanks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Blanks_Users");
            });

            modelBuilder.Entity<CardPayment>(entity =>
            {
                entity.HasIndex(e => e.BarCode, "IX_CardPaymentsBarCode")
                    .IsUnique();

                entity.HasIndex(e => e.OrderId, "IX_CardPaymentsOrderId")
                    .IsUnique();

                entity.HasIndex(e => e.Time, "IX_CardPaymentsTime");

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Bank).HasMaxLength(50);

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(24);

                entity.Property(e => e.CardholderName).HasMaxLength(150);

                entity.Property(e => e.DuplicateBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.ErrorCode)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.ErrorMessage).HasMaxLength(600);

                entity.Property(e => e.Expiration).HasMaxLength(12);

                entity.Property(e => e.OrderId).HasMaxLength(50);

                entity.Property(e => e.OrderStatusDesc).HasMaxLength(1000);

                entity.Property(e => e.Pan).HasMaxLength(24);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.CardPayments)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CardPayments_Applies");

                entity.HasOne(d => d.DuplicateBarCodeNavigation)
                    .WithMany(p => p.CardPayments)
                    .HasForeignKey(d => d.DuplicateBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CardPayments_Duplicates");
            });

            modelBuilder.Entity<ChiefRoomFrame>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Reporting)
                    .WithMany(p => p.ChiefRoomFrames)
                    .HasForeignKey(d => d.ReportingId)
                    .HasConstraintName("FK_ChiefRoomFrames_Reportings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChiefRoomFrames)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ChiefRoomFrames_Users");
            });

            modelBuilder.Entity<Conclusion>(entity =>
            {
                entity.HasIndex(e => e.CreateTime, "IX_ConclusionsCreateTime");

                entity.HasIndex(e => e.SourceDocumentTypeForExpertId, "IX_FK_ApplyDocTypeConclusion");

                entity.HasIndex(e => e.AimId, "IX_FK_ConclusionAim");

                entity.HasIndex(e => e.CertificationTypeId, "IX_FK_ConclusionCertificationTypeConclusion");

                entity.HasIndex(e => e.ConclusionDocTypeId, "IX_FK_ConclusionConclusionDocType");

                entity.HasIndex(e => e.ConclusionReasonsId, "IX_FK_ConclusionConclusionReason");

                entity.HasIndex(e => e.ConfirmTypeId, "IX_FK_ConclusionConfirmTypeConclusion");

                entity.HasIndex(e => e.CreatorCitizenshipId, "IX_FK_ConclusionCreatorCitizenship");

                entity.HasIndex(e => e.DocumentTypeId, "IX_FK_ConclusionDocumentType");

                entity.HasIndex(e => e.BaseLearnIdorightId, "IX_FK_ConclusionIDORightConclusion");

                entity.HasIndex(e => e.LearnFormId, "IX_FK_ConclusionLearnForm");

                entity.HasIndex(e => e.LicenseTypeId, "IX_FK_ConclusionLicenseTypeConclusion");

                entity.HasIndex(e => e.SpecLearnMeasureId, "IX_FK_ConclusionMeasureConclusion");

                entity.HasIndex(e => e.QualificationIdoId, "IX_FK_ConclusionQualificationIDO");

                entity.HasIndex(e => e.SchoolId, "IX_FK_ConclusionSchool");

                entity.HasIndex(e => e.SpecialityIdoId, "IX_FK_ConclusionSpecialityIDO");

                entity.HasIndex(e => e.ExpertizeId, "IX_FK_ExpertizeConclusion");

                entity.HasIndex(e => e.LearnLevelId, "IX_FK_LearnLevelConclusion");

                entity.HasIndex(e => e.StandardLearnLevelId, "IX_FK_LearnLevelConclusion1");

                entity.HasIndex(e => e.LearnLevelMskoId, "IX_FK_LearnLevelMSKOConclusion");

                entity.HasIndex(e => e.LegalizationTypeId, "IX_FK_LegalizationConclusion");

                entity.HasIndex(e => e.QualificationId, "IX_FK_QualificationConclusion");

                entity.HasIndex(e => e.StandardDocTypeId, "IX_FK_StandardDocTypeConclusion");

                entity.HasIndex(e => e.CreatorId, "IX_FK_UserConclusion");

                entity.Property(e => e.AimId).HasColumnName("Aim_Id");

                entity.Property(e => e.BaseLearnIdorightId).HasColumnName("BaseLearnIDORight_Id");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CertificationAttachmentIdo).HasColumnName("CertificationAttachmentIDO");

                entity.Property(e => e.CertificationDocumentIdo).HasColumnName("CertificationDocumentIDO");

                entity.Property(e => e.CertificationTypeId).HasColumnName("CertificationType_Id");

                entity.Property(e => e.CertificationUrl).HasColumnName("CertificationURL");

                entity.Property(e => e.ConclusionDocTypeId).HasColumnName("ConclusionDocType_Id");

                entity.Property(e => e.ConclusionReasonsId).HasColumnName("ConclusionReasons_Id");

                entity.Property(e => e.ConfirmTypeId).HasColumnName("ConfirmType_Id");

                entity.Property(e => e.ConfirmationUrl).HasColumnName("ConfirmationURL");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatorCitizenshipId).HasColumnName("CreatorCitizenship_Id");

                entity.Property(e => e.CreatorId).HasColumnName("Creator_Id");

                entity.Property(e => e.DocumentDate).HasMaxLength(150);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_Id");

                entity.Property(e => e.DocumentTypeName).HasMaxLength(250);

                entity.Property(e => e.ExpertizeId).HasColumnName("Expertize_Id");

                entity.Property(e => e.LearnFormId).HasColumnName("LearnForm_Id");

                entity.Property(e => e.LearnLevelId).HasColumnName("LearnLevel_Id");

                entity.Property(e => e.LearnLevelMskoId).HasColumnName("LearnLevelMSKO_Id");

                entity.Property(e => e.LearnLevelMskotype)
                    .HasMaxLength(20)
                    .HasColumnName("LearnLevelMSKOType");

                entity.Property(e => e.LegalizationTypeId).HasColumnName("LegalizationType_Id");

                entity.Property(e => e.LicenseAttachmentIdo).HasColumnName("LicenseAttachmentIDO");

                entity.Property(e => e.LicenseDocumentIdo).HasColumnName("LicenseDocumentIDO");

                entity.Property(e => e.LicenseTypeId).HasColumnName("LicenseType_Id");

                entity.Property(e => e.LicenseUrl).HasColumnName("LicenseURL");

                entity.Property(e => e.LicensingVariantAttachmentIdo).HasColumnName("LicensingVariantAttachmentIDO");

                entity.Property(e => e.LicensingVariantDocumentIdo).HasColumnName("LicensingVariantDocumentIDO");

                entity.Property(e => e.LicensingVariantId).HasColumnName("LicensingVariant_Id");

                entity.Property(e => e.LicensingVariantUrl).HasColumnName("LicensingVariantURL");

                entity.Property(e => e.QualificationId).HasColumnName("Qualification_Id");

                entity.Property(e => e.QualificationIdoId).HasColumnName("QualificationIDO_Id");

                entity.Property(e => e.QualificationIdoname)
                    .HasMaxLength(250)
                    .HasColumnName("QualificationIDOName");

                entity.Property(e => e.SchoolCountryId).HasColumnName("SchoolCountry_Id");

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.SourceDocumentTypeForExpertId).HasColumnName("SourceDocumentTypeForExpert_Id");

                entity.Property(e => e.SpecLearnMeasureId).HasColumnName("SpecLearnMeasure_Id");

                entity.Property(e => e.SpecialityIdoId).HasColumnName("SpecialityIDO_Id");

                entity.Property(e => e.SpecialityIdoname)
                    .HasMaxLength(250)
                    .HasColumnName("SpecialityIDOName");

                entity.Property(e => e.StandardDocTypeId).HasColumnName("StandardDocType_Id");

                entity.Property(e => e.StandardLearnLevelId).HasColumnName("StandardLearnLevel_Id");

                entity.Property(e => e.StandardQualificationExId).HasColumnName("StandardQualificationEx_Id");

                entity.Property(e => e.StandardSpecialityExId).HasColumnName("StandardSpecialityEx_Id");

                entity.Property(e => e.StandardUgs).HasMaxLength(500);

                entity.HasOne(d => d.Aim)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.AimId)
                    .HasConstraintName("FK_ConclusionAim");

                entity.HasOne(d => d.BaseLearnIdoright)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.BaseLearnIdorightId)
                    .HasConstraintName("FK_ConclusionIDORightConclusion");

                entity.HasOne(d => d.CertificationType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.CertificationTypeId)
                    .HasConstraintName("FK_ConclusionCertificationTypeConclusion");

                entity.HasOne(d => d.ConclusionDocType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.ConclusionDocTypeId)
                    .HasConstraintName("FK_ConclusionConclusionDocType");

                entity.HasOne(d => d.ConclusionReasons)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.ConclusionReasonsId)
                    .HasConstraintName("FK_ConclusionConclusionReason");

                entity.HasOne(d => d.ConfirmType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.ConfirmTypeId)
                    .HasConstraintName("FK_ConclusionConfirmTypeConclusion");

                entity.HasOne(d => d.CreatorCitizenship)
                    .WithMany(p => p.ConclusionCreatorCitizenships)
                    .HasForeignKey(d => d.CreatorCitizenshipId)
                    .HasConstraintName("FK_ConclusionCreatorCitizenship");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserConclusion");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.ConclusionDocumentTypes)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_ConclusionDocumentType");

                entity.HasOne(d => d.Expertize)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.ExpertizeId)
                    .HasConstraintName("FK_ExpertizeConclusion");

                entity.HasOne(d => d.LearnForm)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.LearnFormId)
                    .HasConstraintName("FK_ConclusionLearnForm");

                entity.HasOne(d => d.LearnLevel)
                    .WithMany(p => p.ConclusionLearnLevels)
                    .HasForeignKey(d => d.LearnLevelId)
                    .HasConstraintName("FK_LearnLevelConclusion");

                entity.HasOne(d => d.LearnLevelMsko)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.LearnLevelMskoId)
                    .HasConstraintName("FK_LearnLevelMSKOConclusion");

                entity.HasOne(d => d.LegalizationType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.LegalizationTypeId)
                    .HasConstraintName("FK_LegalizationConclusion");

                entity.HasOne(d => d.LicenseType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.LicenseTypeId)
                    .HasConstraintName("FK_ConclusionLicenseTypeConclusion");

                entity.HasOne(d => d.LicensingVariant)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.LicensingVariantId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Conclusions_ConclusionLicensingVariants");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.ConclusionQualifications)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK_QualificationConclusion");

                entity.HasOne(d => d.QualificationIdo)
                    .WithMany(p => p.ConclusionQualificationIdos)
                    .HasForeignKey(d => d.QualificationIdoId)
                    .HasConstraintName("FK_ConclusionQualificationIDO");

                entity.HasOne(d => d.SchoolCountry)
                    .WithMany(p => p.ConclusionSchoolCountries)
                    .HasForeignKey(d => d.SchoolCountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Conclusions_SchoolCountry");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_ConclusionSchool");

                entity.HasOne(d => d.SourceDocumentTypeForExpert)
                    .WithMany(p => p.ConclusionSourceDocumentTypeForExperts)
                    .HasForeignKey(d => d.SourceDocumentTypeForExpertId)
                    .HasConstraintName("FK_ApplyDocTypeConclusion");

                entity.HasOne(d => d.SpecLearnMeasure)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.SpecLearnMeasureId)
                    .HasConstraintName("FK_ConclusionMeasureConclusion");

                entity.HasOne(d => d.SpecialityIdo)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.SpecialityIdoId)
                    .HasConstraintName("FK_ConclusionSpecialityIDO");

                entity.HasOne(d => d.StandardDocType)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.StandardDocTypeId)
                    .HasConstraintName("FK_StandardDocTypeConclusion");

                entity.HasOne(d => d.StandardLearnLevel)
                    .WithMany(p => p.ConclusionStandardLearnLevels)
                    .HasForeignKey(d => d.StandardLearnLevelId)
                    .HasConstraintName("FK_LearnLevelConclusion1");

                entity.HasOne(d => d.StandardQualificationEx)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.StandardQualificationExId)
                    .HasConstraintName("FK_Conclusions_StandardQualifications");

                entity.HasOne(d => d.StandardSpecialityEx)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.StandardSpecialityExId)
                    .HasConstraintName("FK_Conclusions_StandardSpecialities");
            });

            modelBuilder.Entity<ConclusionAdditionalField>(entity =>
            {
                entity.HasIndex(e => e.DecisionDecisionAdditionalFieldDecisionAdditionalFieldId, "IX_FK_DecisionDecisionAdditionalField");

                entity.HasIndex(e => e.FieldId, "IX_FK_DecisionFieldDecisionAdditionalField");

                entity.Property(e => e.DecisionDecisionAdditionalFieldDecisionAdditionalFieldId).HasColumnName("DecisionDecisionAdditionalField_DecisionAdditionalField_Id");

                entity.Property(e => e.FieldId).HasColumnName("Field_Id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DecisionDecisionAdditionalFieldDecisionAdditionalField)
                    .WithMany(p => p.ConclusionAdditionalFields)
                    .HasForeignKey(d => d.DecisionDecisionAdditionalFieldDecisionAdditionalFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DecisionDecisionAdditionalField");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.ConclusionAdditionalFields)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DecisionFieldDecisionAdditionalField");
            });

            modelBuilder.Entity<ConclusionCertificationType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionConclusionRight>(entity =>
            {
                entity.HasKey(e => new { e.ConclusionConclusionRightConclusionRightId, e.RightsId })
                    .IsClustered(false);

                entity.ToTable("ConclusionConclusionRight");

                entity.HasIndex(e => e.RightsId, "IX_FK_ConclusionConclusionRight_ConclusionRight");

                entity.Property(e => e.ConclusionConclusionRightConclusionRightId).HasColumnName("ConclusionConclusionRight_ConclusionRight_Id");

                entity.Property(e => e.RightsId).HasColumnName("Rights_Id");

                entity.HasOne(d => d.ConclusionConclusionRightConclusionRight)
                    .WithMany(p => p.ConclusionConclusionRights)
                    .HasForeignKey(d => d.ConclusionConclusionRightConclusionRightId)
                    .HasConstraintName("FK_ConclusionConclusionRight_Conclusion");

                entity.HasOne(d => d.Rights)
                    .WithMany(p => p.ConclusionConclusionRights)
                    .HasForeignKey(d => d.RightsId)
                    .HasConstraintName("FK_ConclusionConclusionRight_ConclusionRight");
            });

            modelBuilder.Entity<ConclusionConfirmType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionDocType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ConclusionField>(entity =>
            {
                entity.HasIndex(e => e.DecisionTemplateFieldDecisionFieldDecisionFieldId, "IX_FK_DecisionTemplateFieldDecisionField");

                entity.Property(e => e.Block).IsRequired();

                entity.Property(e => e.DecisionTemplateFieldDecisionFieldDecisionFieldId).HasColumnName("DecisionTemplateFieldDecisionField_DecisionField_Id");

                entity.Property(e => e.IsArchiveField)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Label).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.OrderNum).IsRequired();

                entity.Property(e => e.ParentFieldId).HasColumnName("ParentFieldID");

                entity.Property(e => e.Remark).IsRequired();

                entity.Property(e => e.Subblock).IsRequired();

                entity.Property(e => e.Type).IsRequired();

                entity.HasOne(d => d.DecisionTemplateFieldDecisionFieldDecisionField)
                    .WithMany(p => p.ConclusionFields)
                    .HasForeignKey(d => d.DecisionTemplateFieldDecisionFieldDecisionFieldId)
                    .HasConstraintName("FK_DecisionTemplateFieldDecisionField");
            });

            modelBuilder.Entity<ConclusionIdoright>(entity =>
            {
                entity.ToTable("ConclusionIDORights");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionLicenseConfirmType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionLicenseConfirmTypeConclusion>(entity =>
            {
                entity.HasKey(e => new { e.LicenseConfirmTypeId, e.ConclusionId })
                    .IsClustered(false);

                entity.ToTable("ConclusionLicenseConfirmTypeConclusion");

                entity.HasIndex(e => e.ConclusionId, "IX_FK_ConclusionLicenseConfirmTypeConclusion_Conclusion");

                entity.Property(e => e.LicenseConfirmTypeId).HasColumnName("LicenseConfirmType_Id");

                entity.Property(e => e.ConclusionId).HasColumnName("Conclusion_Id");

                entity.HasOne(d => d.Conclusion)
                    .WithMany(p => p.ConclusionLicenseConfirmTypeConclusions)
                    .HasForeignKey(d => d.ConclusionId)
                    .HasConstraintName("FK_ConclusionLicenseConfirmTypeConclusion_Conclusion");

                entity.HasOne(d => d.LicenseConfirmType)
                    .WithMany(p => p.ConclusionLicenseConfirmTypeConclusions)
                    .HasForeignKey(d => d.LicenseConfirmTypeId)
                    .HasConstraintName("FK_ConclusionLicenseConfirmTypeConclusion_ConclusionLicenseConfirmType");
            });

            modelBuilder.Entity<ConclusionLicenseType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionLicensingVariant>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ConclusionMeasure>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ConclusionReason>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ResultId).HasColumnName("Result_Id");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ConclusionReasons)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ConclusionReasons_Results");
            });

            modelBuilder.Entity<ConclusionResult>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ConclusionRight>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ConclusionTemplate>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ConclusionTemplateField>(entity =>
            {
                entity.HasIndex(e => e.DecisionTemplateDecisionTemplateFieldDecisionTemplateFieldId, "IX_FK_DecisionTemplateDecisionTemplateField");

                entity.Property(e => e.DecisionTemplateDecisionTemplateFieldDecisionTemplateFieldId).HasColumnName("DecisionTemplateDecisionTemplateField_DecisionTemplateField_Id");

                entity.HasOne(d => d.DecisionTemplateDecisionTemplateFieldDecisionTemplateField)
                    .WithMany(p => p.ConclusionTemplateFields)
                    .HasForeignKey(d => d.DecisionTemplateDecisionTemplateFieldDecisionTemplateFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DecisionTemplateDecisionTemplateField");
            });

            modelBuilder.Entity<ConfirmedSchoolDoc>(entity =>
            {
                entity.Property(e => e.Equivalent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ConfirmedSchoolDocs)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfirmedSchoolDocs_ConfirmedSchoolNames");
            });

            modelBuilder.Entity<ConfirmedSchoolName>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NameEng).HasMaxLength(500);

                entity.Property(e => e.NameNative).HasMaxLength(500);

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ConfirmedSchoolNames)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfirmedSchoolNames_Countries");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ConfirmedSchoolNames)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfirmedSchoolNames_ConfirmedSchoolOrders");
            });

            modelBuilder.Entity<ConfirmedSchoolOrder>(entity =>
            {
                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Site).HasMaxLength(500);
            });

            modelBuilder.Entity<CorrespondentionInput>(entity =>
            {
                entity.Property(e => e.Correspondent).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("DeliveryType_Id");

                entity.Property(e => e.DocNum).HasMaxLength(64);

                entity.Property(e => e.InpNum).HasMaxLength(64);

                entity.Property(e => e.ModifiedById).HasColumnName("ModifiedBy_Id");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Organization).HasMaxLength(200);

                entity.Property(e => e.PerformerId).HasColumnName("Performer_id");

                entity.Property(e => e.Priority).HasDefaultValueSql("((1))");

                entity.Property(e => e.TermTime).HasColumnType("datetime");

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CorrespondentionInputCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionInputsCreated_Users");

                entity.HasOne(d => d.DeletedBy)
                    .WithMany(p => p.CorrespondentionInputDeletedBies)
                    .HasForeignKey(d => d.DeletedById)
                    .HasConstraintName("FK_CorrespondentionInputsDeleted_Users");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.CorrespondentionInputs)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CorrespondentionInputs_CorrespondentionInputDeliveryTypes");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.CorrespondentionInputModifiedBies)
                    .HasForeignKey(d => d.ModifiedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionInputsModified_Users");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.CorrespondentionInputPerformers)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("FK_CorrespondentionInputs_Performers");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CorrespondentionInputs)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_CorrespondentionInputs_CorrespondentionInputTypes");
            });

            modelBuilder.Entity<CorrespondentionInputAttach>(entity =>
            {
                entity.HasKey(e => new { e.CorrInputId, e.FileId });

                entity.Property(e => e.CorrInputId).HasColumnName("CorrInput_Id");

                entity.Property(e => e.FileId).HasColumnName("File_Id");

                entity.HasOne(d => d.CorrInput)
                    .WithMany(p => p.CorrespondentionInputAttaches)
                    .HasForeignKey(d => d.CorrInputId)
                    .HasConstraintName("FK_CorrespondentionInputAttachs_CorrespondentionInputs");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CorrespondentionInputAttaches)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK_CorrespondentionInputAttachs_UploadedFiles");
            });

            modelBuilder.Entity<CorrespondentionInputBarCode>(entity =>
            {
                entity.HasKey(e => new { e.InputId, e.BarCode });

                entity.Property(e => e.InputId).HasColumnName("Input_Id");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Input)
                    .WithMany(p => p.CorrespondentionInputBarCodes)
                    .HasForeignKey(d => d.InputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionInputBarCodes_CorrespondentionInputs");
            });

            modelBuilder.Entity<CorrespondentionInputDeliveryType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CorrespondentionInputType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CorrespondentionOutput>(entity =>
            {
                entity.Property(e => e.Correspondent).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedById).HasColumnName("DeletedBy_Id");

                entity.Property(e => e.DeletedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DocNum).HasMaxLength(64);

                entity.Property(e => e.ModifiedById).HasColumnName("ModifiedBy_Id");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Organization).HasMaxLength(200);

                entity.Property(e => e.PerformerId).HasColumnName("Performer_id");

                entity.Property(e => e.Priority).HasDefaultValueSql("((1))");

                entity.Property(e => e.SignerId).HasColumnName("Signer_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CorrespondentionOutputCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionOutputsCreated_Users");

                entity.HasOne(d => d.DeletedBy)
                    .WithMany(p => p.CorrespondentionOutputDeletedBies)
                    .HasForeignKey(d => d.DeletedById)
                    .HasConstraintName("FK_CorrespondentionOutputsDeleted_Users");

                entity.HasOne(d => d.Input)
                    .WithMany(p => p.CorrespondentionOutputs)
                    .HasForeignKey(d => d.InputId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CorrespondentionOutputs_CorrespondentionInputs");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.CorrespondentionOutputModifiedBies)
                    .HasForeignKey(d => d.ModifiedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionOutputsModified_Users");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.CorrespondentionOutputPerformers)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("FK_CorrespondentionOutputsPerformer_Users");

                entity.HasOne(d => d.Signer)
                    .WithMany(p => p.CorrespondentionOutputSigners)
                    .HasForeignKey(d => d.SignerId)
                    .HasConstraintName("FK_CorrespondentionOutputsSigner_Users");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CorrespondentionOutputs)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_CorrespondentionOutputs_CorrespondentionOutputStatuses");
            });

            modelBuilder.Entity<CorrespondentionOutputAttach>(entity =>
            {
                entity.HasKey(e => new { e.CorrOutputId, e.FileId });

                entity.Property(e => e.CorrOutputId).HasColumnName("CorrOutput_Id");

                entity.Property(e => e.FileId).HasColumnName("File_Id");

                entity.HasOne(d => d.CorrOutput)
                    .WithMany(p => p.CorrespondentionOutputAttaches)
                    .HasForeignKey(d => d.CorrOutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionOutputAttachs_CorrespondentionOutputs");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CorrespondentionOutputAttaches)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK_CorrespondentionOutputAttachs_UploadedFiles");
            });

            modelBuilder.Entity<CorrespondentionOutputBarCode>(entity =>
            {
                entity.HasKey(e => new { e.OutputId, e.BarCode });

                entity.Property(e => e.OutputId).HasColumnName("Output_Id");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Type).HasMaxLength(16);

                entity.HasOne(d => d.Output)
                    .WithMany(p => p.CorrespondentionOutputBarCodes)
                    .HasForeignKey(d => d.OutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrespondentionOutputBarCodes_CorrespondentionOutputs");
            });

            modelBuilder.Entity<CorrespondentionOutputStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.IsgaCode, "IX_Countries_IsgaCode");

                entity.HasIndex(e => e.LegalizationId, "IX_FK_LegalizationCountry");

                entity.HasIndex(e => e.RegionId, "IX_FK_RegionCountry");

                entity.Property(e => e.A2code)
                    .HasMaxLength(2)
                    .HasColumnName("A2Code");

                entity.Property(e => e.A3code)
                    .HasMaxLength(3)
                    .HasColumnName("A3Code");

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.IsgaCode).HasMaxLength(50);

                entity.Property(e => e.LegalizationId).HasColumnName("Legalization_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameEng).HasMaxLength(100);

                entity.Property(e => e.RegionId).HasColumnName("Region_Id");

                entity.HasOne(d => d.Legalization)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.LegalizationId)
                    .HasConstraintName("FK_LegalizationCountry");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RegionCountry");
            });

            modelBuilder.Entity<CustomObjectsId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CustomObjectsId");

                entity.Property(e => e.DateChange)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ObjectName)
                    .HasMaxLength(255)
                    .HasColumnName("objectName");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.CreatorUnityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CurrentBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.FullAddress).HasMaxLength(256);

                entity.Property(e => e.OutDate).HasColumnType("datetime");

                entity.Property(e => e.OutNumber).HasMaxLength(100);

                entity.HasOne(d => d.CurrentBarCodeNavigation)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.CurrentBarCode)
                    .HasConstraintName("FK_Deliveries_Applies");
            });

            modelBuilder.Entity<DocxTemplate>(entity =>
            {
                entity.HasIndex(e => new { e.Group, e.FileName }, "UX_DocxTemplates_Group_FileName")
                    .IsUnique();

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedById).HasColumnName("ModifiedBy_Id");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Param).HasMaxLength(50);

                entity.Property(e => e.ReceiverAs).HasDefaultValueSql("((1))");

                entity.Property(e => e.Signers).HasMaxLength(500);
            });

            modelBuilder.Entity<Duplicate>(entity =>
            {
                entity.HasKey(e => e.BarCode)
                    .HasName("PK_Duplicate");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.ActId).HasColumnName("Act_Id");

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Block)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Building)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.Corpus)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedBy_Id");

                entity.Property(e => e.CreatorCountryId).HasColumnName("CreatorCountry_Id");

                entity.Property(e => e.DocCountryId).HasColumnName("DocCountry_Id");

                entity.Property(e => e.DocumentDate).HasMaxLength(20);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentType_Id");

                entity.Property(e => e.Flat)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.HandoutById).HasColumnName("HandoutBy_Id");

                entity.Property(e => e.HandoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastBlankNumber)
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.MailIndex).HasMaxLength(10);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");

                entity.Property(e => e.ReturnOriginalsFormId).HasColumnName("ReturnOriginalsForm_Id");

                entity.Property(e => e.ReturnOriginalsPostAddress).HasMaxLength(200);

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_Id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Street)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Uin).HasMaxLength(50);

                entity.Property(e => e.UinCreateTime).HasColumnType("datetime");

                entity.Property(e => e.UinRemoveTime).HasColumnType("datetime");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.ActId)
                    .HasConstraintName("FK_Duplicates_Acts");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Duplicates_Applies");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DuplicateCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_DuplicateCreatedBy_Users");

                entity.HasOne(d => d.CreatorCountry)
                    .WithMany(p => p.DuplicateCreatorCountries)
                    .HasForeignKey(d => d.CreatorCountryId)
                    .HasConstraintName("FK_Duplicates_CreatorCountries");

                entity.HasOne(d => d.DocCountryNavigation)
                    .WithMany(p => p.DuplicateDocCountryNavigations)
                    .HasForeignKey(d => d.DocCountryId)
                    .HasConstraintName("FK_Duplicates_DocCountries");

                entity.HasOne(d => d.DocumentTypeNavigation)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_Duplicates_ApplyDocTypes");

                entity.HasOne(d => d.HandoutBy)
                    .WithMany(p => p.DuplicateHandoutBies)
                    .HasForeignKey(d => d.HandoutById)
                    .HasConstraintName("FK_DuplicateHandoutBy_Users");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Duplicates_Payments");

                entity.HasOne(d => d.ReturnOriginalsForm)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.ReturnOriginalsFormId)
                    .HasConstraintName("FK_Duplicates_ReturnOriginalsForm");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Duplicates)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Duplicates_DuplicateStatuses");
            });

            modelBuilder.Entity<DuplicateStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEng).HasMaxLength(50);
            });

            modelBuilder.Entity<DuplicateStatusHistory>(entity =>
            {
                entity.HasIndex(e => e.ChangeTime, "IX_DuplicateStatusHistories");

                entity.Property(e => e.ChangeTime).HasColumnType("datetime");

                entity.Property(e => e.DuplicateBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Duplicate_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.PrevStatusId).HasColumnName("PrevStatus_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.DuplicateBarCodeNavigation)
                    .WithMany(p => p.DuplicateStatusHistories)
                    .HasForeignKey(d => d.DuplicateBarCode)
                    .HasConstraintName("FK_DuplicateStatusHistories_Duplicates");

                entity.HasOne(d => d.PrevStatus)
                    .WithMany(p => p.DuplicateStatusHistoryPrevStatuses)
                    .HasForeignKey(d => d.PrevStatusId)
                    .HasConstraintName("FK_DuplicateStatusHistories_DuplicatePrevStatuses");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.DuplicateStatusHistoryStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DuplicateStatusHistories_DuplicateStatuses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DuplicateStatusHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DuplicateStatusHistories_Users");
            });

            modelBuilder.Entity<EiisCountry>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Code2)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Code3)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.FullName).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<EiisPackage>(entity =>
            {
                entity.HasKey(e => e.CreatedId);

                entity.HasIndex(e => e.CompletedId, "IX_EiisPackages_CompletedId");

                entity.HasIndex(e => e.SessionId, "IX_EiisPackages_SessionId");

                entity.Property(e => e.CreatedId).ValueGeneratedNever();

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.EiisPackages)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_EiisPackages_EiisSessions");
            });

            modelBuilder.Entity<EiisPackagePart>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.PartNum });

                entity.Property(e => e.PartText)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.EiisPackageParts)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_EiisPackageParts_EiisPackages");
            });

            modelBuilder.Entity<EiisSession>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.Property(e => e.SessionId).ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<EiisUinEvent>(entity =>
            {
                entity.HasKey(e => new { e.Uin, e.Operation });

                entity.Property(e => e.Uin).HasMaxLength(50);

                entity.Property(e => e.Operation).HasMaxLength(10);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscountDate).HasColumnType("datetime");

                entity.Property(e => e.DocNumber).HasMaxLength(150);

                entity.Property(e => e.DoneTime).HasColumnType("datetime");

                entity.Property(e => e.EpguId).HasMaxLength(50);

                entity.Property(e => e.PayerName).HasMaxLength(200);

                entity.Property(e => e.ResultCode).HasMaxLength(10);
            });

            modelBuilder.Entity<EnglishResource>(entity =>
            {
                entity.HasKey(e => e.Rus);

                entity.Property(e => e.Rus).HasMaxLength(400);

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Eng)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<EpguEvent>(entity =>
            {
                entity.HasIndex(e => e.CreateTime, "IX_EpguEvents_CreateTime");

                entity.HasIndex(e => e.ThreatTime, "IX_EpguEvents_ThreatTime");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.EpguCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.ThreatTime).HasColumnType("datetime");

                entity.Property(e => e.Uin).HasMaxLength(50);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ExpertDepartment>(entity =>
            {
                entity.HasIndex(e => e.ParentId, "IX_FK_ExpertDepartmentExpertDepartment");

                entity.HasIndex(e => e.OrganizationId, "IX_FK_ExpertOrganizationExpertDepartment");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OrganizationId).HasColumnName("Organization_Id");

                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ExpertDepartments)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_ExpertOrganizationExpertDepartment");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ExpertDepartmentExpertDepartment");
            });

            modelBuilder.Entity<ExpertOrganization>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Expertize>(entity =>
            {
                entity.HasIndex(e => e.ApplyBarCode, "IX_FK_ApplyExpertize");

                entity.HasIndex(e => e.ManagerId, "IX_FK_UserExpertize");

                entity.HasIndex(e => e.ExpertId, "IX_FK_UserExpertize1");

                entity.Property(e => e.ApplyBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.AssignDate).HasColumnType("datetime");

                entity.Property(e => e.ExpertId).HasColumnName("Expert_Id");

                entity.Property(e => e.ManagerId).HasColumnName("Manager_Id");

                entity.Property(e => e.TermDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Expertizes)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .HasConstraintName("FK_ApplyExpertize");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.ExpertizeExperts)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExpertize1");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ExpertizeManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExpertize");
            });

            modelBuilder.Entity<ExpertizeStatus>(entity =>
            {
                entity.Property(e => e.AllowStepToStatuses).HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StepAction).HasMaxLength(256);

                entity.Property(e => e.ViewGrants)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExpertizeStatusHistory>(entity =>
            {
                entity.HasIndex(e => e.ExpertizeId, "IX_FK_ExpertizeExpertizeStatusHistory");

                entity.HasIndex(e => e.StatusId, "IX_FK_ExpertizeStatusExpertizeStatusHistory");

                entity.HasIndex(e => e.UserId, "IX_FK_UserExpertizeStatusHistory");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ChangeTime).HasColumnType("datetime");

                entity.Property(e => e.ExpertizeId).HasColumnName("Expertize_Id");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Expertize)
                    .WithMany(p => p.ExpertizeStatusHistories)
                    .HasForeignKey(d => d.ExpertizeId)
                    .HasConstraintName("FK_ExpertizeExpertizeStatusHistory");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ExpertizeStatusHistories)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpertizeStatusExpertizeStatusHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExpertizeStatusHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExpertizeStatusHistory");
            });

            modelBuilder.Entity<FisReception>(entity =>
            {
                entity.ToTable("FisReception");

                entity.HasIndex(e => e.FirstName, "IX_FisReception_FirstName");

                entity.HasIndex(e => e.Surname, "IX_FisReception_Surname");

                entity.Property(e => e.Act).HasMaxLength(3000);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BlankNum).HasMaxLength(50);

                entity.Property(e => e.Citizenship).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(500);

                entity.Property(e => e.ConclusionId).HasColumnName("Conclusion_Id");

                entity.Property(e => e.DepartCode).HasMaxLength(100);

                entity.Property(e => e.DocDate).HasColumnType("datetime");

                entity.Property(e => e.DocNum).HasMaxLength(100);

                entity.Property(e => e.DocOrg).HasMaxLength(2000);

                entity.Property(e => e.DocSer).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(400);

                entity.Property(e => e.ForControlCount).HasMaxLength(150);

                entity.Property(e => e.ForSpecialQuota).HasMaxLength(150);

                entity.Property(e => e.Gender).HasMaxLength(500);

                entity.Property(e => e.LastName).HasMaxLength(400);

                entity.Property(e => e.LearnDepart).HasMaxLength(250);

                entity.Property(e => e.LearnLevel).HasMaxLength(250);

                entity.Property(e => e.LearnPrograms).HasMaxLength(2000);

                entity.Property(e => e.PassportDate).HasColumnType("datetime");

                entity.Property(e => e.PassportGivenBy).HasMaxLength(500);

                entity.Property(e => e.PassportNum).HasMaxLength(150);

                entity.Property(e => e.PassportSer).HasMaxLength(50);

                entity.Property(e => e.PassportType).HasMaxLength(500);

                entity.Property(e => e.Region).HasMaxLength(500);

                entity.Property(e => e.Surname).HasMaxLength(400);

                entity.Property(e => e.SvidDate).HasColumnType("datetime");

                entity.Property(e => e.SvidNum).HasMaxLength(50);

                entity.Property(e => e.ToOrg).HasMaxLength(2000);
            });

            modelBuilder.Entity<Foundation>(entity =>
            {
                entity.HasIndex(e => e.CountryId, "IX_FK_CountryFoundation");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Fax).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phones).HasMaxLength(250);

                entity.Property(e => e.Uri).HasMaxLength(250);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Foundations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryFoundation");
            });

            modelBuilder.Entity<InfoLetter>(entity =>
            {
                entity.Property(e => e.ApplyBarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.DocxFileName).HasMaxLength(256);

                entity.Property(e => e.OutDate).HasColumnType("datetime");

                entity.Property(e => e.OutNum).HasMaxLength(100);

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.SignerName).HasMaxLength(200);

                entity.Property(e => e.SignerPosition).HasMaxLength(100);

                entity.Property(e => e.TemplateId).HasColumnName("Template_Id");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.InfoLetters)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfoLetters_Applies");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.InfoLetters)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfoLetters_DocxTemplates");
            });

            modelBuilder.Entity<LearnCourse>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<LearnLevel>(entity =>
            {
                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LearnLevelMsko>(entity =>
            {
                entity.ToTable("LearnLevelMSKOs");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Code1997).HasMaxLength(20);

                entity.Property(e => e.Code2011).HasMaxLength(20);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Type).HasDefaultValueSql("((1997))");
            });

            modelBuilder.Entity<LearnProgram>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<LearnProgramDetail>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_FK_LearnProgramDetailLearnCourse");

                entity.HasIndex(e => e.LearnProgramLearnProgramDetailLearnProgramDetailId, "IX_FK_LearnProgramLearnProgramDetail");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.LearnProgramLearnProgramDetailLearnProgramDetailId).HasColumnName("LearnProgramLearnProgramDetail_LearnProgramDetail_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.LearnProgramDetails)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_LearnProgramDetailLearnCourse");

                entity.HasOne(d => d.LearnProgramLearnProgramDetailLearnProgramDetail)
                    .WithMany(p => p.LearnProgramDetails)
                    .HasForeignKey(d => d.LearnProgramLearnProgramDetailLearnProgramDetailId)
                    .HasConstraintName("FK_LearnProgramLearnProgramDetail");
            });

            modelBuilder.Entity<LegalAct>(entity =>
            {
                entity.HasIndex(e => e.TypeId, "IX_FK_LegalActTypeLegalAct");

                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.LegalActs)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_LegalActTypeLegalAct");
            });

            modelBuilder.Entity<LegalActType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LegalActVersion>(entity =>
            {
                entity.HasIndex(e => e.LegalActId, "IX_FK_LegalActLegalActVersion");

                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.LegalActId).HasColumnName("LegalAct_Id");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.LegalAct)
                    .WithMany(p => p.LegalActVersions)
                    .HasForeignKey(d => d.LegalActId)
                    .HasConstraintName("FK_LegalActLegalActVersion");
            });

            modelBuilder.Entity<Legalization>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasIndex(e => e.ApplyBarCode, "IX_FK_LogApply");

                entity.HasIndex(e => e.UserId, "IX_FK_UserLog");

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Operation).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LogApply");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLog");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ForUserId).HasColumnName("ForUser_Id");

                entity.Property(e => e.ReadsOn).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(200);

                entity.HasOne(d => d.ForUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ForUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Users");
            });

            modelBuilder.Entity<MigrConfirm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Migr_Confirm");

                entity.Property(e => e.CountryId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Country_ID");

                entity.Property(e => e.DocsId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Docs_Id");

                entity.Property(e => e.Order).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.SchoolId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("School_ID");
            });

            modelBuilder.Entity<NostrificationV2Expertize>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NostrificationV2_Expertizes");

                entity.Property(e => e.Aday)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ADay")
                    .IsFixedLength(true);

                entity.Property(e => e.Aim).HasMaxLength(200);

                entity.Property(e => e.Amonth)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("AMonth")
                    .IsFixedLength(true);

                entity.Property(e => e.Ayear).HasColumnName("AYear");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.CbarCode)
                    .HasMaxLength(12)
                    .HasColumnName("CBarCode");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.IdoCountry)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LearnForm).HasMaxLength(50);

                entity.Property(e => e.LearnLevel).HasMaxLength(100);

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.StandardDocumentType).HasMaxLength(200);
            });

            modelBuilder.Entity<NostrificationV2Request>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NostrificationV2_Requests");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.CurrentBarCode).HasMaxLength(12);

                entity.Property(e => e.Idate)
                    .HasColumnType("date")
                    .HasColumnName("IDate");

                entity.Property(e => e.Odate)
                    .HasColumnType("date")
                    .HasColumnName("ODate");

                entity.Property(e => e.OutNum)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SentDate).HasColumnType("date");

                entity.Property(e => e.ToName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.BankDate, "IX_Payments_BankDate");

                entity.HasIndex(e => e.DocDate, "IX_Payments_DocDate");

                entity.Property(e => e.BankDate).HasColumnType("datetime");

                entity.Property(e => e.DocDate).HasColumnType("datetime");

                entity.Property(e => e.ImpotDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PayInfo).IsRequired();

                entity.Property(e => e.Payer).IsRequired();

                entity.Property(e => e.PayerInfo).IsRequired();
            });

            modelBuilder.Entity<PhoneDuty>(entity =>
            {
                entity.ToTable("PhoneDuty");

                entity.Property(e => e.AcceptDate).HasColumnType("date");

                entity.Property(e => e.CancelComments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CancelDate).HasColumnType("date");

                entity.Property(e => e.CancelManagerId).HasColumnName("CancelManager_Id");

                entity.Property(e => e.DutyDate).HasColumnType("date");

                entity.Property(e => e.DutyManagerId).HasColumnName("DutyManager_Id");

                entity.Property(e => e.DutyUserId).HasColumnName("DutyUser_Id");

                entity.Property(e => e.IsActual)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CancelManager)
                    .WithMany(p => p.PhoneDutyCancelManagers)
                    .HasForeignKey(d => d.CancelManagerId)
                    .HasConstraintName("FK_PhoneDuty_Users2");

                entity.HasOne(d => d.DutyManager)
                    .WithMany(p => p.PhoneDutyDutyManagers)
                    .HasForeignKey(d => d.DutyManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneDuty_Users1");

                entity.HasOne(d => d.DutyUser)
                    .WithMany(p => p.PhoneDutyDutyUsers)
                    .HasForeignKey(d => d.DutyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneDuty_Users");
            });

            modelBuilder.Entity<PreferredPage>(entity =>
            {
                entity.ToTable("PreferredPage");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PreferredPages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferredPage_Users");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Ratification>(entity =>
            {
                entity.HasIndex(e => e.AgreementId, "IX_FK_AgreementRatification");

                entity.HasIndex(e => e.CountryId, "IX_FK_CountryRatification");

                entity.Property(e => e.AcceptDate).HasColumnType("datetime");

                entity.Property(e => e.AgreementId).HasColumnName("Agreement_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.RejectDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.Ratifications)
                    .HasForeignKey(d => d.AgreementId)
                    .HasConstraintName("FK_AgreementRatification");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Ratifications)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CountryRatification");
            });

            modelBuilder.Entity<ReLinkSchoolApply>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.Cn).HasColumnName("cn");

                entity.Property(e => e.NewId).HasColumnName("newId");

                entity.Property(e => e.OldId).HasColumnName("oldId");
            });

            modelBuilder.Entity<ReLinkSchoolConclusion>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Cn).HasColumnName("cn");

                entity.Property(e => e.NewId).HasColumnName("newId");

                entity.Property(e => e.OldId).HasColumnName("oldId");
            });

            modelBuilder.Entity<ReLinkSchoolSpeciality>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Cn).HasColumnName("cn");

                entity.Property(e => e.NewId).HasColumnName("newId");

                entity.Property(e => e.OldId).HasColumnName("oldId");
            });

            modelBuilder.Entity<ReLinkSchoolToDelete>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ReLinkSchoolToDelete");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PostIndex).HasMaxLength(10);

                entity.Property(e => e.Shingle).HasMaxLength(250);

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");
            });

            modelBuilder.Entity<ReceptionQueue>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TimeInterval)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReceptionQueueBarCode>(entity =>
            {
                entity.HasKey(e => new { e.QueueId, e.BarCode })
                    .HasName("PK_ReceptionQueueApplies");

                entity.Property(e => e.QueueId).HasColumnName("Queue_Id");

                entity.Property(e => e.BarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.ReceptionQueueBarCodes)
                    .HasForeignKey(d => d.QueueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceptionQueueBarCodes_ReceptionQueues");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ReglamentEtap>(entity =>
            {
                entity.HasIndex(e => e.OrderNum, "IX_ReglamentEtaps_OrderNum");

                entity.Property(e => e.MaxTerm).HasDefaultValueSql("((1))");

                entity.Property(e => e.MinTerm).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'-')");

                entity.Property(e => e.OrderNum).HasDefaultValueSql("((1))");

                entity.Property(e => e.Required)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Reporting>(entity =>
            {
                entity.Property(e => e.DefaultUpdateTime).HasDefaultValueSql("((2))");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Type).HasDefaultValueSql("((2))");

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasIndex(e => e.ApplyBarCode, "IX_FK_ApplyRequest");

                entity.HasIndex(e => e.StatusId, "IX_FK_RequestStatusRequest");

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .HasColumnName("Apply_BarCode")
                    .IsFixedLength(true);

                entity.Property(e => e.DocxCreatedById).HasColumnName("DocxCreatedBy_Id");

                entity.Property(e => e.DocxCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocxFileName).HasMaxLength(256);

                entity.Property(e => e.DocxModifiedById).HasColumnName("DocxModifiedBy_Id");

                entity.Property(e => e.DocxModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.DocxTemplateId).HasColumnName("DocxTemplate_Id");

                entity.Property(e => e.InDate).HasColumnType("datetime");

                entity.Property(e => e.InNum).HasMaxLength(100);

                entity.Property(e => e.OutDate).HasColumnType("datetime");

                entity.Property(e => e.OutNum).HasMaxLength(100);

                entity.Property(e => e.ReceivedTime).HasColumnType("datetime");

                entity.Property(e => e.RespCreatedById).HasColumnName("RespCreatedBy_Id");

                entity.Property(e => e.RespCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RespFileName).HasMaxLength(256);

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.ToAddress).HasMaxLength(256);

                entity.Property(e => e.ToEmail).HasMaxLength(256);

                entity.Property(e => e.ToName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ToPostIndex).HasMaxLength(10);

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ApplyRequest");

                entity.HasOne(d => d.DocxTemplate)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.DocxTemplateId)
                    .HasConstraintName("FK_Requests_DocxTemplates");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestStatusRequest");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.Property(e => e.AllowStepToStatuses).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StepAction).HasMaxLength(256);
            });

            modelBuilder.Entity<RequestTemplate>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Grants).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StepApplyStatusesString).HasMaxLength(250);

                entity.Property(e => e.ViewApplyStatusesString).HasMaxLength(250);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.CountryId, "IX_FK_CountrySchool");

                entity.HasIndex(e => e.TypeId, "IX_FK_SchoolTypeSchool");

                entity.HasIndex(e => e.Name, "IX_Schools_Name");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PostIndex).HasMaxLength(10);

                entity.Property(e => e.Shingle).HasMaxLength(250);

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_CountrySchool");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_SchoolTypeSchool");
            });

            modelBuilder.Entity<SchoolType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameEng).HasMaxLength(100);
            });

            modelBuilder.Entity<SchoolsSpeciality>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.SpecialityId });

                entity.Property(e => e.SchoolId).HasColumnName("School_Id");

                entity.Property(e => e.SpecialityId).HasColumnName("Speciality_Id");

                entity.Property(e => e.EquivalentSpecialityId).HasColumnName("EquivalentSpeciality_Id");

                entity.HasOne(d => d.EquivalentSpeciality)
                    .WithMany(p => p.SchoolsSpecialities)
                    .HasForeignKey(d => d.EquivalentSpecialityId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SchoolsSpecialities_StandardSpecialities");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolsSpecialities)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolsSpecialities_Schools");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.SchoolsSpecialities)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK_SchoolsSpecialities_Specialities");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<ShedulerTask>(entity =>
            {
                entity.Property(e => e.Group).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastExec).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NextExec).HasColumnType("datetime");

                entity.Property(e => e.Period).HasDefaultValueSql("((60))");

                entity.Property(e => e.ToMessages)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Smev3Job>(entity =>
            {
                entity.HasIndex(e => e.CreateTime, "IX_Smev3Jobs_CreateTime");

                entity.HasIndex(e => e.SendRequestTime, "IX_Smev3Jobs_SendRequestTime");

                entity.Property(e => e.AppNum).HasMaxLength(100);

                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.GetResponseContext).HasColumnType("text");

                entity.Property(e => e.GetResponseStatus).HasMaxLength(100);

                entity.Property(e => e.GetResponseTime).HasColumnType("datetime");

                entity.Property(e => e.Parameters).HasColumnType("text");

                entity.Property(e => e.SendRequestStatus).HasMaxLength(100);

                entity.Property(e => e.SendRequestTime).HasColumnType("datetime");

                entity.Property(e => e.Uri).HasMaxLength(500);
            });

            modelBuilder.Entity<SpecialitiesLearnProgram>(entity =>
            {
                entity.HasKey(e => new { e.SpecialitiesId, e.LearnProgramsId })
                    .IsClustered(false);

                entity.HasIndex(e => e.LearnProgramsId, "IX_FK_SpecialityLearnProgram_LearnProgram");

                entity.Property(e => e.SpecialitiesId).HasColumnName("Specialities_Id");

                entity.Property(e => e.LearnProgramsId).HasColumnName("LearnPrograms_Id");

                entity.HasOne(d => d.LearnPrograms)
                    .WithMany(p => p.SpecialitiesLearnPrograms)
                    .HasForeignKey(d => d.LearnProgramsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialityLearnProgram_LearnProgram");

                entity.HasOne(d => d.Specialities)
                    .WithMany(p => p.SpecialitiesLearnPrograms)
                    .HasForeignKey(d => d.SpecialitiesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialityLearnProgram_Speciality");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.Property(e => e.Conformity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<StandardDocType>(entity =>
            {
                entity.HasIndex(e => e.LearnLevelId, "IX_FK_LearnLevelStandardDocType");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LearnLevelId).HasColumnName("LearnLevel_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PublicName).HasMaxLength(255);

                entity.Property(e => e.ShortName).HasMaxLength(10);

                entity.HasOne(d => d.LearnLevel)
                    .WithMany(p => p.StandardDocTypes)
                    .HasForeignKey(d => d.LearnLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnLevelStandardDocType");
            });

            modelBuilder.Entity<StandardLearnProgram>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<StandardLearnProgramDetail>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_FK_LearnCourseStandardLearnProgramDetail");

                entity.HasIndex(e => e.StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetailId, "IX_FK_StandardLearnProgramStandardLearnProgramDetail");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetailId).HasColumnName("StandardLearnProgramStandardLearnProgramDetail_StandardLearnProgramDetail_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StandardLearnProgramDetails)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_LearnCourseStandardLearnProgramDetail");

                entity.HasOne(d => d.StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetail)
                    .WithMany(p => p.StandardLearnProgramDetails)
                    .HasForeignKey(d => d.StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandardLearnProgramStandardLearnProgramDetail");
            });

            modelBuilder.Entity<StandardQualification>(entity =>
            {
                entity.HasIndex(e => e.StandardSpecialityId, "IX_FK_StandardQualificationStandardSpeciality");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StandardSpecialityId).HasColumnName("StandardSpeciality_Id");

                entity.HasOne(d => d.StandardSpeciality)
                    .WithMany(p => p.StandardQualifications)
                    .HasForeignKey(d => d.StandardSpecialityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandardQualificationStandardSpeciality");
            });

            modelBuilder.Entity<StandardSpecialitiesCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Manual).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Number).IsRequired();
            });

            modelBuilder.Entity<StandardSpecialitiesGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<StandardSpeciality>(entity =>
            {
                entity.HasIndex(e => e.StandardSpecialitiesCategoryId, "IX_FK_StandardSpecialitiesCategoryStandardSpeciality");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.GroupId).HasColumnName("Group_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StandardSpecialitiesCategoryId).HasColumnName("StandardSpecialitiesCategory_Id");

                entity.Property(e => e.Ugs).HasMaxLength(500);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.StandardSpecialities)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_StandardSpecialities_StandardSpecialitiesGroups");

                entity.HasOne(d => d.StandardSpecialitiesCategory)
                    .WithMany(p => p.StandardSpecialities)
                    .HasForeignKey(d => d.StandardSpecialitiesCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandardSpecialitiesCategoryStandardSpeciality");
            });

            modelBuilder.Entity<StandardSpecialityStandardLearnProgram>(entity =>
            {
                entity.HasKey(e => new { e.SpecialitiesId, e.LearnProgramsId })
                    .IsClustered(false);

                entity.ToTable("StandardSpecialityStandardLearnProgram");

                entity.HasIndex(e => e.LearnProgramsId, "IX_FK_StandardSpecialityStandardLearnProgram_StandardLearnProgram");

                entity.Property(e => e.SpecialitiesId).HasColumnName("Specialities_Id");

                entity.Property(e => e.LearnProgramsId).HasColumnName("LearnPrograms_Id");

                entity.HasOne(d => d.LearnPrograms)
                    .WithMany(p => p.StandardSpecialityStandardLearnPrograms)
                    .HasForeignKey(d => d.LearnProgramsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandardSpecialityStandardLearnProgram_StandardLearnProgram");

                entity.HasOne(d => d.Specialities)
                    .WithMany(p => p.StandardSpecialityStandardLearnPrograms)
                    .HasForeignKey(d => d.SpecialitiesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StandardSpecialityStandardLearnProgram_StandardSpeciality");
            });

            modelBuilder.Entity<Svid>(entity =>
            {
                entity.Property(e => e.ApplyBarCode)
                    .HasMaxLength(12)
                    .IsFixedLength(true);

                entity.Property(e => e.CertFromDate).HasColumnType("datetime");

                entity.Property(e => e.CertNumber).HasMaxLength(250);

                entity.Property(e => e.CertToDate).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FileData).HasColumnType("text");

                entity.Property(e => e.PdfDataBase64).HasColumnType("text");

                entity.Property(e => e.PdfHashBase64).HasColumnType("text");

                entity.Property(e => e.PdfSignature).HasColumnType("text");

                entity.Property(e => e.SvidNum)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Xml).HasColumnType("text");

                entity.HasOne(d => d.ApplyBarCodeNavigation)
                    .WithMany(p => p.Svids)
                    .HasForeignKey(d => d.ApplyBarCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Svids_Applies");
            });

            modelBuilder.Entity<TempApplyCreator>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temp_ApplyCreators");

                entity.Property(e => e.DeleteTerm).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(765);

                entity.Property(e => e.FullName).HasMaxLength(765);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(765);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<TmpSchool>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Tmp_School");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.NewId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("New_id");
            });

            modelBuilder.Entity<UploadedFile>(entity =>
            {
                entity.HasIndex(e => e.Deleted, "IX_UploadedFiles_Deleted");

                entity.HasIndex(e => e.FileName, "IX_UploadedFiles_FileName");

                entity.HasIndex(e => e.Inserted, "IX_UploadedFiles_Inserted");

                entity.HasIndex(e => e.UploadSessionId, "IX_UploadedFiles_UploadSessionId");

                entity.HasIndex(e => e.UploadTime, "IX_UploadedFiles_UploadTime");

                entity.Property(e => e.CreatorEmail).HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(256);

                entity.Property(e => e.Source)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UploadTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UploadedFiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UploadedFiles_Users");
            });

            modelBuilder.Entity<UploadedFileRequest>(entity =>
            {
                entity.HasKey(e => new { e.AttachmentsId, e.UploadedFileRequestUploadedFileId })
                    .IsClustered(false);

                entity.ToTable("UploadedFileRequest");

                entity.HasIndex(e => e.UploadedFileRequestUploadedFileId, "IX_FK_UploadedFileRequest_Request");

                entity.Property(e => e.AttachmentsId).HasColumnName("Attachments_Id");

                entity.Property(e => e.UploadedFileRequestUploadedFileId).HasColumnName("UploadedFileRequest_UploadedFile_Id");

                entity.HasOne(d => d.Attachments)
                    .WithMany(p => p.UploadedFileRequests)
                    .HasForeignKey(d => d.AttachmentsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UploadedFileRequest_UploadedFile");

                entity.HasOne(d => d.UploadedFileRequestUploadedFile)
                    .WithMany(p => p.UploadedFileRequests)
                    .HasForeignKey(d => d.UploadedFileRequestUploadedFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UploadedFileRequest_Request");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.ExpertDepartmentId, "IX_FK_ExpertDepartmentUser");

                entity.Property(e => e.Blocked).HasColumnName("blocked");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Removed).HasColumnName("removed");

                entity.Property(e => e.Snils).HasMaxLength(20);

                entity.HasOne(d => d.ExpertDepartment)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ExpertDepartmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ExpertDepartmentUser");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.RolesId })
                    .IsClustered(false);

                entity.ToTable("UserRole");

                entity.HasIndex(e => e.RolesId, "IX_FK_UserRole_Role");

                entity.Property(e => e.UsersId).HasColumnName("Users_Id");

                entity.Property(e => e.RolesId).HasColumnName("Roles_Id");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RolesId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
