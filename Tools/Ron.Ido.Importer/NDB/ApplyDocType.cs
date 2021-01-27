using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyDocType
    {
        public ApplyDocType()
        {
            AgreementApplyDocTypes = new HashSet<AgreementApplyDocType>();
            Applies = new HashSet<Apply>();
            ApplyRons = new HashSet<ApplyRon>();
            ConclusionDocumentTypes = new HashSet<Conclusion>();
            ConclusionSourceDocumentTypeForExperts = new HashSet<Conclusion>();
            Duplicates = new HashSet<Duplicate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public int? LearnLevelId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string NameEng { get; set; }

        public virtual LearnLevel LearnLevel { get; set; }
        public virtual ICollection<AgreementApplyDocType> AgreementApplyDocTypes { get; set; }
        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<ApplyRon> ApplyRons { get; set; }
        public virtual ICollection<Conclusion> ConclusionDocumentTypes { get; set; }
        public virtual ICollection<Conclusion> ConclusionSourceDocumentTypeForExperts { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
    }
}
