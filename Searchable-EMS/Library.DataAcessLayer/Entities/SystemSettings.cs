using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("SystemSettings", Schema = "dbo")]
    public class SystemSettings
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string SettingType { get; set; }
        [MaxLength(200)]
        public string SettingKey { get; set; }
        [MaxLength(200)]
        public string SettingValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCacheable { get; set; }
        public Nullable<DateTime> AddedOn { get; set; }
    }
}
