using Library.DataAcessLayer.Entities;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public interface IManageEmployeRepository : IRepository<Employee>
    {
        List<EmployeeListViewModel> GetEmployeeList(EmployeeViewModelFilter filter);
    }
}
