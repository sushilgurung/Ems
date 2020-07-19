using Library.Core.Controls;
using Library.Core.Framework;
using Library.Implementation;
using Library.Implementation.LoginService.Enums;
using Library.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AccountController : UserBaseController
    {
        private IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.IpAddress = GetPublicIpAddress;
                var result = _authService.SignIn(model, false);
                switch (result)
                {
                    case LogInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case LogInStatus.LockedOut:
                        return View("Lockout");
                    case LogInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case LogInStatus.Failure:
                        model.ErrorMessage = "UserName or password not match";
                        return View(model);
                    case LogInStatus.IpSuspended:
                        model.ErrorMessage = "";
                        return View(model);
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
                //return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard", new { Area = "Dashboard" });
            //return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}