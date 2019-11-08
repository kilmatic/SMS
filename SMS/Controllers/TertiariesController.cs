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
    public class TertiariesController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Tertiaries
        public IQueryable<Tertiary> GetTertiaries()
        {
            return db.Tertiaries;
        }

        // GET: api/Tertiaries/5
        [ResponseType(typeof(Tertiary))]
        public async Task<IHttpActionResult> GetTertiary(string id)
        {
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            if (tertiary == null)
            {
                return NotFound();
            }

            return Ok(tertiary);
        }

        // PUT: api/Tertiaries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTertiary(string id, Tertiary tertiary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tertiary.tertiary_id)
            {
                return BadRequest();
            }

            db.Entry(tertiary).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TertiaryExists(id))
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

        // POST: api/Tertiaries
        [ResponseType(typeof(Tertiary))]
        public async Task<IHttpActionResult> PostTertiary(Tertiary tertiary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tertiaries.Add(tertiary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TertiaryExists(tertiary.tertiary_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tertiary.tertiary_id }, tertiary);
        }

        // DELETE: api/Tertiaries/5
        [ResponseType(typeof(Tertiary))]
        public async Task<IHttpActionResult> DeleteTertiary(string id)
        {
            Tertiary tertiary = await db.Tertiaries.FindAsync(id);
            if (tertiary == null)
            {
                return NotFound();
            }

            db.Tertiaries.Remove(tertiary);
            await db.SaveChangesAsync();

            return Ok(tertiary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TertiaryExists(string id)
        {
            return db.Tertiaries.Count(e => e.tertiary_id == id) > 0;
        }
    }
}