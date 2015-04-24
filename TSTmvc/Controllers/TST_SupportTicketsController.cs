using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST.Data.EF;
using Microsoft.AspNet.Identity;
using TSTmvc.Enums;
using PagedList.Mvc;
using TST.Data.EF.Results;


namespace TSTmvc.Controllers
{
    [Authorize]
    public class TST_SupportTicketsController : Controller
    {
        private TSTEntities db = new TSTEntities();


        // GET: TST_SupportTickets
        public ActionResult Index(TicketSortOrderEnum sortOrder = TicketSortOrderEnum.PriorityId, int? page = 1)
        {
            ViewBag.CurrentSort = sortOrder;

            var tST_SupportTickets = db.TST_SupportTickets.Include(t => t.TST_Employees).Include(t => t.TST_Employees1).Include(t => t.TST_Employees2).Include(t => t.TST_TicketPriorities).Include(t => t.TST_TicketStatuses);//.Where(x=>x.StatusId == 4 || x.StatusId == 2)
            //return View(tST_SupportTickets.ToList());

            //get the employee for the current user
            var userId = User.Identity.GetUserId();

            TST_Employees employee = db.TST_Employees.FirstOrDefault(x => x.UserId == userId);

            if (User.IsInRole("Tech"))
            {

                //supportTickets = supportTickets.Where(t=> t.SubmittedBy == employee.EmployeeId || t.TechId == employee.EmployeeId);

                tST_SupportTickets = from t in tST_SupportTickets
                                     where t.SubmittedByEmployeeId == employee.EmployeeId ||
                                           t.TechId == employee.EmployeeId
                                     select t;
            }
            else if (User.IsInRole("User") || User.IsInRole("HRAdmin"))
            {
                tST_SupportTickets = from t in tST_SupportTickets
                                     where t.SubmittedByEmployeeId == employee.EmployeeId
                                     select t;
            }

            List<TST_SupportTickets> results = null;
            switch (sortOrder)
            {
                case TicketSortOrderEnum.PriorityId:
                    results = tST_SupportTickets.OrderByDescending(x => x.PriorityId).ToList();
                    break;
                case TicketSortOrderEnum.StatusId:
                    results = tST_SupportTickets.OrderBy(x => x.StatusId).ToList();
                    break;
                case TicketSortOrderEnum.DateSubmitted:
                    results = tST_SupportTickets.OrderBy(x => x.DateSubmitted).ToList();
                    break;
                case TicketSortOrderEnum.TechId:
                    results = tST_SupportTickets.OrderBy(x => x.TechId).ToList();
                    break;
                case TicketSortOrderEnum.SubmittedByEmployeeId:
                    results = tST_SupportTickets.OrderBy(x => x.SubmittedByEmployeeId).ToList();
                    break;
            }

            //var ticketCount = employee.TST_SupportTickets1.Where(x => x.StatusId == 1).Count();

            return View(results);
        }

        // GET: TST_SupportTickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
            if (tST_SupportTickets == null)
            {
                return HttpNotFound();
            }
            return View(tST_SupportTickets);
        }

        // GET: TST_SupportTickets/Create
        public ActionResult Create()
        {
            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName");
            ViewBag.TechId = new SelectList(db.TST_Employees.Where(x => x.DepartmentId == 5), "EmployeeId", "FullName");
            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName");
            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name");
            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name");
            return View();
        }

        // POST: TST_SupportTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusId,TicketId,Subject,Description")] TST_SupportTickets tST_SupportTickets)
        {
            tST_SupportTickets.DateSubmitted = DateTime.Now;//current time

            //get the user id
            var userId = User.Identity.GetUserId();
            //get the employee associated with that user
            TST_Employees employee = db.TST_Employees.FirstOrDefault(x => x.UserId == userId);
            if (employee != null)
            {
                //set the employeeId that submitted the ticket
                tST_SupportTickets.SubmittedByEmployeeId = employee.EmployeeId;
            }

            if (tST_SupportTickets.TechId == null)
            {
                tST_SupportTickets.StatusId = 5;
            }
            else
            {
                tST_SupportTickets.StatusId = 1;
            }

            if (tST_SupportTickets.PriorityId == null)
            {
                tST_SupportTickets.PriorityId = 2;
            }

            if (ModelState.IsValid)
            {
                db.TST_SupportTickets.Add(tST_SupportTickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);

            ViewBag.TechId = new SelectList(db.TST_Employees.Where(x => x.DepartmentId == 5), "EmployeeId", "FullName", tST_SupportTickets.TechId);
            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FullName", tST_SupportTickets.CancelledBy);
            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);

            return View(tST_SupportTickets);
        }

        // GET: TST_SupportTickets/Edit/5
        [Authorize(Roles = "SuperAdmin, TechAdmin, Tech")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
            if (tST_SupportTickets == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);
            ViewBag.TechId = new SelectList(db.TST_Employees.Where(x => x.DepartmentId == 5), "EmployeeId", "FullName");
            //ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.CancelledBy);
            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);
            return View(tST_SupportTickets);
        }

        // POST: TST_SupportTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "SuperAdmin, TechAdmin, Tech")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,TicketId,SubmittedByEmployeeId,TechId,Subject,Description,DateSubmitted,PriorityId,StatusId")] TST_SupportTickets tST_SupportTickets)
        {
            if (tST_SupportTickets.TechId != null)
            {
                tST_SupportTickets.StatusId = 1;
            }
            if (ModelState.IsValid)
            {
                db.Entry(tST_SupportTickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);
            ViewBag.TechId = new SelectList(db.TST_Employees.Where(x => x.DepartmentId == 5), "EmployeeId", "FullName", tST_SupportTickets.TechId);
            //ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.CancelledBy);
            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);
            return View(tST_SupportTickets);
        }

        // GET: TST_SupportTickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
            if (tST_SupportTickets == null)
            {
                return HttpNotFound();
            }
            return View(tST_SupportTickets);
        }

        // POST: TST_SupportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
            db.TST_SupportTickets.Remove(tST_SupportTickets);
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

        //Tech note via ajax using JSOn
        public JsonResult AddTechNote(int ticketId, string note)
        {
            //get the ticket
            TST_SupportTickets ticket = db.TST_SupportTickets.FirstOrDefault(x => x.TicketId == ticketId);

            //get the tech employee
            TST_Employees tech = db.TST_Employees.ToList().FirstOrDefault(x => x.UserId == User.Identity.GetUserId());

            if (tech != null)
            {
                //create the note
                TST_TechNotes newNote = new TST_TechNotes()
                {
                    TST_SupportTickets = ticket,
                    TechId = tech.EmployeeId,
                    NoteSubmittedOn = DateTime.Now,
                    Note = note
                };

                //add the note to the DB
                db.TST_TechNotes.Add(newNote);
                db.SaveChanges();

                //return the Data to the view to be displayed
                var data = new
                {
                    TechNotes = newNote.Note,
                    Tech = newNote.TST_Employees.FullName,
                    Date = string.Format("{0:D}", newNote.NoteSubmittedOn)
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}
