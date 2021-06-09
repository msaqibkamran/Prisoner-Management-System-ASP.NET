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
    public class assetsController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: assets
        public ActionResult Index()
        {
            var asset = db.asset.Include(a => a.prisoner);
            return View(asset.ToList());
        }

        // GET: assets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: assets/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,worth,prisonerid")] asset asset)
        {
            if (ModelState.IsValid)
            {
                String prisonerid = Session["prisonerID"].ToString();
                asset.prisonerid = Int32.Parse(prisonerid);
                db.asset.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = asset.id });
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", asset.prisonerid);
            return View(asset);
        }

        // GET: assets/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            Session["prisonerID"] = asset.prisonerid;
            if (asset == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", asset.prisonerid);
            return View(asset);
        }

        // POST: assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,worth,prisonerid")] asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = asset.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", asset.prisonerid);
            return View(asset);
        }

        // GET: assets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.asset.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: assets/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            asset asset = db.asset.Find(id);
            var prisoner_id = asset.prisonerid;
            db.asset.Remove(asset);
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
