using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TrelloAlt.Models;

namespace TrelloAlt.Controllers
{
    public class ToDoActModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoActModels
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<ToDoActModel> GetMyToDoActs()
        {
            // custom
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);

            IEnumerable<ToDoActModel> myToDoAct = db.ToDoActModels.ToList().Where(x => x.User == currentUser);

            int completeCount = 0;

            foreach (ToDoActModel toDoActModel in myToDoAct)
            {
                if (toDoActModel.IsDone)
                {
                    completeCount++;
                }
            }

            ViewBag.Percent = Math.Round(100f*((float)completeCount/(float)myToDoAct.Count()));

            return myToDoAct;
            //
        }

        public ActionResult BuildDashboardTable()
        {

            return PartialView("_Dashboard",
                GetMyToDoActs());
        }

        // GET: ToDoActModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoActModel toDoActModel = db.ToDoActModels.Find(id);
            if (toDoActModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoActModel);
        }

        // GET: ToDoActModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoActModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "Id,Description")] ToDoActModel toDoActModel)
        {
            if (ModelState.IsValid)
            {
                // custom
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                toDoActModel.User = currentUser;
                toDoActModel.IsDone = false;
                //
                db.ToDoActModels.Add(toDoActModel);
                db.SaveChanges();
            }
            return PartialView("_Dashboard",
                GetMyToDoActs());
        }

        // GET: ToDoActModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoActModel toDoActModel = db.ToDoActModels.Find(id);

            if (toDoActModel == null)
            {
                return HttpNotFound();
            }

            // custom
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);
            //

            if (toDoActModel.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(toDoActModel);
        }

        // POST: ToDoActModels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDone")] ToDoActModel toDoActModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoActModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoActModel);
        }

        [HttpPost]
        public ActionResult AjaxEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoActModel toDoActModel = db.ToDoActModels.Find(id);
            if (toDoActModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDoActModel.IsDone = value;
                db.Entry(toDoActModel).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Dashboard",
                    GetMyToDoActs());
            }
        }

        // GET: ToDoActModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoActModel toDoActModel = db.ToDoActModels.Find(id);
            if (toDoActModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoActModel);
        }

        // POST: ToDoActModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoActModel toDoActModel = db.ToDoActModels.Find(id);
            db.ToDoActModels.Remove(toDoActModel);
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
