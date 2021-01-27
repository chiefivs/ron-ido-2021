using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class UploadedFile
    {
        public UploadedFile()
        {
            ApplyCommentUploadedFiles = new HashSet<ApplyCommentUploadedFile>();
            ApplyDocuments = new HashSet<ApplyDocument>();
            ApplyRonDocuments = new HashSet<ApplyRonDocument>();
            ApplyRonLetters = new HashSet<ApplyRonLetter>();
            CorrespondentionInputAttaches = new HashSet<CorrespondentionInputAttach>();
            CorrespondentionOutputAttaches = new HashSet<CorrespondentionOutputAttach>();
            UploadedFileRequests = new HashSet<UploadedFileRequest>();
        }

        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public bool Inserted { get; set; }
        public bool Deleted { get; set; }
        public DateTime? UploadTime { get; set; }
        public Guid? UploadSessionId { get; set; }
        public string Source { get; set; }
        public string CreatorEmail { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ApplyCommentUploadedFile> ApplyCommentUploadedFiles { get; set; }
        public virtual ICollection<ApplyDocument> ApplyDocuments { get; set; }
        public virtual ICollection<ApplyRonDocument> ApplyRonDocuments { get; set; }
        public virtual ICollection<ApplyRonLetter> ApplyRonLetters { get; set; }
        public virtual ICollection<CorrespondentionInputAttach> CorrespondentionInputAttaches { get; set; }
        public virtual ICollection<CorrespondentionOutputAttach> CorrespondentionOutputAttaches { get; set; }
        public virtual ICollection<UploadedFileRequest> UploadedFileRequests { get; set; }
    }
}
