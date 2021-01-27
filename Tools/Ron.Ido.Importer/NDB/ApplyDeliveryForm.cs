using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyDeliveryForm
    {
        public ApplyDeliveryForm()
        {
            ApplyDeliveryForms = new HashSet<Apply>();
            ApplyReturnOriginalsForms = new HashSet<Apply>();
            Duplicates = new HashSet<Duplicate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<Apply> ApplyDeliveryForms { get; set; }
        public virtual ICollection<Apply> ApplyReturnOriginalsForms { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
    }
}
