using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMSWebPortal.Models;

namespace SMSWebPortal.Controllers
{
    public class SubjectsController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Subjects
        public async Task<ActionResult> Index()
        {
            return View(await db.Subjects.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "subject_id,subject_name,test,assignment,exam")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "subject_id,subject_name,test,assignment,exam")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
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
