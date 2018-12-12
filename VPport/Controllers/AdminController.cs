using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace VPport.Controllers
{
    public class AdminController : Controller
    {
        private dbPortEntities db = new dbPortEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return Redirect("~/Home/Index");
        }
        // GET: Projects
        public ActionResult Projects()
        {
            return View(db.Projects.ToList());
        }

        // GET: ProjectDetails/{id}
        public ActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // GET: Projects/Create
        public ActionResult CreateProject()
        {
            return View();
        }

        // POST: CreateProject
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject([Bind(Include = "Id,Title,Teaser,Image,Symbol,Description")] Project project, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string[] Ex = { ".jpg", ".png", ".gif"};
                IOTools.FileUploader("~/Content/img/projects", FileId.ToString(), Image, Ex);

                project.Image = FileId + "_" + Image.FileName;

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Projects");
            }

            return View(project);
        }
        // GET: EditProject/{id}
        public ActionResult EditProject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: EditProject/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject([Bind(Include = "Id,Title,Teaser,Image,Symbol,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Projects");
            }
            return View(project);
        }

        // GET: Delete/{id}
        public ActionResult DeleteProject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Delete/{id}
        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Projects");
        }

        // About page

        // GET: EditAbout/1
        public ActionResult EditAbout(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: EditAbout/1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAbout([Bind(Include = "Id,About")] About about)
        {
            if (ModelState.IsValid)
            {
                db.Entry(about).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(about);
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