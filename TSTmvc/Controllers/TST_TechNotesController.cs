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
    public class TST_TechNotesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_TechNotes
        public ActionResult Index()
        {
            var tST_TechNotes = db.TST_TechNotes.Include(t => t.TST_Employees).Include(t => t.TST_SupportTickets);
            return View(tST_TechNotes.ToList());
        }

        // GET: TST_TechNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TechNotes tST_TechNotes = db.TST_TechNotes.Find(id);
            if (tST_TechNotes == null)
            {
                return HttpNotFound();
            }
            return View(tST_TechNotes);
        }

        // GET: TST_TechNotes/Create
        public ActionResult Create()
        {
            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName");
            ViewBag.TicketId = new SelectList(db.TST_SupportTickets, "TicketId", "Subject");
            return View();
        }

        // POST: TST_TechNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TechNoteId,TicketId,TechId,Note,NoteSubmittedOn")] TST_TechNotes tST_TechNotes)
        {
            if (ModelState.IsValid)
            {
                db.TST_TechNotes.Add(tST_TechNotes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_TechNotes.TechId);
            ViewBag.TicketId = new SelectList(db.TST_SupportTickets, "TicketId", "Subject", tST_TechNotes.TicketId);
            return View(tST_TechNotes);
        }

        // GET: TST_TechNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TechNotes tST_TechNotes = db.TST_TechNotes.Find(id);
            if (tST_TechNotes == null)
            {
                return HttpNotFound();
            }
            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_TechNotes.TechId);
            ViewBag.TicketId = new SelectList(db.TST_SupportTickets, "TicketId", "Subject", tST_TechNotes.TicketId);
            return View(tST_TechNotes);
        }

        // POST: TST_TechNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TechNoteId,TicketId,TechId,Note,NoteSubmittedOn")] TST_TechNotes tST_TechNotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_TechNotes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_TechNotes.TechId);
            ViewBag.TicketId = new SelectList(db.TST_SupportTickets, "TicketId", "Subject", tST_TechNotes.TicketId);
            return View(tST_TechNotes);
        }

        // GET: TST_TechNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TechNotes tST_TechNotes = db.TST_TechNotes.Find(id);
            if (tST_TechNotes == null)
            {
                return HttpNotFound();
            }
            return View(tST_TechNotes);
        }

        // POST: TST_TechNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_TechNotes tST_TechNotes = db.TST_TechNotes.Find(id);
            db.TST_TechNotes.Remove(tST_TechNotes);
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
