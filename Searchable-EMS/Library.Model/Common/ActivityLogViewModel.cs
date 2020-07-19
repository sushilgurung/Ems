using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class ActivityLogViewModel
    {
        public string TableName { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
