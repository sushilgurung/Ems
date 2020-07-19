using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class ManageEmployeeListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public int Gender { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string Designation { get; set; }
    }
}
