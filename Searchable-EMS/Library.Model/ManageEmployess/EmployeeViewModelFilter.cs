using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class EmployeeViewModelFilter
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public string SearchText { get; set; }
        public Nullable<decimal> SalaryRangeFrom { get; set; }
        public Nullable<decimal> SalaryRangeTo { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<DateTime> ImportedRangeFrom { get; set; }
        public Nullable<DateTime> ImportedRangeTo { get; set; }
    }
}
