using Library.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Core.Controls
{
    public class UserControl : Controller
    {
        SecurityPolicy policy = new SecurityPolicy();
        /// <summary>
        /// Get Current User Name.
        /// </summary>
        public string GetUsername
        {
            get
            {
                try
                {
                    string userName = policy.UserName();
                    return userName;
                }
                catch
                {
                    return ApplicationKeys.anonymousUser;
                }
            }
        }

        /// <summary>
        /// Get Current User Id.
        /// </summary>
        public string GetUserId
        {
            get
            {
                try
                {
                    string userId = policy.UserId();
                    return userId;
                }
                catch
                {
                    return ApplicationKeys.anonymousRoleId.ToString();
                }
            }
        }

        /// <summary>
        /// Get Current User RoleId.
        /// </summary>
        public string[] GetRoleId
        {
            get
            {
                try
                {
                    return policy.RoleId();
                }
                catch
                {
                    return new string[] { ApplicationKeys.anonymousRoleId.ToString() };
                }
            }
        }

        public string GetPublicIpAddress
        {
            get
            {
                try
                {
                    string ipAddress = IpHelper.GetPublicIPAddress();
                    return ipAddress;
                }
                catch
                {
                    return "12.0.0.1";
                }
            }
        }  
      
        public string ControllerName
        {
            get
            {
                try
                {
                    return Utility.GetControllerName();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public string ActionName
        {
            get
            {
                try
                {
                    return Utility.GetActionName();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
