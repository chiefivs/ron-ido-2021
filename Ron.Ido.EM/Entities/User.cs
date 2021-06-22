using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.EM.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string SurName { get; set; }
        
        [StringLength(200)]
        public string FirstName { get; set; }
        
        [StringLength(200)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Snils { get; set; }

        public string Remark { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsBlocked { get; set; }

        public string PasswordHash { get; set; }

        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
