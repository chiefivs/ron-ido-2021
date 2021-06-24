using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class Dossier
    {
        [Key]
        public long Id { get; set; }

        public long? ApplyId { get; set; }
        public virtual Apply Apply { get; set; }

        public long? DuplicateId { get; set; }
        public virtual Duplicate Duplicate { get; set; }

        public virtual List<DossierComment> Comments { get; set; } = new List<DossierComment>();
    }
}
