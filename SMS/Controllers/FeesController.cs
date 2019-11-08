using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SMS.Models;

namespace SMS.Controllers
{
    public class FeesController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Fees
        public IQueryable<Fee> GetFees()
        {
            return db.Fees;
        }

        // GET: api/Fees/5
        [ResponseType(typeof(Fee))]
        public async Task<IHttpActionResult> GetFee(string id)
        {
            Fee fee = await db.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }

            return Ok(fee);
        }

        // PUT: api/Fees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFee(string id, Fee fee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fee.fee_id)
            {
                return BadRequest();
            }

            db.Entry(fee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Fees
        [ResponseType(typeof(Fee))]
        public async Task<IHttpActionResult> PostFee(Fee fee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fees.Add(fee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FeeExists(fee.fee_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fee.fee_id }, fee);
        }

        // DELETE: api/Fees/5
        [ResponseType(typeof(Fee))]
        public async Task<IHttpActionResult> DeleteFee(string id)
        {
            Fee fee = await db.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }

            db.Fees.Remove(fee);
            await db.SaveChangesAsync();

            return Ok(fee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeeExists(string id)
        {
            return db.Fees.Count(e => e.fee_id == id) > 0;
        }
    }
}