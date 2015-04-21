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
    [Authorize(Roles = "HRAdmin, SuperAdmin")]
    public class TST_DepartmentsController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_Departments
        public ActionResult Index(bool showInactive = false)
        {
            //add below line for soft delete
            var tST_Departments = db.TST_Departments.Include(t => t.TST_Employees).Where(x => x.IsActive == !showInactive);
            return View(tST_Departments.ToList());
        }

        // GET: TST_Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Departments tST_Departments = db.TST_Departments.Find(id);
            if (tST_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tST_Departments);
        }

        // GET: TST_Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TST_Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,Description,IsActive")] TST_Departments tST_Departments)
        {
            if (ModelState.IsValid)
            {
                db.TST_Departments.Add(tST_Departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tST_Departments);
        }

        // GET: TST_Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Departments tST_Departments = db.TST_Departments.Find(id);
            if (tST_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tST_Departments);
        }

        // POST: TST_Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,Description,IsActive")] TST_Departments tST_Departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_Departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tST_Departments);
        }

        [Authorize(Roles = "SuperAdmin")]
        // GET: TST_Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Departments tST_Departments = db.TST_Departments.Find(id);
            if (tST_Departments == null)
            {
                return HttpNotFound();
            }
            return View(tST_Departments);
        }

        [Authorize(Roles = "SuperAdmin")]
        // POST: TST_Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_Departments tST_Departments = db.TST_Departments.Find(id);
            tST_Departments.IsActive = false;
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
