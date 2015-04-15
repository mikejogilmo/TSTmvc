using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST.Data.EF;

namespace TSTmvc.Controllers
{
    [Authorize(Roles="HRAdmin, SuperAdmin")]
    public class TST_EmployeeStatusesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_EmployeeStatuses
        public ActionResult Index()
        {
            return View(db.TST_EmployeeStatuses.ToList());
        }

        // GET: TST_EmployeeStatuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_EmployeeStatuses tST_EmployeeStatuses = db.TST_EmployeeStatuses.Find(id);
            if (tST_EmployeeStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_EmployeeStatuses);
        }

        // GET: TST_EmployeeStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TST_EmployeeStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeStatusId,Name")] TST_EmployeeStatuses tST_EmployeeStatuses)
        {
            if (ModelState.IsValid)
            {
                db.TST_EmployeeStatuses.Add(tST_EmployeeStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tST_EmployeeStatuses);
        }

        // GET: TST_EmployeeStatuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_EmployeeStatuses tST_EmployeeStatuses = db.TST_EmployeeStatuses.Find(id);
            if (tST_EmployeeStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_EmployeeStatuses);
        }

        // POST: TST_EmployeeStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeStatusId,Name")] TST_EmployeeStatuses tST_EmployeeStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_EmployeeStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tST_EmployeeStatuses);
        }

        // GET: TST_EmployeeStatuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_EmployeeStatuses tST_EmployeeStatuses = db.TST_EmployeeStatuses.Find(id);
            if (tST_EmployeeStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_EmployeeStatuses);
        }

        // POST: TST_EmployeeStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_EmployeeStatuses tST_EmployeeStatuses = db.TST_EmployeeStatuses.Find(id);
            db.TST_EmployeeStatuses.Remove(tST_EmployeeStatuses);
            db.SaveChanges();
            return RedirectToAction("Index");
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
