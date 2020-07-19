using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Library.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Library.Core.Security.Helpers;
using System.Collections.Generic;
using Library.DataAcessLayer.Repository;
using Library.DataAcessLayer.Entities;
using Library.DataAcessLayer;
using Library.Core.Security;
using Library.Core.Framework;
using Library.DataAcessLayer.Context;

namespace Library.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<LoginActivities> _LoginActivities;
        //public AuthService(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}
        public AuthService(IUserRepository userRepository, IRepository<LoginActivities> loginActivities)
        {
            _userRepository = userRepository;
            _LoginActivities = loginActivities;
            //_userRepository = new UserRepository(new DatabaseEntities());
            //_LoginActivities = new Repository<LoginActivities>(new DatabaseEntities());
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        //public AuthService(IUnitOfWork unitOfWork)
        //{
        //    this._unitOfWork = unitOfWork;
        //}

        public LogInStatus SignIn(LoginViewModel loginModel, bool rememberMe)
        {
            //User userDetail = _userRepository.GetUserWithUserName(loginModel.UserName);
            UserAuthViewModel userDetail = _userRepository.GetUserDetailsByUserName(loginModel.UserName);
            LogInStatus result = LogInStatus.Failure;
            if (userDetail != null)
            {
                if (PasswordHelpers.ValidateUser(userDetail.PasswordFormat, loginModel.Password, userDetail.Password, userDetail.PasswordSalt))
                {
                    string token = TokenManager.GenerateToken(userDetail, 30);
                    userDetail.Token = token;
                    SignInSucessfully(userDetail, rememberMe, loginModel.IpAddress);
                    result = LogInStatus.Success;
                }
                else
                {
                    result = LogInStatus.Failure;
                }
            }
            return result;
        }


        public void SignInSucessfully(UserAuthViewModel user, bool rememberMe, string ipAddress)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            List<Claim> claims = GetClaims(user);
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            LoginActivities login = new LoginActivities()
            {
                UserId = user.UserId,
                LoginTime = DateTime.UtcNow,
                IpAddress = ipAddress
            };
            _LoginActivities.Add(login);
            _LoginActivities.SaveChanges();
        }


        private List<Claim> GetClaims(UserAuthViewModel user)
        {
            string[] roles = new string[] { "Admin", "User" };
            List<Claim> claims = new List<Claim>();
            // create *required* claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "harry"));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(",", user.RoleId)));
            claims.Add(new Claim(ClaimTypes.Gender, ""));
            claims.Add(new Claim(ClaimTypes.Name, ""));
            claims.Add(new Claim(ClaimTypes.Surname, ""));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ApplicationKeys.userIdClaims, user.UserId.ToString()));
            claims.Add(new Claim(ApplicationKeys.userNameClaims, user.UserName));
            claims.Add(new Claim(ApplicationKeys.token, user.Token));
            return claims;
        }




    }
}
