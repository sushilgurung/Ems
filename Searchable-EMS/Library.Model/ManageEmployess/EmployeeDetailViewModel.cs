﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class EmployeeDetailViewModel
    {
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
        public int Gender { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public string ImageName { get; set; }
    }
}
