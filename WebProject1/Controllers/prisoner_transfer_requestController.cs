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
    public class prisoner_transfer_requestController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: prisoner_transfer_request
        public ActionResult Index()
        {
            var prisoner_transfer_request = db.prisoner_transfer_request.Include(p => p.court_officer).Include(p => p.jailer).Include(p => p.prisoner);
            return View(prisoner_transfer_request.ToList());
        }

        // GET: prisoner_transfer_request/Details/5
        [jailerAuthentication]
        public ActionResult Details(int? id)
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

        // GET: prisoner_transfer_request/Create

        [courtOfficerAuthentication]
        public ActionResult Create()
        {
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name");
            ViewBag.jailer_id = new SelectList(db.jailer, "id", "name");
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: prisoner_transfer_request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [courtOfficerAuthentication]
        public ActionResult Create([Bind(Include = "id,from_jail,to_jail,reason_of_transfer,description,request_date,accept_date,status,prisonerid,court_officer_id,jailer_id")] prisoner_transfer_request prisoner_transfer_request)
        {
            if (ModelState.IsValid)
            {
                prisoner_transfer_request.status = "Pending";
                db.prisoner_transfer_request.Add(prisoner_transfer_request);
                db.SaveChanges();
                return RedirectToAction("Home", "court_officer");
            }

            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", prisoner_transfer_request.court_officer_id);
            ViewBag.jailer_id = new SelectList(db.jailer, "id", "name", prisoner_transfer_request.jailer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", prisoner_transfer_request.prisonerid);
            return View(prisoner_transfer_request);
        }

        // GET: prisoner_transfer_request/Edit/5

        [courtOfficerAuthentication]
        public ActionResult Edit(int? id)
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
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", prisoner_transfer_request.court_officer_id);
            ViewBag.jailer_id = new SelectList(db.jailer, "id", "name", prisoner_transfer_request.jailer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", prisoner_transfer_request.prisonerid);
            return View(prisoner_transfer_request);
        }

        // POST: prisoner_transfer_request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [courtOfficerAuthentication]
        public ActionResult Edit([Bind(Include = "id,from_jail,to_jail,reason_of_transfer,description,request_date,accept_date,status,prisonerid,court_officer_id,jailer_id")] prisoner_transfer_request prisoner_transfer_request)
        {
            if (ModelState.IsValid)
            {
                prisoner_transfer_request.status = "Pending";
                db.Entry(prisoner_transfer_request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home", "court_officer");
            }
            ViewBag.court_officer_id = new SelectList(db.court_officer, "id", "name", prisoner_transfer_request.court_officer_id);
            ViewBag.jailer_id = new SelectList(db.jailer, "id", "name", prisoner_transfer_request.jailer_id);
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", prisoner_transfer_request.prisonerid);
            return View(prisoner_transfer_request);
        }

        // GET: prisoner_transfer_request/Delete/5
        public ActionResult Delete(int? id)
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
            return RedirectToAction("Home", "court_officer");
        }

        // POST: prisoner_transfer_request/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            prisoner_transfer_request prisoner_transfer_request = db.prisoner_transfer_request.Find(id);
            db.prisoner_transfer_request.Remove(prisoner_transfer_request);
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



        public ActionResult Accept(int? id)
        {
            var request = db.prisoner_transfer_request.SingleOrDefault(f => f.id == id);
            request.status = "Accepted";
            request.accept_date = DateTime.Now.ToString("dd-MM-yyyy");
            db.SaveChanges();
            return RedirectToAction("Home", "jailers");
        }

        public ActionResult Reject(int? id)
        {
            var request = db.prisoner_transfer_request.SingleOrDefault(f => f.id == id);
            request.status = "Rejected";
            request.accept_date = DateTime.Now.ToString("dd-MM-yyyy");
            db.SaveChanges();
            return RedirectToAction("Home", "jailers");
        }
    }
}
