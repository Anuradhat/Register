using Register.Cls;
using Register.Cls.User;
using Register.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Register.Controllers
{
    public class HomeController : Controller
    {
        private clsU_User clsU_User = new clsU_User();

        public ActionResult Landing()
        {
            return View();
        }

        [Authorize]
        public ActionResult Index()
        {  
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(emsLogIn l, string returnUrl = "")
        {
            #region Part 4 Code

            if (ModelState.IsValid)
            {
                bool isValidUser = Membership.ValidateUser(l.Username, l.Password);
                if (isValidUser)
                {
                    UserU _emsUserU = null;
                    _emsUserU = clsU_User.GetUserByUsername(l.Username);

                    if (User != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string data = js.Serialize(_emsUserU);

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, _emsUserU.Username, DateTime.Now, DateTime.Now.AddMinutes(30), l.RememberMe, data);
                        string encToken = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authCookies);
                        //return Redirect(returnUrl);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { CurrentUser = l.Username });
                        }
                    }
                }
            }
            #endregion

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }        
    }
}