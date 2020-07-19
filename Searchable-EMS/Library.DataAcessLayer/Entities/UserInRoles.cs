using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAcessLayer.Entities
{
    [Table("UserInRoles", Schema = "dbo")]
    public class UserInRoles
    {
        [Key]
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
