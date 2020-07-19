using Library.DataAcessLayer.Context;
using Library.DataAcessLayer.Entities;
using Library.DataAcessLayer.SQLHandler;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAcessLayer.Repository
{
    public class ManageEmployeRepository : Repository<Employee>, IManageEmployeRepository
    {
        public DatabaseEntities DbContext
        {
            get
            {
                return DatabaseEntities as DatabaseEntities;
            }
        }

        public List<EmployeeListViewModel> GetEmployeeList(EmployeeViewModelFilter filter)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Offset", filter.Offset));
                Param.Add(new KeyValuePair<string, object>("@Limit", filter.Limit));
                Param.Add(new KeyValuePair<string, object>("@DateFrom", filter.DateFrom));
                Param.Add(new KeyValuePair<string, object>("@DateTo", filter.DateTo));
                Param.Add(new KeyValuePair<string, object>("@SearchText", filter.SearchText));
                Param.Add(new KeyValuePair<string, object>("@SalaryRangeFrom", filter.SalaryRangeFrom));
                Param.Add(new KeyValuePair<string, object>("@SalaryRangeTo", filter.SalaryRangeTo));
                Param.Add(new KeyValuePair<string, object>("@Gender", filter.Gender));
                Param.Add(new KeyValuePair<string, object>("@ImportDateFrom", filter.ImportedRangeFrom));
                Param.Add(new KeyValuePair<string, object>("@ImportDateTo", filter.ImportedRangeTo));
                SQLHandlers sqlh = new SQLHandlers();
                return sqlh.ExecuteAsList<EmployeeListViewModel>("[dbo].[usp_GetEmployeList]", Param);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
