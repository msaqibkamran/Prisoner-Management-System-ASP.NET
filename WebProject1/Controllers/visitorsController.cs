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
    public class visitorsController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: visitors
        public ActionResult Index()
        {
            var visitor = db.visitor.Include(v => v.prisoner);
            return View(visitor.ToList());
        }

        // GET: visitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            visitor visitor = db.visitor.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // GET: visitors/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            prisoner prisoner = db.prisoner.Find(id);
            int allocated_time = Int32.Parse(prisoner.allocated_meeting_time);
            
            var time = (from v in db.visitor
                            where v.prisonerid == id
                            select v.duration).ToList();
            int totalTime = 0;
            foreach(var t in time)
            {
                int to = Int32.Parse(t);
                totalTime += to;
            }
            if(totalTime >= allocated_time)
            {
                return RedirectToAction("NoAvailableTime", "jail_officer");
            }
            

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,contact,address,dob,date_of_visit,duration,image,prisonerid")] visitor visitor)
        {
            if (ModelState.IsValid)
            {
                if (visitor.image == null)
                {
                    visitor.image = "userava.png";
                }

                String prisonerid = Session["prisonerID"].ToString();
                visitor.prisonerid = Int32.Parse(prisonerid);

                prisoner prisoner = db.prisoner.Find(visitor.prisonerid);
                int allocated_time = Int32.Parse(prisoner.allocated_meeting_time);
               
                var time = (from v in db.visitor
                            where v.prisonerid == visitor.prisonerid
                            select v.duration).ToList();
                int totalTime = 0;
                foreach (var t in time)
                {
                    int to = Int32.Parse(t);
                    totalTime += to;
                }

                totalTime += Int32.Parse(visitor.duration);

                if (totalTime >= allocated_time)
                {
                    return RedirectToAction("NoAvailableTime", "jail_officer");
                }


                
                
                int available_time = Int32.Parse(prisoner.available_meeting_time);
                available_time -= Int32.Parse(visitor.duration);
                prisoner.available_meeting_time = available_time.ToString();

                db.visitor.Add(visitor);
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = prisonerid });
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", visitor.prisonerid);
            return View(visitor);
        }

        // GET: visitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            visitor visitor = db.visitor.Find(id);
            Session["prisonerID"] = visitor.prisonerid;
            if (visitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", visitor.prisonerid);
            return View(visitor);
        }

        // POST: visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,contact,address,dob,date_of_visit,duration,image,prisonerid")] visitor visitor)
        {
            if (ModelState.IsValid)
            {
                String ids = Session["prisonerID"].ToString();
                int id = Int32.Parse(ids);
                prisoner prisoner = db.prisoner.Find(id);
                int allocated_time = Int32.Parse(prisoner.allocated_meeting_time);

                var time = (from v in db.visitor
                            where v.prisonerid == id
                            select v.duration).ToList();
                int totalTime = 0;
                foreach (var t in time)
                {
                    int to = Int32.Parse(t);
                    totalTime += to;
                }

                totalTime += Int32.Parse(visitor.duration);

                if (totalTime >= allocated_time)
                {
                    return RedirectToAction("NoAvailableTime", "jail_officer");
                }

                db.Entry(visitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = visitor.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", visitor.prisonerid);
            return View(visitor);
        }

        // GET: visitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            visitor visitor = db.visitor.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: visitors/Delete/5
       
        public ActionResult DeleteConfirmed(int id)
        {
            visitor visitor = db.visitor.Find(id);
            var prisonerid = visitor.prisonerid;
            var prisoner = db.prisoner.SingleOrDefault(f => f.id == prisonerid);
            int oldTime = Int32.Parse(prisoner.available_meeting_time);
            int newTime = Int32.Parse(visitor.duration);
            prisoner.available_meeting_time = (oldTime + newTime).ToString();
            db.visitor.Remove(visitor);
            db.SaveChanges();
            return RedirectToAction("Details", "prisoners", new { id = prisonerid });
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
