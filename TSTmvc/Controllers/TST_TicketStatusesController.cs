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
    public class TST_TicketStatusesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_TicketStatuses
        public ActionResult Index()
        {
            return View(db.TST_TicketStatuses.ToList());
        }

        // GET: TST_TicketStatuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketStatuses tST_TicketStatuses = db.TST_TicketStatuses.Find(id);
            if (tST_TicketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketStatuses);
        }

        // GET: TST_TicketStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TST_TicketStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketStatusId,Name")] TST_TicketStatuses tST_TicketStatuses)
        {
            if (ModelState.IsValid)
            {
                db.TST_TicketStatuses.Add(tST_TicketStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tST_TicketStatuses);
        }

        // GET: TST_TicketStatuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketStatuses tST_TicketStatuses = db.TST_TicketStatuses.Find(id);
            if (tST_TicketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketStatuses);
        }

        // POST: TST_TicketStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketStatusId,Name")] TST_TicketStatuses tST_TicketStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_TicketStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tST_TicketStatuses);
        }

        // GET: TST_TicketStatuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketStatuses tST_TicketStatuses = db.TST_TicketStatuses.Find(id);
            if (tST_TicketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketStatuses);
        }

        // POST: TST_TicketStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_TicketStatuses tST_TicketStatuses = db.TST_TicketStatuses.Find(id);
            db.TST_TicketStatuses.Remove(tST_TicketStatuses);
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
