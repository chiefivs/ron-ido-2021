using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Act
    {
        public Act()
        {
            Applies = new HashSet<Apply>();
            Duplicates = new HashSet<Duplicate>();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Number { get; set; }
        public DateTime? SignDate { get; set; }
        public int StatusId { get; set; }
        public int ConfirmResultId { get; set; }
        public int? Count { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? ProjectSignDate { get; set; }
        public DateTime? OutputDate { get; set; }
        public string Coordination { get; set; }
        public string Sight { get; set; }
        public DateTime? InputDate { get; set; }
        public DateTime? GiveToRonDate { get; set; }
        public DateTime? PreCoordinationDate { get; set; }
        public int? DigSvidCreatorId { get; set; }

        public virtual ActConfirmResult ConfirmResult { get; set; }
        public virtual User DigSvidCreator { get; set; }
        public virtual ActStatus Status { get; set; }
        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
    }
}
