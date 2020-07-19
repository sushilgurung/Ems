using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Implementation
{
    public interface IManageEmployeService
    {
        ResponseModel<List<ManageEmployeeListViewModel>> UploadFile(HttpPostedFile hpf);
        ResponseModel<string> UploadImage(HttpPostedFile hpf);
        ResponseModel<List<EmployeeListViewModel>> GetEmployeeList(EmployeeViewModelFilter filter);
        ResponseModel<List<GenderListViewModel>> GetGenderList();
        ResponseModel<bool> AddUpdateEmployee(ManageEmployeeCreateViewModel model);
        ResponseModel<EmployeeDetailViewModel> GetEmployeeDetails(int id);
   
        string ExportTOCSV(string exportid);
        string ExportTOPDF(string exportid);
    }
}
