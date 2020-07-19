using Library.Core.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Dashboard.Controllers
{
    public class DashBoardController : AdminBaseController
    {
        // GET: Dashboard/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}