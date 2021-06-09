using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject1.DAL;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    [myAuthorize]
    [jailOfficerAuthentication]
    public class jail_officerController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: jail_officer
        public ActionResult Index()
        {
            return View(db.jail_officer.ToList());
        }

        // GET: jail_officer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jail_officer jail_officer = db.jail_officer.Find(id);
            if (jail_officer == null)
            {
                return HttpNotFound();
            }
            return View(jail_officer);
        }

        // GET: jail_officer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: jail_officer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,contact,address,dob,image,appointment_date,retirement_date,in_service,email,password")] jail_officer jail_officer)
        {
            if (ModelState.IsValid)
            {
                db.jail_officer.Add(jail_officer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jail_officer);
        }

        // GET: jail_officer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jail_officer jail_officer = db.jail_officer.Find(id);
            if (jail_officer == null)
            {
                return HttpNotFound();
            }
            return View(jail_officer);
        }

        // POST: jail_officer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,contact,address,dob,image,appointment_date,retirement_date,in_service,email,password")] jail_officer jail_officer)
        {
            if (ModelState.IsValid)
            {
                var officer = db.jail_officer.SingleOrDefault(f => f.id == jail_officer.id);
                officer.name = jail_officer.name;
                officer.cnic = jail_officer.cnic;
                officer.contact = jail_officer.contact;
                officer.address = jail_officer.address;
                officer.dob = jail_officer.dob;
                officer.image = jail_officer.image;
                officer.email = jail_officer.email;

                db.Entry(officer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = officer.id });
            }
            return View(jail_officer);
        }

        // GET: jail_officer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jail_officer jail_officer = db.jail_officer.Find(id);
            if (jail_officer == null)
            {
                return HttpNotFound();
            }
            return View(jail_officer);
        }

        // POST: jail_officer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jail_officer jail_officer = db.jail_officer.Find(id);
            db.jail_officer.Remove(jail_officer);
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

        public ActionResult Home()
        {

            List<prisoner> prisoners = (from p in db.prisoner
                                        select p).ToList();
            return View(prisoners);
        }

        public ActionResult PendingComplaints()
        {

            List<complaint> complaints = (from c in db.complaint
                                          where c.status == "Pending"
                                          select c).ToList();
            return View(complaints);
        }

        public ActionResult ResolvedComplaints()
        {

            List<complaint> complaints = (from c in db.complaint
                                          where c.status == "Resolved"
                                          select c).ToList();
            return View(complaints);
        }

        public ActionResult ComplaintDetails(int id)
        {

            complaint complaint = db.complaint.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
            
        }

        public ActionResult Resolved(int? id)
        {
            var request = db.complaint.SingleOrDefault(f => f.id == id);
            request.status = "Resolved";
            request.resolved_date = DateTime.Now.ToString("dd-MM-yyyy");
            db.SaveChanges();
            return RedirectToAction("PendingComplaints");
        }


        public ActionResult PayStipend()
        {

            return View();
        }

        public ActionResult SearchRequest(string searchText, string options)
        {

            if (options == "byComplaintTitle" && searchText != "")
            {
                var requests = db.complaint.Where(x => x.title.Contains(searchText) || searchText == null).ToList();
                return View(requests);
            }
            else if (options == "prisonerName" && searchText != "")
            {
                var requests = db.prisoner.Where(x => x.name.Contains(searchText) || searchText == null).ToList();
                return RedirectToAction("prisonerSearch", new { searchText = searchText });
            }
            

            else
            {
                return RedirectToAction("NotFound");
            }

        }

        public ActionResult prisonerSearch(String searchText)
        {
            var requests = db.prisoner.Where(x => x.name.Contains(searchText) || searchText == null).ToList();
            return View(requests);
        }

        public ActionResult NotFound()
        {
           
            return View();
        }

        public ActionResult NoAvailableTime()
        {
            return View();
        }

        public ActionResult ChangePassword(int id)
        {

            jail_officer jail_officer = db.jail_officer.Find(id);
            if (jail_officer == null)
            {
                return HttpNotFound();
            }
            return View(jail_officer);
        }

        public ActionResult ConfirmChangePassword(int id, string oldpassword, string newpassword, string newpassword1)
        {

            jail_officer jail_officer = db.jail_officer.Find(id);

            if (jail_officer == null)
            {
                return HttpNotFound();
            }

            if (jail_officer.password != oldpassword)
            {
                Session["passwordMessage"] = "Incorrect Old Password";
                return RedirectToAction("ChangePassword", new { id = jail_officer.id });
            }

            else if (newpassword != newpassword1)
            {
                Session["passwordMessage"] = "New Password did not match";
                return RedirectToAction("ChangePassword", new { id = jail_officer.id });
            }
            else
            {
                jail_officer.password = newpassword;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = jail_officer.id });
            }

        }

    }
}
