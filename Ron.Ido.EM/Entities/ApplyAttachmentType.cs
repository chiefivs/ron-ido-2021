using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    public class ApplyAttachmentType // NDB:ApplyDocumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string NameEng { get; set; }

        public int OrderNum { get; set; }

        public bool Required { get; set; }

        public bool ForArchive { get; set; }

        public bool ForPortal { get; set; }
    }
}
