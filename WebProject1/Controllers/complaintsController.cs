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
    public class complaintsController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: complaints
        public ActionResult Index()
        {
            var complaint = db.complaint.Include(c => c.court_officer).Include(c => c.jail_officer).Include(c => c.prisoner);
            return View(complaint.ToList());
        }

        // GET: complaints/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            complaint complaint = db.complaint.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: complaints/Create

        [courtOfficerAuthentication]
        public ActionResult Create()
        {
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name");
            ViewBag.jail_officer_id = new SelectList(db.jail_officer, "id", "name");
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [courtOfficerAuthentication]
        public ActionResult Create([Bind(Include = "id,title,description,prisonerid,reg_date,court_officer_id,jail_officer_id")] complaint complaint)
        {
            
            if (ModelState.IsValid)
            {
                complaint.status = "Pending";
                db.complaint.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", complaint.court_officer_id);
            ViewBag.jail_officer_id = new SelectList(db.jail_officer, "id", "name", complaint.jail_officer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", complaint.prisonerid);
            return View(complaint);
        }

        // GET: complaints/Edit/5

        [courtOfficerAuthentication]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            complaint complaint = db.complaint.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", complaint.court_officer_id);
            ViewBag.jail_officer_id = new SelectList(db.jail_officer, "id", "name", complaint.jail_officer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", complaint.prisonerid);
            return View(complaint);
        }

        // POST: complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [courtOfficerAuthentication]
        public ActionResult Edit([Bind(Include = "id,title,description,prisonerid,reg_date,resolved_date,status,court_officer_id,jail_officer_id")] complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", complaint.court_officer_id);
            ViewBag.jail_officer_id = new SelectList(db.jail_officer, "id", "name", complaint.jail_officer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", complaint.prisonerid);
            return View(complaint);
        }

        // GET: complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            complaint complaint = db.complaint.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Home", "court_officer");
        }

       
        public ActionResult DeleteConfirmed(int id)
        {
            complaint complaint = db.complaint.Find(id);
            db.complaint.Remove(complaint);
            db.SaveChanges();
            return RedirectToAction("Home", "court_officer");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
