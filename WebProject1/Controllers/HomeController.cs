using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject1.DAL;
using WebProject1.DAL.ViewModels;
using WebProject1.Models;
using System.Dynamic;

namespace WebProject1.Controllers
{
    public class HomeController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "SignUp page.";

            return View();
        }

        public ActionResult MainPage(string username, string password, string types)
        {
            Session["error"] = null;
            Session["userid"] = null;
            if (types == "jailofficer")
            {
                var user = (from u in db.jail_officer
                            where u.email == username && u.password == password
                            select u).ToList().SingleOrDefault();
                if(user != null)
                {
                    Session["userid"] = user.id;
                    Session["email"] = user.email;
                    Session["password"] = user.password;
                    Session["cnic"] = user.cnic;

                    return RedirectToAction("Home", "jail_officer");
                }
                else
                {
                    Session["error"] = "Incorrect Username or Password";
                    return RedirectToAction("Login");
                }
                
            }

            else if (types == "courtofficer")
            {
                var user = (from u in db.court_officer
                            where u.email == username && u.password == password
                            select u).ToList().SingleOrDefault();
                if (user != null)
                {
                    Session["userid"] = user.id;
                    Session["email"] = user.email;
                    Session["password"] = user.password;
                    Session["cnic"] = user.cnic;

                    return RedirectToAction("Home", "court_officer");
                }
                else
                {
                    Session["error"] = "Incorrect Username or Password";
                    return RedirectToAction("Login");
                }
                
            }

            else if (types == "jailer")
            {
                var user = (from u in db.jailer
                            where u.email == username && u.password == password
                            select u).ToList().SingleOrDefault();
                if (user != null)
                {
                    Session["userid"] = user.id;
                    Session["email"] = user.email;
                    Session["password"] = user.password;
                    Session["cnic"] = user.cnic;

                    return RedirectToAction("Home", "jailers");
                }
                else
                {
                    Session["error"] = "Incorrect Username or Password";
                    return RedirectToAction("Login");
                }
                
            }

            
            return View();
        }

       

        public ActionResult SignupNew()
        {
            ViewBag.Message = "SignupNew";
            return View();
        }
        
    }
}