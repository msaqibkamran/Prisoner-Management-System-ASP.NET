using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


// class for checking if user is logged in before accessing any page

namespace WebProject1.Models
{
    public class myAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var vUser = httpContext.Session["userid"];
            if (vUser != null)
            {
                return true; // yay user found
            }
            httpContext.Session["notloginmsg"] = "Please Login First";
            return false; // user not found, this will cause the redirection to kick in
        }
    }
}