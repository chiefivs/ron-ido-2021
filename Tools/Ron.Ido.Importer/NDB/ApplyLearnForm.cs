using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyLearnForm
    {
        public ApplyLearnForm()
        {
            Applies = new HashSet<Apply>();
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
