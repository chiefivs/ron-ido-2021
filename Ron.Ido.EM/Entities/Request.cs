using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    public class Request
    {
        [Key]
        public long Id { get; set; }

    }
}
