using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Security
{
    public static class IdentityExtensions
    {
        public static string[] GetRolesId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            if (claim == null)
                return new string[] { "C7F5D260-0AB7-4FF5-AE99-6DD3F4031F23" };

            return claim.Value.Split(',');
        }

        public static bool IsAdmin(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);
            string[] roles = claim.Value.Split(',');
            if (Array.IndexOf(roles, Utility.SuperAdminRole.ToLower()) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetUserName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Email);

            return claim?.Value ?? string.Empty;
        }
    }
}
