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
    public class ParentsController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Parents
        public IQueryable<Parent> GetParents()
        {
            return db.Parents;
        }

        // GET: api/Parents/5
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> GetParent(string id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // PUT: api/Parents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParent(string id, Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parent.parent_id)
            {
                return BadRequest();
            }

            db.Entry(parent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(id))
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

        // POST: api/Parents
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> PostParent(Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parents.Add(parent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParentExists(parent.parent_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parent.parent_id }, parent);
        }

        // DELETE: api/Parents/5
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> DeleteParent(string id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            db.Parents.Remove(parent);
            await db.SaveChangesAsync();

            return Ok(parent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentExists(string id)
        {
            return db.Parents.Count(e => e.parent_id == id) > 0;
        }
    }
}