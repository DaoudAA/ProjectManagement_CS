using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MembersController : Controller
    {
        private CsharpCBContext db = new CsharpCBContext();

        public ActionResult Index()
        {
            return View(db.Member.ToList());
        }


        //GET
        public ActionResult AddMember(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberProjectModel model = new MemberProjectModel(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();

            var projects = db.Project.Select(p => new { p.ID,p.Manager, DisplayText = p.Code + " : " + p.Description }).Where(p => p.Manager.UserID == userId).ToList();
            ViewBag.ProjectId = new SelectList(projects, "ID", "DisplayText");

            return View(model);
        }

        //POST
        [HttpPost, ActionName("AddMember")]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember([Bind(Include = "ProjectId,MemberId")] MemberProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Retrieve the project by projectId
            var project = db.Project.Find(model.ProjectId);

            if (project == null)
            {
                return HttpNotFound(); // Handle project not found
            }

            Member member = db.Member.Where(m => m.UserID == model.MemberId).First();
            // Check if member exists (replace 'viewModel.MemberId' with the appropriate property)

            if (member == null)
            {
                ModelState.AddModelError("", "Member not found.");
                return View(model);
            }

            // Add member to the project's Members collection
            project.Members.Add(member);

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("Index"); // Redirect to project index or details page
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
