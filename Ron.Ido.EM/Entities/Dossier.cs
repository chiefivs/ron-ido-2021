using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class Dossier
    {
        [Key]
        public long Id { get; set; }

        public long? ApplyId { get; set; }

        public virtual Apply Apply { get; set; }
    }
}
