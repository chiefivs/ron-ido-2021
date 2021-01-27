using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Expertize
    {
        public Expertize()
        {
            Conclusions = new HashSet<Conclusion>();
            ExpertizeStatusHistories = new HashSet<ExpertizeStatusHistory>();
        }

        public int Id { get; set; }
        public DateTime? TermDate { get; set; }
        public DateTime? AssignDate { get; set; }
        public string ApplyBarCode { get; set; }
        public int ManagerId { get; set; }
        public int ExpertId { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual User Expert { get; set; }
        public virtual User Manager { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<ExpertizeStatusHistory> ExpertizeStatusHistories { get; set; }
    }
}
