using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class ApplyMessage   //  NDB:ApplyError
    {
        public long ApplyId { get; set; }

        public string FieldName { get; set; }

        [StringLength(2000)]
        public string Text { get; set; }
    }
}
