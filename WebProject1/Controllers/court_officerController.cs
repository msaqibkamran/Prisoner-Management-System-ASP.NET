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
    [courtOfficerAuthentication]
    public class court_officerController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: court_officer
        
        public ActionResult Index()
        {
            return View(db.court_officer.ToList());
        }

        // GET: court_officer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            court_officer court_officer = db.court_officer.Find(id);
            if (court_officer == null)
            {
                return HttpNotFound();
            }
            return View(court_officer);
        }

        // GET: court_officer/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: court_officer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,contact,address,dob,image,appointment_date,retirement_date,in_service,email,password")] court_officer court_officer)
        {
            if (ModelState.IsValid)
            {
                db.court_officer.Add(court_officer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(court_officer);
        }

        // GET: court_officer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            court_officer court_officer = db.court_officer.Find(id);
            if (court_officer == null)
            {
                return HttpNotFound();
            }
            return View(court_officer);
        }

        // POST: court_officer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,contact,address,dob,image,email")] court_officer court_officer)
        {
            if (ModelState.IsValid)
            {
                var officer = db.court_officer.SingleOrDefault(f => f.id == court_officer.id);
                officer.name = court_officer.name;
                officer.cnic = court_officer.cnic;
                officer.contact = court_officer.contact;
                officer.address = court_officer.address;
                officer.dob = court_officer.dob;
                officer.image = court_officer.image;
                officer.email = court_officer.email;

                db.Entry(officer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = officer.id });
            }
            return View(court_officer);
        }

        // GET: court_officer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            court_officer court_officer = db.court_officer.Find(id);
            if (court_officer == null)
            {
                return HttpNotFound();
            }
            return View(court_officer);
        }

        // POST: court_officer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            court_officer court_officer = db.court_officer.Find(id);
            db.court_officer.Remove(court_officer);
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


        // GET: court_officer/Home

        public ActionResult Home()
        {
            List<prisoner_transfer_request> prisoner_transfer_requests = (from requests in db.prisoner_transfer_request
                                                                          where requests.status == "Pending" || requests.status == "Rejected"
                                                                          select requests).ToList();

            return View(prisoner_transfer_requests);
        }

        public ActionResult AcceptedTransferRequests()
        {
            List<prisoner_transfer_request> prisoner_transfer_requests = (from requests in db.prisoner_transfer_request
                                                                          where requests.status == "Accepted"
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
            else if (options == "byRequestStatus" && searchText != "")
            {
                var requests = db.prisoner_transfer_request.Where(x => x.status.Contains(searchText) || searchText == null).ToList();
                return View(requests);
            }
            else if (options == "byComplaintStatus" && searchText != "")
            {
                
                return RedirectToAction("SearchComplaints", new { searchText = searchText});
            }

            else
            {
                return RedirectToAction("NotFound");
            }

        }

        public ActionResult SearchComplaints(String searchText)
        {
            var requests = db.complaint.Where(x => x.status.Contains(searchText) || searchText == null).ToList();
            return View(requests);
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult RequestDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prisoner_transfer_request prisoner_transfer_request = db.prisoner_transfer_request.Find(id);
            if (prisoner_transfer_request == null)
            {
                return HttpNotFound();
            }
            return View(prisoner_transfer_request);
        }

        public ActionResult AddComplaint()
        {
            
            return View();
        }

        public ActionResult ChangePassword(int id)
        {

            court_officer court_officer = db.court_officer.Find(id);
            if (court_officer == null)
            {
                return HttpNotFound();
            }
            return View(court_officer);
        }

        public ActionResult ConfirmChangePassword(int id, string oldpassword, string newpassword, string newpassword1)
        {

            court_officer court_officer = db.court_officer.Find(id);

            if (court_officer == null)
            {
                return HttpNotFound();
            }

            if(court_officer.password != oldpassword)
            {
                Session["passwordMessage"] = "Incorrect Old Password";
                return RedirectToAction("ChangePassword", new {id = court_officer.id });
            }
            
            else if (newpassword != newpassword1)
            {
                Session["passwordMessage"] = "New Password did not match";
                return RedirectToAction("ChangePassword", new { id = court_officer.id });
            }
            else
            {
                court_officer.password = newpassword;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = court_officer.id});
            }
            
        }

    }
}
