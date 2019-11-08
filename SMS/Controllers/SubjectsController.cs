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
    public class SubjectsController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Subjects
        public IQueryable<Subject> GetSubjects()
        {
            return db.Subjects;
        }

        // GET: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> GetSubject(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubject(string id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.subject_id)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(subject.subject_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = subject.subject_id }, subject);
        }

        // DELETE: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> DeleteSubject(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(string id)
        {
            return db.Subjects.Count(e => e.subject_id == id) > 0;
        }
    }
}