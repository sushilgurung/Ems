using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("SuspendedIP", Schema = "dbo")]
    public class SuspendedIP
    {
        [Key]
        public int IPAddressID { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("IpAddress")]
        [Display(Name = "IpAddress")]
        public string IpAddress { get; set; }
        public string IpAddressPublic { get; set; }
        public DateTime SuspendedTime { get; set; } = DateTime.UtcNow;
        public bool IsSuspended { get; set; } = true;
    }
}
