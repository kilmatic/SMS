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
    public class TimetablesController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Timetables
        public IQueryable<Timetable> GetTimetables()
        {
            return db.Timetables;
        }

        // GET: api/Timetables/5
        [ResponseType(typeof(Timetable))]
        public async Task<IHttpActionResult> GetTimetable(string id)
        {
            Timetable timetable = await db.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }

            return Ok(timetable);
        }

        // PUT: api/Timetables/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTimetable(string id, Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timetable.timetable_id)
            {
                return BadRequest();
            }

            db.Entry(timetable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableExists(id))
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

        // POST: api/Timetables
        [ResponseType(typeof(Timetable))]
        public async Task<IHttpActionResult> PostTimetable(Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Timetables.Add(timetable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TimetableExists(timetable.timetable_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = timetable.timetable_id }, timetable);
        }

        // DELETE: api/Timetables/5
        [ResponseType(typeof(Timetable))]
        public async Task<IHttpActionResult> DeleteTimetable(string id)
        {
            Timetable timetable = await db.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }

            db.Timetables.Remove(timetable);
            await db.SaveChangesAsync();

            return Ok(timetable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimetableExists(string id)
        {
            return db.Timetables.Count(e => e.timetable_id == id) > 0;
        }
    }
}