using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Framework
{
    public class SecurityPolicy
    {
        public string UserName()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var userName = claims?.FirstOrDefault(x => x.Type.Equals(ApplicationKeys.userNameClaims, StringComparison.OrdinalIgnoreCase))?.Value;
            return userName;
        }

        public string UserId()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var userId = claims?.FirstOrDefault(x => x.Type.Equals(ApplicationKeys.userIdClaims, StringComparison.OrdinalIgnoreCase))?.Value;
            return userId;
        }

        public string[] RoleId()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var roleIds = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value;
            return roleIds.Split(',');
        }

        public string Token()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var token = claims?.FirstOrDefault(x => x.Type.Equals(ApplicationKeys.token, StringComparison.OrdinalIgnoreCase))?.Value;
            return token;
        }


    }
}
