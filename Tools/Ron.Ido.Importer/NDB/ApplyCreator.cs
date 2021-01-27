using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyCreator
    {
        public ApplyCreator()
        {
            Applies = new HashSet<Apply>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeleteTerm { get; set; }
        public int Quota { get; set; }
        public string Organisation { get; set; }
        public string Comment { get; set; }
        public bool AllowIdokb { get; set; }
        public bool IsImportant { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
    }
}
