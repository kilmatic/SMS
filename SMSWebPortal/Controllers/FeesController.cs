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
    public class FeesController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Fees
        public async Task<ActionResult> Index()
        {
            return View(await db.Fees.ToListAsync());
        }

        // GET: Fees/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = await db.Fees.FindAsync(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "fee_id,balance")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Fees.Add(fee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fee);
        }

        // GET: Fees/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = await db.Fees.FindAsync(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "fee_id,balance")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fee);
        }

        // GET: Fees/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = await db.Fees.FindAsync(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Fee fee = await db.Fees.FindAsync(id);
            db.Fees.Remove(fee);
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
