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
    public class AdministratorsController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Administrators
        public async Task<ActionResult> Index()
        {
            return View(await db.Administrators.ToListAsync());
        }

        // GET: Administrators/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "admin_id,admin_name,surname")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "admin_id,admin_name,surname")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Administrator administrator = await db.Administrators.FindAsync(id);
            db.Administrators.Remove(administrator);
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
