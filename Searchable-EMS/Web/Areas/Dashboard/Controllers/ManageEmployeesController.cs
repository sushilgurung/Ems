using Library.Core.Controls;
using Library.Core.Security;
using Library.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Dashboard.Controllers
{
    public class ManageEmployeesController : AdminBaseController
    {
        private readonly IManageEmployeService _manageEmployeService;
        public ManageEmployeesController(IManageEmployeService manageEmployeService)
        {
            this._manageEmployeService = manageEmployeService;
        }
        // GET: Dashboard/ManageEmployees
        public ActionResult Index()
        {
            return View(_manageEmployeService.GetGenderList().Entity);
        }

    }
}