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
    public class SchoolsController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Schools
        public IQueryable<School> GetSchools()
        {
            return db.Schools;
        }

        // GET: api/Schools/5
        [ResponseType(typeof(School))]
        public async Task<IHttpActionResult> GetSchool(Guid id)
        {
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT: api/Schools/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSchool(Guid id, School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.school_id)
            {
                return BadRequest();
            }

            db.Entry(school).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        [ResponseType(typeof(School))]
        public async Task<IHttpActionResult> PostSchool(School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schools.Add(school);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolExists(school.school_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = school.school_id }, school);
        }

        // DELETE: api/Schools/5
        [ResponseType(typeof(School))]
        public async Task<IHttpActionResult> DeleteSchool(Guid id)
        {
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            db.Schools.Remove(school);
            await db.SaveChangesAsync();

            return Ok(school);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolExists(Guid id)
        {
            return db.Schools.Count(e => e.school_id == id) > 0;
        }
    }
}