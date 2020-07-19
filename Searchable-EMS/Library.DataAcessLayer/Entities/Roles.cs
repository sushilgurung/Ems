using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("Roles", Schema = "dbo")]
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleFriendlyName { get; set; }
    }
}
