using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class User
    {
        public User()
        {
            Acts = new HashSet<Act>();
            ApplyComments = new HashSet<ApplyComment>();
            ApplyConclusionResultUsers = new HashSet<Apply>();
            ApplyRonStatusHistories = new HashSet<ApplyRonStatusHistory>();
            ApplyRons = new HashSet<ApplyRon>();
            ApplyStatusChangeUsers = new HashSet<Apply>();
            ApplyStatusHistories = new HashSet<ApplyStatusHistory>();
            Blanks = new HashSet<Blank>();
            ChiefRoomFrames = new HashSet<ChiefRoomFrame>();
            Conclusions = new HashSet<Conclusion>();
            CorrespondentionInputCreatedBies = new HashSet<CorrespondentionInput>();
            CorrespondentionInputDeletedBies = new HashSet<CorrespondentionInput>();
            CorrespondentionInputModifiedBies = new HashSet<CorrespondentionInput>();
            CorrespondentionInputPerformers = new HashSet<CorrespondentionInput>();
            CorrespondentionOutputCreatedBies = new HashSet<CorrespondentionOutput>();
            CorrespondentionOutputDeletedBies = new HashSet<CorrespondentionOutput>();
            CorrespondentionOutputModifiedBies = new HashSet<CorrespondentionOutput>();
            CorrespondentionOutputPerformers = new HashSet<CorrespondentionOutput>();
            CorrespondentionOutputSigners = new HashSet<CorrespondentionOutput>();
            DuplicateCreatedBies = new HashSet<Duplicate>();
            DuplicateHandoutBies = new HashSet<Duplicate>();
            DuplicateStatusHistories = new HashSet<DuplicateStatusHistory>();
            ExpertizeExperts = new HashSet<Expertize>();
            ExpertizeManagers = new HashSet<Expertize>();
            ExpertizeStatusHistories = new HashSet<ExpertizeStatusHistory>();
            Logs = new HashSet<Log>();
            Messages = new HashSet<Message>();
            PhoneDutyCancelManagers = new HashSet<PhoneDuty>();
            PhoneDutyDutyManagers = new HashSet<PhoneDuty>();
            PhoneDutyDutyUsers = new HashSet<PhoneDuty>();
            PreferredPages = new HashSet<PreferredPage>();
            UploadedFiles = new HashSet<UploadedFile>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Blocked { get; set; }
        public bool Removed { get; set; }
        public int? ExpertDepartmentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public Guid? ForTest { get; set; }
        public string Snils { get; set; }

        public virtual ExpertDepartment ExpertDepartment { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual ICollection<ApplyComment> ApplyComments { get; set; }
        public virtual ICollection<Apply> ApplyConclusionResultUsers { get; set; }
        public virtual ICollection<ApplyRonStatusHistory> ApplyRonStatusHistories { get; set; }
        public virtual ICollection<ApplyRon> ApplyRons { get; set; }
        public virtual ICollection<Apply> ApplyStatusChangeUsers { get; set; }
        public virtual ICollection<ApplyStatusHistory> ApplyStatusHistories { get; set; }
        public virtual ICollection<Blank> Blanks { get; set; }
        public virtual ICollection<ChiefRoomFrame> ChiefRoomFrames { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<CorrespondentionInput> CorrespondentionInputCreatedBies { get; set; }
        public virtual ICollection<CorrespondentionInput> CorrespondentionInputDeletedBies { get; set; }
        public virtual ICollection<CorrespondentionInput> CorrespondentionInputModifiedBies { get; set; }
        public virtual ICollection<CorrespondentionInput> CorrespondentionInputPerformers { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputCreatedBies { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputDeletedBies { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputModifiedBies { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputPerformers { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputSigners { get; set; }
        public virtual ICollection<Duplicate> DuplicateCreatedBies { get; set; }
        public virtual ICollection<Duplicate> DuplicateHandoutBies { get; set; }
        public virtual ICollection<DuplicateStatusHistory> DuplicateStatusHistories { get; set; }
        public virtual ICollection<Expertize> ExpertizeExperts { get; set; }
        public virtual ICollection<Expertize> ExpertizeManagers { get; set; }
        public virtual ICollection<ExpertizeStatusHistory> ExpertizeStatusHistories { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<PhoneDuty> PhoneDutyCancelManagers { get; set; }
        public virtual ICollection<PhoneDuty> PhoneDutyDutyManagers { get; set; }
        public virtual ICollection<PhoneDuty> PhoneDutyDutyUsers { get; set; }
        public virtual ICollection<PreferredPage> PreferredPages { get; set; }
        public virtual ICollection<UploadedFile> UploadedFiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
