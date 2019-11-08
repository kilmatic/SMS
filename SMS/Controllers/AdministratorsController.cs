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
using System.Web.Mvc;
using SMS.Models;

namespace SMS.Controllers
{
    public class AdministratorsController : ApiController
    {
        private SMSEntities db = new SMSEntities();

        // GET: api/Administrators
        public IQueryable<Administrator> GetAdministrators()
        {
            return db.Administrators;
        }

        // GET: api/Administrators/5
        [ResponseType(typeof(Administrator))]
        public async Task<IHttpActionResult> GetAdministrator(string id)
        {
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            return Ok(administrator);
        }

        // PUT: api/Administrators/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdministrator(string id, Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrator.admin_id)
            {
                return BadRequest();
            }

            db.Entry(administrator).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators
        [ResponseType(typeof(Administrator))]
        public async Task<IHttpActionResult> PostAdministrator(Administrator administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administrators.Add(administrator);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdministratorExists(administrator.admin_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = administrator.admin_id }, administrator);
        }

        // DELETE: api/Administrators/5
        [ResponseType(typeof(Administrator))]
        public async Task<IHttpActionResult> DeleteAdministrator(string id)
        {
            Administrator administrator = await db.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            db.Administrators.Remove(administrator);
            await db.SaveChangesAsync();

            return Ok(administrator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministratorExists(string id)
        {
            return db.Administrators.Count(e => e.admin_id == id) > 0;
        }
    }
}