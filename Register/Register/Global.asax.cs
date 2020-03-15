using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using Register.Cls.User;
using Register.Models;

namespace Register
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie authoCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authoCookies != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                JavaScriptSerializer js = new JavaScriptSerializer();
                UserU user = js.Deserialize<UserU>(ticket.UserData);
                clsIdentity clsIdentity = new clsIdentity(user);
                clsPrincipal clsPrincipal = new clsPrincipal(clsIdentity);
                HttpContext.Current.User = clsPrincipal;
            }
        }
    }
}