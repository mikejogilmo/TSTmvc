using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSTmvc.Controllers
{
    public class EmployeeStatusController : Controller
    {
        // GET: EmployeeStatus
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeStatus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeStatus/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeStatus/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
