//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using TST.Data.EF;
//using Microsoft.AspNet.Identity;


//namespace TSTmvc.Controllers
//{
//    [Authorize]
//    public class TST_SupportTicketsController : Controller
//    {
//        private TSTEntities db = new TSTEntities();

//        // GET: TST_SupportTickets
//        public ActionResult Index(bool ShowInactive = false)
//        {
//            IEnumerable<TST_SupportTickets> tST_SupportTickets = db.TST_SupportTickets.Include(t => t.TST_Employees).Include(t => t.TST_Employees1).Include(t => t.TST_Employees2).Include(t => t.TST_TicketPriorities).Include(t => t.TST_TicketStatuses);
//            return View(tST_SupportTickets.ToList());

//            //get the employee for the current user
//            var userId = User.Identity.GetUserId();

//            TST_Employees employee = db.TST_Employees.FirstOrDefault(x => x.UserId == userId);

//            //Use a LINQ extension
//            //tST_SupportTickets = tST_SupportTickets.Where(t=>t.SubmittedByEmployeeId == employee.EmployeeId || t.TechId == employee.EmployeeId);

//            //or LINQ query
//            tST_SupportTickets = from t in tST_SupportTickets
//                                 where t.SubmittedByEmployeeId ==
//                                 employee.EmployeeId || t.TechId == employee.EmployeeId
//                                 select t;
//        }

//        // GET: TST_SupportTickets/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
//            if (tST_SupportTickets == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tST_SupportTickets);
//        }

//        // GET: TST_SupportTickets/Create
//        public ActionResult Create()
//        {
//            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName");
//            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FullName");
//            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FullName");
//            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name");
//            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name");
//            ViewBag.TicketId = new SelectList(db.TST_SupportTickets, "TicketId");
//            return View();
//        }

//        // POST: TST_SupportTickets/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "TicketId,TechId,Subject,Description,PriorityId,StatusId")] TST_SupportTickets tST_SupportTickets, TST_Employees tST_Employees)
//        {

//            if (ModelState.IsValid)
//            {

//                var newTicket = new TST_SupportTickets();

//                tST_SupportTickets.DateSubmitted = DateTime.Now;//current time


//                //get the user id
//                var userId = User.Identity.GetUserId();


//                //get the employee associated with that user
//                TST_Employees employee = db.TST_Employees.FirstOrDefault(x => x.UserId == userId);

//                if (employee != null)
//                {
//                    //set the employeeId that submitted the ticket
//                    tST_SupportTickets.SubmittedByEmployeeId = employee.EmployeeId;
//                }

//                newTicket.TechId = tST_SupportTickets.TechId;
//                newTicket.PriorityId = tST_SupportTickets.PriorityId;
//                newTicket.StatusId = 1;

//                db.TST_SupportTickets.Add(tST_SupportTickets);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);
//            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.TechId);
//            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.CancelledBy);
//            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
//            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);

//            return View(tST_SupportTickets);
//        }

//        // GET: TST_SupportTickets/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
//            if (tST_SupportTickets == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);
//            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.TechId);
//            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.CancelledBy);
//            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
//            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);
//            return View(tST_SupportTickets);
//        }

//        // POST: TST_SupportTickets/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "TicketId,SubmittedByEmployeeId,TechId,Subject,Description,DateSubmitted,PriorityId,StatusId,CancelledBy,DateClosed")] TST_SupportTickets tST_SupportTickets)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(tST_SupportTickets).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.SubmittedByEmployeeId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.SubmittedByEmployeeId);
//            ViewBag.TechId = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.TechId);
//            ViewBag.CancelledBy = new SelectList(db.TST_Employees, "EmployeeId", "FirstName", tST_SupportTickets.CancelledBy);
//            ViewBag.PriorityId = new SelectList(db.TST_TicketPriorities, "PriorityId", "Name", tST_SupportTickets.PriorityId);
//            ViewBag.StatusId = new SelectList(db.TST_TicketStatuses, "TicketStatusId", "Name", tST_SupportTickets.StatusId);
//            return View(tST_SupportTickets);
//        }

//        // GET: TST_SupportTickets/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
//            if (tST_SupportTickets == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tST_SupportTickets);
//        }

//        // POST: TST_SupportTickets/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            TST_SupportTickets tST_SupportTickets = db.TST_SupportTickets.Find(id);
//            db.TST_SupportTickets.Remove(tST_SupportTickets);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }



//    }

//}
