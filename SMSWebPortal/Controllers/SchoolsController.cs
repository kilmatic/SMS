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
    public class SchoolsController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Schools
        public async Task<ActionResult> Index()
        {
            return View(await db.Schools.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "school_id,schoole_name,district")] School school)
        {
            if (ModelState.IsValid)
            {
                school.school_id = Guid.NewGuid();
                db.Schools.Add(school);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "school_id,schoole_name,district")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            School school = await db.Schools.FindAsync(id);
            db.Schools.Remove(school);
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
