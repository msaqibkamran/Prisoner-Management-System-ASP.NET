using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject1.DAL;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    [myAuthorize]
    [jailerAuthentication]
    public class jailersController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();
        dynamic obj = new ExpandoObject();
        

        // GET: jailers
        public ActionResult Index()
        {
            return View(db.jailer.ToList());
        }

        // GET: jailers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jailer jailer = db.jailer.Find(id);
            if (jailer == null)
            {
                return HttpNotFound();
            }
            return View(jailer);
        }

        // GET: jailers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: jailers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,contact,address,dob,image,appointment_date,retirement_date,in_service,email,password")] jailer jailer)
        {
            if (ModelState.IsValid)
            {
                db.jailer.Add(jailer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jailer);
        }

        // GET: jailers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jailer jailer = db.jailer.Find(id);
            if (jailer == null)
            {
                return HttpNotFound();
            }
            return View(jailer);
        }

        // POST: jailers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,contact,address,dob,image,appointment_date,retirement_date,in_service,email,password")] jailer jailer)
        {
            
            if (ModelState.IsValid)
            {
                var officer = db.jailer.SingleOrDefault(f => f.id == jailer.id);
                officer.name = jailer.name;
                officer.cnic = jailer.cnic;
                officer.contact = jailer.contact;
                officer.address = jailer.address;
                officer.dob = jailer.dob;
                officer.image = jailer.image;
                officer.email = jailer.email;

                db.Entry(officer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = officer.id });
            }
            return View(jailer);
        }

        // GET: jailers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jailer jailer = db.jailer.Find(id);
            if (jailer == null)
            {
                return HttpNotFound();
            }
            return View(jailer);
        }

        // POST: jailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jailer jailer = db.jailer.Find(id);
            db.jailer.Remove(jailer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: jailers/Home
        public ActionResult Home()
        {
            List<prisoner_transfer_request> prisoner_transfer_requests = (from requests in db.prisoner_transfer_request
                                                                          where requests.status == "Pending"
                                                                          select requests).ToList();
            
            return View(prisoner_transfer_requests);
        }

        
        public ActionResult AllRequests()
        {
            List<prisoner_transfer_request> prisoner_transfer_requests = (from requests in db.prisoner_transfer_request
                                                                          select requests).ToList();
            
            return View(prisoner_transfer_requests);
        }

        public ActionResult SearchRequest(string searchText, string options)
        {

            if (options == "byFromJail" && searchText != "")
            {
                var requests = db.prisoner_transfer_request.Where(x => x.from_jail.Contains(searchText) || searchText == null).ToList();
                return View(requests);
            }
            else if (options == "byToJail" && searchText != "")
            {
                var requests = db.prisoner_transfer_request.Where(x => x.to_jail.Contains(searchText) || searchText == null).ToList();
                return View(requests);
            }
            else if (options == "byStatus" && searchText != "")
            {
                var requests = db.prisoner_transfer_request.Where(x => x.status.Contains(searchText) || searchText == null).ToList();
                return View(requests);
            }
            else
            {
                return RedirectToAction("NotFound");
            }

        }

        public ActionResult NotFound()
        {
            return View();
        }


        public ActionResult ChangePassword(int id)
        {

            jailer jailer = db.jailer.Find(id);
            if (jailer == null)
            {
                return HttpNotFound();
            }
            return View(jailer);
        }

        public ActionResult ConfirmChangePassword(int id, string oldpassword, string newpassword, string newpassword1)
        {

            jailer jailer = db.jailer.Find(id);

            if (jailer == null)
            {
                return HttpNotFound();
            }

            if (jailer.password != oldpassword)
            {
                Session["passwordMessage"] = "Incorrect Old Password";
                return RedirectToAction("ChangePassword", new { id = jailer.id });
            }

            else if (newpassword != newpassword1)
            {
                Session["passwordMessage"] = "New Password did not match";
                return RedirectToAction("ChangePassword", new { id = jailer.id });
            }
            else
            {
                jailer.password = newpassword;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = jailer.id });
            }

        }
    }
}
