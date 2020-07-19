using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("LoginActivities", Schema = "dbo")]
    public class LoginActivities
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        [MaxLength(100)]
        public string IpAddress { get; set; }
    }
}
