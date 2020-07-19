using Library.Core.Framework;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Implementation
{
    public interface IAuthService
    {
        void SignInSucessfully(UserAuthViewModel user, bool rememberMe, string ipAddress);
        LogInStatus SignIn(LoginViewModel loginModel, bool rememberMe);
    }
}
