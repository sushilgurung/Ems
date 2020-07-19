using Library.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Core
{
    public class UserAPIControl : ApiController
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
        public Guid GetUserId
        {
            get
            {
                try
                {
                    Guid userId = new Guid(policy.UserId());
                    return userId;
                }
                catch
                {
                    return ApplicationKeys.anonymousRoleId;
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


        /// <summary>
        /// Get User Ip Public Address
        /// </summary>
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

    }
}
