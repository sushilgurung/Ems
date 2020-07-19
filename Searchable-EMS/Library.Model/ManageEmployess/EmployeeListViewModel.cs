using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class EmployeeListViewModel
    {
        public int RowNum { get; set; }
        public int RowTotal { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string DateOfBirthView
        {
            get
            {
                return this.DateOfBirth.ToString("MM/dd/yyyy");
            }
            set
            {
            }
        }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
    }
}
