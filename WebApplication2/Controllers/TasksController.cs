using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class TasksController : Controller
    {
        private CsharpCBContext db = new CsharpCBContext();

        // GET: Tasks
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            return View(db.Task.Where(t => t.Project.Members.Any(m => m.UserID == userId)).ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            var projects = db.Project.Select(p => new { p.ID,p.Members, DisplayText = p.Code + " : " + p.Description }).Where(p => p.Members.Any(m => m.UserID == userId)).ToList();
            ViewBag.ProjectId = new SelectList(projects, "ID", "DisplayText");
            return View();
        }

        // POST: Tasks/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Code,Description,StartDate,EndDate,Project")] Task task)
        {
            if (ModelState.IsValid)
            {
                var project = db.Project.Find(task.Project.ID);
                if (project != null)
                {
                    string userId = User.Identity.GetUserId();
                    task.UserId = userId;
                    task.Project = project;
                    db.Task.Add(task);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Project_ID", "Invalid ProjectId");
                }
            }

           
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            var projects = db.Project.Select(p => new { p.ID, DisplayText = p.Code + " : " + p.Description }).ToList();
            ViewBag.ProjectId = new SelectList(projects, "ID", "DisplayText", task.Project.ID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,Description,StartDate,EndDate,Project")] Task task)
        {
            if (ModelState.IsValid)
            {
                var project = db.Project.Find(task.Project.ID);
                if (project != null)
                {
                    string userId = User.Identity.GetUserId();
                    task.UserId = userId;
                    task.Project = project;
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Project_ID", "Invalid ProjectId");
                }
            }

            var projects = db.Project.Select(p => new { p.ID, DisplayText = p.Code + " : " + p.Description }).ToList();
            ViewBag.ProjectId = new SelectList(projects, "ID", "DisplayText", task.Project);
            return View(task);
        }
        

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Task.Find(id);
            db.Task.Remove(task);
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
