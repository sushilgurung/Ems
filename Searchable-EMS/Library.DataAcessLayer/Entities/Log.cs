using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("Log", Schema = "dbo")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string IpAddressPrivate { get; set; }
        public string IpAddressPublic { get; set; }
        public string PageName { get; set; }
        public string Createdby { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int IsActive { get; set; }
    }
}
