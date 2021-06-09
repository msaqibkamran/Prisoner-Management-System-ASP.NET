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
    public class community_taskController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: community_task
        public ActionResult Index()
        {
            var community_task = db.community_task.Include(c => c.prisoner);
            return View(community_task.ToList());
        }

        // GET: community_task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            community_task community_task = db.community_task.Find(id);
            if (community_task == null)
            {
                return HttpNotFound();
            }
            return View(community_task);
        }

        // GET: community_task/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: community_task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,task,date,amount,prisonerid")] community_task community_task)
        {
            if (ModelState.IsValid)
            {
                String prisonerid = Session["prisonerID"].ToString();
                community_task.prisonerid = Int32.Parse(prisonerid);
                var prisoner = db.prisoner.SingleOrDefault(p => p.id == community_task.prisonerid);
                prisoner.stipend += community_task.amount;


                db.community_task.Add(community_task);
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = prisonerid});
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", community_task.prisonerid);
            return View(community_task);
        }

        // GET: community_task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            community_task community_task = db.community_task.Find(id);
            Session["prisonerID"] = community_task.prisonerid;
            if (community_task == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", community_task.prisonerid);
            return View(community_task);
        }

        // POST: community_task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,task,date,amount,prisonerid")] community_task community_task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community_task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = community_task.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", community_task.prisonerid);
            return View(community_task);
        }

        // GET: community_task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            community_task community_task = db.community_task.Find(id);
            if (community_task == null)
            {
                return HttpNotFound();
            }
            return View(community_task);
        }

        // POST: community_task/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            community_task community_task = db.community_task.Find(id);
            var prisoner_id = community_task.prisonerid;
            var prisoner = db.prisoner.SingleOrDefault(f => f.id == prisoner_id);
            prisoner.stipend -= community_task.amount;
            db.community_task.Remove(community_task);
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
