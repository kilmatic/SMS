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
    public class TertiariesController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Tertiaries
        public async Task<ActionResult> Index()
        {
            return View(await db.Tertiaries.ToListAsync());
        }

        // GET: Tertiaries/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            if (tertiary == null)
            {
                return HttpNotFound();
            }
            return View(tertiary);
        }

        // GET: Tertiaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tertiaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "tertiary_id,institution,aps")] Tertiary tertiary)
        {
            if (ModelState.IsValid)
            {
                db.Tertiaries.Add(tertiary);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tertiary);
        }

        // GET: Tertiaries/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            if (tertiary == null)
            {
                return HttpNotFound();
            }
            return View(tertiary);
        }

        // POST: Tertiaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "tertiary_id,institution,aps")] Tertiary tertiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tertiary).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tertiary);
        }

        // GET: Tertiaries/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            if (tertiary == null)
            {
                return HttpNotFound();
            }
            return View(tertiary);
        }

        // POST: Tertiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            db.Tertiaries.Remove(tertiary);
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
