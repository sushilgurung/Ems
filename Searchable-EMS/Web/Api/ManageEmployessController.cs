using Library.Core.Controls;
using Library.Core.Security;
using Library.Implementation;
using Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http;

namespace Web.Api
{
    [ApiAuthenticationFilter]
    public class ManageEmployessController : AdminAPIBaseController
    {
        private readonly IManageEmployeService _manageEmployeService;
        public ManageEmployessController(IManageEmployeService manageEmployeService)
        {
            _manageEmployeService = manageEmployeService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<List<ManageEmployeeListViewModel>> UploadFile()
        {
            ResponseModel<List<ManageEmployeeListViewModel>> response = new ResponseModel<List<ManageEmployeeListViewModel>>();
            var allowedExtensions = new[] { ".xlsx", ".csv" };
            HttpFileCollection hfc = HttpContext.Current.Request.Files;
            HttpPostedFile hpf = hfc[0];
            if (hpf.ContentLength > 0)
            {
                response = _manageEmployeService.UploadFile(hpf);
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        // GET: api/ManageEmployess
        public ResponseModel<List<EmployeeListViewModel>> GetEmployeeList([FromUri] EmployeeViewModelFilter filter)
        {
            ResponseModel<List<EmployeeListViewModel>> response = new ResponseModel<List<EmployeeListViewModel>>();
            response = _manageEmployeService.GetEmployeeList(filter);
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<bool> AddUpdate([FromBody] ManageEmployeeCreateViewModel model)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            response = _manageEmployeService.AddUpdateEmployee(model);
            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<string> UploadImage()
        {
            ResponseModel<string> response = new ResponseModel<string>();
            HttpFileCollection hfc = HttpContext.Current.Request.Files;
            HttpPostedFile hpf = hfc[0];
            if (hpf.ContentLength > 0)
            {
                response = _manageEmployeService.UploadImage(hpf);
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<EmployeeDetailViewModel> GetEmployeeDetails([FromUri] int id)
        {
            ResponseModel<EmployeeDetailViewModel> response = new ResponseModel<EmployeeDetailViewModel>();
            response = _manageEmployeService.GetEmployeeDetails(id);
            return response;
        }

     

    }
}
