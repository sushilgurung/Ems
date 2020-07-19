using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public bool IsModified { get; set; } = false;
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; } = DateTime.UtcNow;
        public object Select(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
