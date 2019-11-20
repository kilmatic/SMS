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
    public class TimetablesController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Timetables
        public async Task<ActionResult> Index()
        {
            return View(await db.Timetables.ToListAsync());
        }

        // GET: Timetables/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = await db.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // GET: Timetables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "timetable_id,day_of_week,hour_of_day")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.Timetables.Add(timetable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(timetable);
        }

        // GET: Timetables/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = await db.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // POST: Timetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "timetable_id,day_of_week,hour_of_day")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(timetable);
        }

        // GET: Timetables/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = await db.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // POST: Timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Timetable timetable = await db.Timetables.FindAsync(id);
            db.Timetables.Remove(timetable);
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
