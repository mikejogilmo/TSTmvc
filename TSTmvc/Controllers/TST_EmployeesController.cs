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
    [Authorize]
    public class TST_EmployeesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_Employees
        public ActionResult Index()
        {
            var tST_Employees = db.TST_Employees.Include(t => t.TST_Departments).Include(t => t.TST_EmployeeStatuses);
            return View(tST_Employees.ToList());
        }

        // GET: TST_Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Employees tST_Employees = db.TST_Employees.Find(id);
            if (tST_Employees == null)
            {
                return HttpNotFound();
            }
            return View(tST_Employees);
        }

        [Authorize(Roles = "HRAdmin, SuperAdmin")]
        // GET: TST_Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.TST_Departments, "DepartmentId", "Name");
            ViewBag.StatusId = new SelectList(db.TST_EmployeeStatuses, "EmployeeStatusId", "Name");
            return View();
        }

        // POST: TST_Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,DepartmentId,Email,WorkPhone,CellPhone,UserId,Street,City,State,Zip,PhotoUrl,StatusId,CreatedOn,CreatedBy,Notes,SSN,DateOfBirth,JobTitle,HireDate,TerminationDate")] TST_Employees tST_Employees)
        {
            if (ModelState.IsValid)
            {
                db.TST_Employees.Add(tST_Employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.TST_Departments, "DepartmentId", "Name", tST_Employees.DepartmentId);
            ViewBag.StatusId = new SelectList(db.TST_EmployeeStatuses, "EmployeeStatusId", "Name", tST_Employees.StatusId);
            return View(tST_Employees);
        }

        [Authorize]
        // GET: TST_Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Employees tST_Employees = db.TST_Employees.Find(id);
            if (tST_Employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.TST_Departments, "DepartmentId", "Name", tST_Employees.DepartmentId);
            ViewBag.StatusId = new SelectList(db.TST_EmployeeStatuses, "EmployeeStatusId", "Name", tST_Employees.StatusId);
            return View(tST_Employees);
        }

        // POST: TST_Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,DepartmentId,Email,WorkPhone,CellPhone,UserId,Street,City,State,Zip,PhotoUrl,StatusId,CreatedOn,CreatedBy,Notes,SSN,DateOfBirth,JobTitle,HireDate,TerminationDate")] TST_Employees tST_Employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_Employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.TST_Departments, "DepartmentId", "Name", tST_Employees.DepartmentId);
            ViewBag.StatusId = new SelectList(db.TST_EmployeeStatuses, "EmployeeStatusId", "Name", tST_Employees.StatusId);
            return View(tST_Employees);
        }

        [Authorize(Roles = "HRAdmin, SuperAdmin")]
        // GET: TST_Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_Employees tST_Employees = db.TST_Employees.Find(id);
            if (tST_Employees == null)
            {
                return HttpNotFound();
            }
            return View(tST_Employees);
        }
        [Authorize(Roles = "HRAdmin, SuperAdmin")]
        // POST: TST_Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_Employees tST_Employees = db.TST_Employees.Find(id);
            db.TST_Employees.Remove(tST_Employees);
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
