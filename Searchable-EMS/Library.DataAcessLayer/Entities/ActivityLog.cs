using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("ActivityLog", Schema = "dbo")]
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string TableName { get; set; }
        public int TableId { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
