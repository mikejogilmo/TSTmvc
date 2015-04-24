using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TST.Data.EF;

using TSTmvc.Models;

using System.Net.Mail;//for email

//for RoleManager and UserManager
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;


namespace TSTmvc.Controllers
{
    [Authorize]
    public class TST_EmployeesController : Controller
    {
        private TSTEntities db = new TSTEntities();

        // GET: TST_Employees                       //add '?' to make it nullable
        public ActionResult Index(string searchText, int? DepartmentId)
        {
            //grab all of the employees that have an active status
            IEnumerable<TST_Employees> tST_Employees = db.TST_Employees.Include(t => t.TST_Departments).Include(t => t.TST_EmployeeStatuses).Where(x => x.StatusId == 1);

            //text search filter
            //make sure the searchText has a value
            if (!String.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToUpper();

                tST_Employees = from emp in tST_Employees.ToList()
                                where emp.FullName.ToUpper().Contains(searchText) ||
                                      emp.Email.ToUpper().Contains(searchText) ||
                                      emp.JobTitle.ToUpper().Contains(searchText) ||
                                    //add Notes!=null to prevent a blow up for running ToUpper() on a null type
                                      emp.Notes != null && emp.Notes.ToUpper().Contains(searchText)
                                select emp;
            }

            //dept search
            if (DepartmentId != null)
            {
                tST_Employees = from emp in tST_Employees
                                where emp.DepartmentId == DepartmentId
                                select emp;
            }

            //dropdown lists for dept and status
            ViewBag.DepartmentId = new SelectList(db.TST_Departments.Where(x => x.IsActive).OrderBy(x => x.Name), "DepartmentId", "Name");

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
            var RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

            //create a list of Techadmins for notifications
            //ApplicationRole role = RoleManager.FindByName("TechAdmin");



            //create list for check box list
            ViewBag.RoleId = new SelectList(RoleManager.Roles.ToList(), "Name", "Name");
            ViewBag.DepartmentId = new SelectList(db.TST_Departments, "DepartmentId", "Name");
            ViewBag.StatusId = new SelectList(db.TST_EmployeeStatuses, "EmployeeStatusId", "Name");
            //var emp = new TST_Employees();
            //emp.CreatedOn = DateTime.Now;

            return View();
        }

        // POST: TST_Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,DepartmentId,Email,WorkPhone,CellPhone,Street,City,State,Zip,PhotoUrl,StatusId,Notes,SSN,DateOfBirth,JobTitle,HireDate,TerminationDate")] TST_Employees tST_Employees, string[] selectedRoles, HttpPostedFileBase employeeImage)
        //^^add a parameter of type HttpPostedFileBase that is named the 
        //same as the input type=file control from the view
        {
            
            tST_Employees.CreatedBy = User.Identity.Name;//current logged on user
            tST_Employees.CreatedOn = DateTime.Now;//current time

            if (ModelState.IsValid)
            {
                //create the Usermanager
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                //create the Application User
                var newUser = new ApplicationUser()
                {
                    UserName = tST_Employees.Email,
                    Email = tST_Employees.Email
                };

                userManager.Create(newUser, "P@ssw0rd");//added user to membership

                if (selectedRoles != null)
                {
                    userManager.AddToRoles(newUser.Id, selectedRoles);
                }

                MailMessage msg = new MailMessage(
                    "mikejogilmo@gmail.com", //from
                    newUser.Email, //to
                    "New Account - Action Required", //subject
                    "An account has been created for you on TST.  Your username is " + newUser.Email + " And your password is P@ssw0rd");

                using (SmtpClient client = new SmtpClient("relay-hosting.secureServer.net"))
                {
                    //client.EnableSsl = true;
                    //client.UseDefaultCredentials = false;
                    //client.Credentials = new System.Net.NetworkCredential("centriqRelay@gmail.com", "c3ntriQc3ntriQ");
                    //client.Port = 587;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    //client.Send(msg);
                }
                //assign the new UserId to the employee object
                tST_Employees.UserId = newUser.Id;

                //image upload process
                var image = "no-photo.jpg";

                //make sure the user uploaded an image
                if (employeeImage != null)
                {
                    //get the file name
                    string fileName = employeeImage.FileName;

                    //get the extension
                    string ext = fileName.Substring(fileName.LastIndexOf('.'));//everything after the last '.'

                    //generate a new file name using a GUID
                    image = Guid.NewGuid().ToString() + ext;

                    //save the image to a "EmployeePhotos" directory
                    employeeImage.SaveAs(Server.MapPath("~/Content/EmployeePhotos/" + image));

                }
                //add the imageName to the Employee Object
                tST_Employees.PhotoUrl = image;

                tST_Employees.StatusId = 1;

                //add the employee to the DB
                db.TST_Employees.Add(tST_Employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

            //create list for check box list
            ViewBag.RoleId = new SelectList(RoleManager.Roles.ToList(), "Name", "Name");

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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,EmployeeId,FirstName,LastName,DepartmentId,Email,WorkPhone,CellPhone,Street,City,State,Zip,PhotoUrl,StatusId,CreatedOn,CreatedBy,Notes,SSN,DateOfBirth,JobTitle,HireDate,TerminationDate")] TST_Employees tST_Employees, HttpPostedFileBase employeeImage)
        {
            //check to see if the user uploaded the file
            if (employeeImage != null)
            {
                //get the file name
                string fileName = employeeImage.FileName;

                //get the extension
                string ext = fileName.Substring(fileName.LastIndexOf('.'));

                //rename the file
                fileName = Guid.NewGuid() + ext;

                //save the file to the EmployeePhotos folder
                employeeImage.SaveAs(Server.MapPath("~/Content/EmployeePhotos/" + fileName));

                //save the fileName to the Employee object
                tST_Employees.PhotoUrl = fileName;
            }
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
