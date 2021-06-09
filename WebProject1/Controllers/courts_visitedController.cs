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
    public class courts_visitedController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: courts_visited
        public ActionResult Index()
        {
            var courts_visited = db.courts_visited.Include(c => c.prisoner);
            return View(courts_visited.ToList());
        }

        // GET: courts_visited/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            courts_visited courts_visited = db.courts_visited.Find(id);
            if (courts_visited == null)
            {
                return HttpNotFound();
            }
            return View(courts_visited);
        }

        // GET: courts_visited/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: courts_visited/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,court_name,address,prisonerid,visit_date,description")] courts_visited courts_visited)
        {
            if (ModelState.IsValid)
            {
                String prisonerid = Session["prisonerID"].ToString();
                courts_visited.prisonerid = Int32.Parse(prisonerid);
                db.courts_visited.Add(courts_visited);
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = prisonerid });
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", courts_visited.prisonerid);
            return View(courts_visited);
        }

        // GET: courts_visited/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            courts_visited courts_visited = db.courts_visited.Find(id);
            Session["prisonerID"] = courts_visited.prisonerid;
            if (courts_visited == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", courts_visited.prisonerid);
            return View(courts_visited);
        }

        // POST: courts_visited/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,court_name,address,prisonerid,visit_date,description")] courts_visited courts_visited)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courts_visited).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners",new { id = courts_visited.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", courts_visited.prisonerid);
            return View(courts_visited);
        }

        // GET: courts_visited/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            courts_visited courts_visited = db.courts_visited.Find(id);
            if (courts_visited == null)
            {
                return HttpNotFound();
            }
            return View(courts_visited);
        }

        // POST: courts_visited/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            courts_visited courts_visited = db.courts_visited.Find(id);
            var prisoner_id = courts_visited.prisonerid;
            db.courts_visited.Remove(courts_visited);
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
