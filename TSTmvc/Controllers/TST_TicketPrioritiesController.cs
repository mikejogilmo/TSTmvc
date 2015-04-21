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
    public class TST_TicketPrioritiesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_TicketPriorities
        public ActionResult Index()
        {
            return View(db.TST_TicketPriorities.ToList());
        }

        // GET: TST_TicketPriorities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketPriorities tST_TicketPriorities = db.TST_TicketPriorities.Find(id);
            if (tST_TicketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketPriorities);
        }

        // GET: TST_TicketPriorities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TST_TicketPriorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriorityId,Name")] TST_TicketPriorities tST_TicketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.TST_TicketPriorities.Add(tST_TicketPriorities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tST_TicketPriorities);
        }

        // GET: TST_TicketPriorities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketPriorities tST_TicketPriorities = db.TST_TicketPriorities.Find(id);
            if (tST_TicketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketPriorities);
        }

        // POST: TST_TicketPriorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriorityId,Name")] TST_TicketPriorities tST_TicketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tST_TicketPriorities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tST_TicketPriorities);
        }

        // GET: TST_TicketPriorities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_TicketPriorities tST_TicketPriorities = db.TST_TicketPriorities.Find(id);
            if (tST_TicketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(tST_TicketPriorities);
        }

        // POST: TST_TicketPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_TicketPriorities tST_TicketPriorities = db.TST_TicketPriorities.Find(id);
            db.TST_TicketPriorities.Remove(tST_TicketPriorities);
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
