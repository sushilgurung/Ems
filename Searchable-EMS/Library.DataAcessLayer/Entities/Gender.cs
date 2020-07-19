using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("Gender", Schema = "dbo")]
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("0")]
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("0")]
        public bool IsUpdated { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

    }
}
