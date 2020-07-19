using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Framework
{
    public static partial class ApplicationKeys
    {
        public static string anonymousUser = "anonymoususer";
        public static Guid anonymousRoleId = new Guid("c7f5d260-0ab7-4ff5-ae99-6dd3f4031f23");


        public static string userNameClaims = "UserName";
        public static string userIdClaims = "UserId";
        public static string realName = "RealName";
        public static string token = "JWTToken";
    }
    public enum LogInStatus
    {
        //
        // Summary:
        //     Sign in was successful
        Success = 0,
        //
        // Summary:
        //     User is locked out
        LockedOut = 1,
        //
        // Summary:
        //     Sign in requires addition verification (i.e. two factor)
        RequiresVerification = 2,
        //
        // Summary:
        //     Sign in failed
        Failure = 3,
        IpSuspended = 4
    }
}
