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
    public class imprisonment_recordController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: imprisonment_record
        public ActionResult Index()
        {
            var imprisonment_record = db.imprisonment_record.Include(i => i.prisoner);
            return View(imprisonment_record.ToList());
        }

        // GET: imprisonment_record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imprisonment_record imprisonment_record = db.imprisonment_record.Find(id);
            if (imprisonment_record == null)
            {
                return HttpNotFound();
            }
            return View(imprisonment_record);
        }

        // GET: imprisonment_record/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: imprisonment_record/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,jail_name,imprisonment_duration,prisonerid,reason_of_transfer,entry_date,exit_date")] imprisonment_record imprisonment_record)
        {
            if (ModelState.IsValid)
            {
                String prisonerid = Session["prisonerID"].ToString();
                imprisonment_record.prisonerid = Int32.Parse(prisonerid);
                db.imprisonment_record.Add(imprisonment_record);
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = prisonerid });
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", imprisonment_record.prisonerid);
            return View(imprisonment_record);
        }

        // GET: imprisonment_record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imprisonment_record imprisonment_record = db.imprisonment_record.Find(id);
            Session["prisonerID"] = imprisonment_record.prisonerid;
            if (imprisonment_record == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", imprisonment_record.prisonerid);
            return View(imprisonment_record);
        }

        // POST: imprisonment_record/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,jail_name,imprisonment_duration,prisonerid,reason_of_transfer,entry_date,exit_date")] imprisonment_record imprisonment_record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imprisonment_record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = imprisonment_record.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", imprisonment_record.prisonerid);
            return View(imprisonment_record);
        }

        // GET: imprisonment_record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imprisonment_record imprisonment_record = db.imprisonment_record.Find(id);
            if (imprisonment_record == null)
            {
                return HttpNotFound();
            }
            return View(imprisonment_record);
        }

        // POST: imprisonment_record/Delete/5
       
        public ActionResult DeleteConfirmed(int id)
        {
            imprisonment_record imprisonment_record = db.imprisonment_record.Find(id);
            var prisoner_id = imprisonment_record.prisonerid;
            db.imprisonment_record.Remove(imprisonment_record);
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
