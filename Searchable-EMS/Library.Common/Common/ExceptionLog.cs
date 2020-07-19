using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Common
{
    public partial class ExceptionLog
    {
        public static void ProcessException(Exception ex)
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            string action = routeData.Values["action"].ToString();
            string controller = routeData.Values["controller"].ToString();
            string parameter = routeData.Values["id"].ToString();
            string dataparameter = String.Empty;
            using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                dataparameter = inputStream.ReadToEnd();
            }
        }
    }


}
