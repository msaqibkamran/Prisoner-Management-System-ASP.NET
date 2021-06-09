using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject1.DAL;

namespace WebProject1.Models
{
    public class courtOfficerAuthentication : AuthorizeAttribute
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string userID = httpContext.Session["userid"].ToString();
            int uid = Int32.Parse(userID);
            string email = httpContext.Session["email"].ToString();
            string pass = httpContext.Session["password"].ToString();
            string cnic = httpContext.Session["cnic"].ToString();

            var user = (from u in db.court_officer
                        where (u.email == email && u.password == pass && u.id == uid && u.cnic == cnic )
                        select u).ToList().SingleOrDefault();

            
            if (user != null)
            {
                return true; // yay user found
            }
            httpContext.Session["noaccess"] = "You dont have access to this page, try logging in from Court Officer Credentials";
            return false; // user not found, this will cause the redirection to kick in
        }
    }
}