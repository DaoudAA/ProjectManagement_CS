using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProjectsController : Controller
    {
        private CsharpCBContext db = new CsharpCBContext();
        private readonly UserManager<ApplicationUser> _userManager;

        // GET: Projects
        public ActionResult  Index()
        {
            string userId = User.Identity.GetUserId();
            var projects = db.Project
                             .Where(p => p.Members.Any(m => m.UserID == userId))
                             .ToList();

            return View(projects);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Project.Include(p => p.Tasks).SingleOrDefault(p => p.ID == id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Db = db;
            ViewBag.Tasks = project.Tasks.ToList();
            return View(project);
        }


        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Code,Description,StartDate")] Project project)
        {
            if (ModelState.IsValid)
            {   

                string userId = User.Identity.GetUserId();
                Models.Member m = db.Member.Where(mem=>mem.UserID == userId).First();
                project.Members.Add(m);
               
                project.Manager = m;
                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Code,Description,StartDate")] Project project)
        {
            if (ModelState.IsValid)
            {   
                db.Entry(project).State = EntityState.Modified;
                string userId = User.Identity.GetUserId();
                Models.Member m = db.Member.Where(mem => mem.UserID == userId).First();
                project.Members.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
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
