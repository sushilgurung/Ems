using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Entities
{
    [Table("Employee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(400)]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedOn { get; set; }
        public bool IsUpdated { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public Nullable<Guid> UpdatedDateBy { get; set; }
        public string ImageName { get; set; }
        public bool IsImported { get; set; }
        public Nullable<DateTime> ImportedDate { get; set; }
    }
}
