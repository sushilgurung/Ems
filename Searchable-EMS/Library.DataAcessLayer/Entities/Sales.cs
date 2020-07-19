using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer
{
    [Table("Sales", Schema = "dbo")]
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
