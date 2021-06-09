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
    public class beneficiariesController : Controller
    {
        private prisonerManagementSystemEntities db = new prisonerManagementSystemEntities();

        // GET: beneficiaries
        public ActionResult Index()
        {
            var beneficiary = db.beneficiary.Include(b => b.prisoner);
            return View(beneficiary.ToList());
        }

        // GET: beneficiaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beneficiary beneficiary = db.beneficiary.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // GET: beneficiaries/Create
        public ActionResult Create(int id)
        {
            Session["prisonerID"] = id;
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name");
            return View();
        }

        // POST: beneficiaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cnic,contact,address,dob,image,prisonerid")] beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                if (beneficiary.image == null)
                {
                    beneficiary.image = "userava.png";
                }

                String prisonerid = Session["prisonerID"].ToString();
                beneficiary.prisonerid = Int32.Parse(prisonerid);
                db.beneficiary.Add(beneficiary);
                db.SaveChanges();
                return RedirectToAction("Details", new {id = beneficiary.id});
            }

            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", beneficiary.prisonerid);
            return View(beneficiary);
        }

        // GET: beneficiaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beneficiary beneficiary = db.beneficiary.Find(id);
            Session["prisonerID"] = beneficiary.prisonerid;
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", beneficiary.prisonerid);
            return View(beneficiary);
        }

        // POST: beneficiaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cnic,contact,address,dob,image,prisonerid")] beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "prisoners", new { id = beneficiary.prisonerid });
            }
            ViewBag.prisonerid = new SelectList(db.prisoner, "id", "name", beneficiary.prisonerid);
            return View(beneficiary);
        }

        // GET: beneficiaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beneficiary beneficiary = db.beneficiary.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        // POST: beneficiaries/Delete/5
       
        public ActionResult DeleteConfirmed(int id)
        {
            beneficiary beneficiary = db.beneficiary.Find(id);
            var prisoner_id = beneficiary.prisonerid;
            db.beneficiary.Remove(beneficiary);
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
