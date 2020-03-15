using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Register.Models;

namespace Register.Cls.User
{
    public class clsIdentity : IIdentity
    {
        
        public IIdentity Identity { get; set; }
        public UserU User { get; set; }

        public clsIdentity(UserU user)
        {
            Identity =  new GenericIdentity(user.Username);
            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}