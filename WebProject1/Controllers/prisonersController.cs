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
    public class prisonersController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: prisoners
        public ActionResult Index()
        {
            return View(db.prisoner.ToList());
        }

        // GET: prisoners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prisoner prisoner = db.prisoner.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            return View(prisoner);
        }

        // GET: prisoners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: prisoners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,dob,father_name,punishment,category,imprisonment_duration,cell_type,allocated_meeting_time,available_meeting_time,address,image,stipend")] prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                if(prisoner.image == null)
                {
                    prisoner.image = "default_prisoner.jpg";
                }
                prisoner.stipend = 0;
                prisoner.available_meeting_time = prisoner.allocated_meeting_time;
                db.prisoner.Add(prisoner);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = prisoner.id});
            }

            return View(prisoner);
        }

        // GET: prisoners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prisoner prisoner = db.prisoner.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            return View(prisoner);
        }

        // POST: prisoners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,dob,father_name,punishment,category,imprisonment_duration,cell_type,allocated_meeting_time,available_meeting_time,address,image,stipend")] prisoner prisoner)
        {
            if (ModelState.IsValid)
            {

                // stipend &  available meeting time not used, it is calculated on runtime
                prisoner.stipend = 100;
                prisoner.available_meeting_time = "100";

                db.Entry(prisoner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = prisoner.id });
            }
            return View(prisoner);
        }

        // GET: prisoners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prisoner prisoner = db.prisoner.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            return View(prisoner);
        }

        // POST: prisoners/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            prisoner prisoner = db.prisoner.Find(id);
            db.prisoner.Remove(prisoner);
            db.SaveChanges();
            return RedirectToAction("Home", "jail_officer");
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
