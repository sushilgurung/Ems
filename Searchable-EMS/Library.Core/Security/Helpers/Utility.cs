using Library.Core.Controls;
using Library.Core.Framework;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Core
{
    public static class Utility
    {
        public const string SuperAdminRole = "87B0BB0B-6083-40F3-A3D5-E8EFCB1A2E68";
        public const string Anonymous = "C7F5D260-0AB7-4FF5-AE99-6DD3F4031F23";
        public static string GetControllerName()
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            string controller = routeData.Values["controller"].ToString();
            return controller;
        }

        public static string GetActionName()
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            string action = routeData.Values["action"].ToString();
            return action;
        }

        public static bool IsAdmin()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var roles = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value;
            string[] rolesArray = roles.Split(',');
            if (Array.IndexOf(rolesArray, Utility.SuperAdminRole.ToLower()) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string RealName()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var realName = claims?.FirstOrDefault(x => x.Type.Equals(ApplicationKeys.realName, StringComparison.OrdinalIgnoreCase))?.Value;
            return realName;
        }

        public static string UserName()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var userName = claims?.FirstOrDefault(x => x.Type.Equals(ApplicationKeys.userNameClaims, StringComparison.OrdinalIgnoreCase))?.Value;
            return userName;
        }

        public static string RemoveHtml(string input)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            input = regex.Replace(input, String.Empty);
            return input;
        }

        public static string Token()
        {
            SecurityPolicy obj = new SecurityPolicy();
            return obj.Token();
        }


    }

    //public static class Helpers
    //{
    //    public static bool Admin()
    //    {
    //        var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
    //        var u = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value;
    //        return true;
    //    }
    //}
}
