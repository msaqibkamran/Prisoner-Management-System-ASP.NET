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
    public class crime_recordController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: crime_record
        public ActionResult Index()
        {
            var crime_record = db.crime_record.Include(c => c.prisoner);
            return View(crime_record.ToList());
        }

        // GET: crime_record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crime_record crime_record = db.crime_record.Find(id);
            if (crime_record == null)
            {
                return HttpNotFound();
            }
            return View(crime_record);
        }

        // GET: crime_record/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: crime_record/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,crime_date,description,prisonerid,punishment,imprisonment_date,release_date")] crime_record crime_record)
        {
            if (ModelState.IsValid)
            {
                String prisonerid = Session["prisonerID"].ToString();
                crime_record.prisonerid = Int32.Parse(prisonerid);
                db.crime_record.Add(crime_record);
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = prisonerid });
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", crime_record.prisonerid);
            return View(crime_record);
        }

        // GET: crime_record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crime_record crime_record = db.crime_record.Find(id);
            Session["prisonerID"] = crime_record.prisonerid;
            if (crime_record == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", crime_record.prisonerid);
            return View(crime_record);
        }

        // POST: crime_record/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,crime_date,description,prisonerid,punishment,imprisonment_date,release_date")] crime_record crime_record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crime_record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = crime_record.prisonerid});
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", crime_record.prisonerid);
            return View(crime_record);
        }

        // GET: crime_record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crime_record crime_record = db.crime_record.Find(id);
            if (crime_record == null)
            {
                return HttpNotFound();
            }
            return View(crime_record);
        }

        // POST: crime_record/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            crime_record crime_record = db.crime_record.Find(id);
            var prisoner_id = crime_record.prisonerid;
            db.crime_record.Remove(crime_record);
            db.SaveChanges();
            return RedirectToAction("Details", "prisoners", new { id = prisoner_id });
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
